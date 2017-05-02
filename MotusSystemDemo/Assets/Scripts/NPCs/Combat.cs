using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public class Combat : NPC
{
    public NavMeshObstacle NavObstacle;
    public Weapon NPCWeapon;
    public List<GameObject> Opponents = new List<GameObject>();
    public GameObject Target;
    public int Health = 10;
    public bool Dead = false;
    public bool Engaged = false;
    public bool OnGuard = false;
    public e_EmotionsState DeathFeeling;

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

        Health = 10;
        Dead = false;
        Engaged = false;
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
            else if(OnGuard)
            {     
                if (NPCAnimator.GetBool(NPCAnimations["Defend"]) == false)
                {
                    NPCAnimator.SetBool(NPCAnimations["Relax"], false);
                    NPCAnimator.SetBool(NPCAnimations["Defend"], true);
                }
            }
        }

    }


    public void SetTarget()
    {
        int l_RandomIndex = Random.Range(0, Opponents.Count);
        Target = Opponents[l_RandomIndex];

        if (Target != null)
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
        Debug.Log("Dying");
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

    }
    public void Flee()
    {

    }
}
