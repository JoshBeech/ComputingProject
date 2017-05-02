using System;
using UnityEngine;
using MotusSystem;
using System.Collections.Generic;

public class Combat : NPC
{
    [Header("CombatNPC Settings")]
    public GameObject Base;
    public NavMeshObstacle NavObstacle;
    public Weapon NPCWeapon;
    public List<GameObject> Opponents = new List<GameObject>();
    public GameObject Target;
    public int Health = 10;
    public bool Engaged = false;
    public bool Alert = false;
    public bool Dead = false;
    public bool Fleeing = false;
    public e_EmotionsState DeathFeeling;
    public event EventHandler<NPCDiedEventArgs> NPCDied;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        Agent = GetComponent<NavMeshAgent>();
        NavObstacle = GetComponent<NavMeshObstacle>();

        if(NavObstacle != null)
            NavObstacle.enabled = false;

        NPCAnimations = new Dictionary<string, int>();
        SetupAnimations();

        if (NPCAnimations.ContainsKey("Idle"))
            NPCAnimator.SetBool(NPCAnimations["Idle"], true);

        NPCAnimator.SetBool(NPCAnimations["Relax"], true);

        Dead = false;
        Engaged = false;

        NPCDied += f_NPCDied;

        NPCMotus.SetAction(e_EmotionsState.FEAR, e_EmotionsState.FEAR, "Entry", delegate { Flee(); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.ANGER, "Entry", delegate { Enrage(); });
    }

    private void SetupAnimations()
    {
        if(NPCWeapon.WeaponType.Equals("Spear"))
        {
            NPCAnimations.Add("Idle", Animator.StringToHash("Spear Idle"));
            NPCAnimations.Add("Walk", Animator.StringToHash("Spear Walk"));
            NPCAnimations.Add("Run", Animator.StringToHash("Spear Run"));
            NPCAnimations.Add("Relax", Animator.StringToHash("Spear Relax"));
            NPCAnimations.Add("Attack", Animator.StringToHash("Spear Melee Attack 01"));
            NPCAnimations.Add("Defend", Animator.StringToHash("Spear Defend"));
            NPCAnimations.Add("Die", Animator.StringToHash("Spear Die"));
        }
        else if(NPCWeapon.WeaponType.Equals("TH Sword"))
        {
            NPCAnimations.Add("Idle", Animator.StringToHash("TH Sword Idle"));
            NPCAnimations.Add("Walk", Animator.StringToHash("TH Sword Walk"));
            NPCAnimations.Add("Run", Animator.StringToHash("TH Sword Run"));
            NPCAnimations.Add("Relax", Animator.StringToHash("TH Sword Relax"));
            NPCAnimations.Add("Attack", Animator.StringToHash("TH Sword Melee Attack 01"));
            NPCAnimations.Add("Defend", Animator.StringToHash("TH Sword Defend"));
            NPCAnimations.Add("Die", Animator.StringToHash("TH Sword Die"));
        }
        else
        {
            NPCAnimations.Add("Walk", Animator.StringToHash("Walk"));
            NPCAnimations.Add("Run", Animator.StringToHash("Run"));
            NPCAnimations.Add("Relax", Animator.StringToHash("Relax"));
            NPCAnimations.Add("Defend", Animator.StringToHash("Defend"));
            NPCAnimations.Add("Die", Animator.StringToHash("Die"));

            if (NPCWeapon.WeaponType.Equals("Sword"))
                NPCAnimations.Add("Attack", Animator.StringToHash("Melee Right Attack 01"));
            else if (NPCWeapon.WeaponType.Equals("Scythe"))
                NPCAnimations.Add("Attack", Animator.StringToHash("Melee Right Attack 03"));
        }

        NPCAnimations.Add("Dead", Animator.StringToHash("Dead"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            if (Health <= 0)
            {
                Die();
            }
            else if (Engaged)
            {
                if (!Target.GetComponent<Combat>().Dead && Vector3.Distance(transform.position, Target.transform.position) <= 1.5f)
                {
                    NPCAnimator.SetBool(NPCAnimations["Run"], false);

                    Vector3 TargetVector = new Vector3(Target.transform.position.x - transform.position.x, 0.0f, Target.transform.position.z - transform.position.z);
                    Vector3 RotationVector = Vector3.RotateTowards(transform.forward, TargetVector, 5.0F * Time.deltaTime, 0.0F);
                    transform.rotation = Quaternion.LookRotation(RotationVector);

                    if (NPCAnimator.GetBool(NPCAnimations["Attack"]) == false)
                    {
                        NPCAnimator.SetTrigger(NPCAnimations["Attack"]);
                    }
                }
                else if (Target.GetComponent<Combat>().Dead)
                {
                    SetTarget();
                }
                else
                { 
                    Agent.destination = Target.transform.position;
                    if (NPCAnimator.GetBool(NPCAnimations["Run"]) == false)
                    {
                        NPCAnimator.SetBool(NPCAnimations["Run"], true);
                    }
                }
            }
        }
    }


    public void SetTarget()
    {
        int l_RandomIndex = UnityEngine.Random.Range(0, Opponents.Count);
        Target = Opponents[l_RandomIndex];

        if (Target != null && Agent != null)
        {
            Agent.destination = Target.transform.position;
            Engaged = true;
            NPCAnimator.SetBool(NPCAnimations["Relax"], false);
            NPCAnimator.SetBool(NPCAnimations["Defend"], false);
        }
    }

    public void Die()
    {
        if (!NPCAnimator.GetBool(NPCAnimations["Die"]))
            NPCAnimator.SetTrigger(NPCAnimations["Die"]);

        foreach (KeyValuePair<string, int> l_Animation in NPCAnimations)
        {
            if (l_Animation.Key != "Die")
            {
                // No error checking needed as no floats in controller
                NPCAnimator.SetBool(NPCAnimations[l_Animation.Key], false);
            }
        }

        Dead = true;
        NPCAnimator.SetBool(NPCAnimations["Dead"], true);
        Agent.Stop();
        Agent.enabled = false;
        if(NavObstacle!= null)
            NavObstacle.enabled = true;

        NPCDiedEventArgs l_args = new NPCDiedEventArgs();
        l_args.Emotion = DeathFeeling;
        onDeath(l_args);
    }

    protected virtual void onDeath(NPCDiedEventArgs e)
    {
        EventHandler<NPCDiedEventArgs> handler = NPCDied;

        if(handler != null)
        {
            handler(this, e);
        }
    }

    public void f_NPCDied(object sender, NPCDiedEventArgs e)
    {
        if(!Dead)
            Reaction(e.Emotion, 0.2f);
    }

    public void Disengage()
    {
        Engaged = false;
        NPCAnimator.SetBool(NPCAnimations["Run"], false);
        NPCAnimator.SetBool(NPCAnimations["Defend"], false);
        Target = null;
    }

    public void Enrage()
    {
        NPCWeapon.Damage = 3;
        Health = 12;
    }


    public void Flee()
    {
        Disengage(); ;
        Agent.destination = Base.transform.position;
        Agent.speed = 8;
        NPCAnimator.SetBool(NPCAnimations["Run"], true);
        Fleeing = true;
    }
}
