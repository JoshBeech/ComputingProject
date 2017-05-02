using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class Spearman : NPC, ICombat
{
    public Weapon IWeaponType { get; set; }
    public Dictionary<string, int> IAnimations { get; set; }
    public List<GameObject> IOpponents { get; set; }
    public GameObject ITarget { get; set; }
    public int IHealth { get; set; }
    public bool IDead { get; set; }
    public bool IEngaged { get; set; }
    public e_EmotionsState IDeathFeeling { get; set; }

    public e_EmotionsState DeathFeeling;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        Agent = GetComponent<NavMeshAgent>();

        IAnimations = new Dictionary<string, int>();
        IAnimations.Add("Idle", Animator.StringToHash("Spear Idle"));
        IAnimations.Add("Walk", Animator.StringToHash("Spear Walk"));
        IAnimations.Add("Run", Animator.StringToHash("Spear Run"));
        IAnimations.Add("Relax", Animator.StringToHash("Spear Relax"));
        IAnimations.Add("Attack", Animator.StringToHash("Spear Melee Attack 01"));
        IAnimations.Add("Defend", Animator.StringToHash("Spear Idle"));
        IAnimations.Add("Die", Animator.StringToHash("Spear Die"));

        NPCAnimator.SetBool(IAnimations["Idle"], true);
        NPCAnimator.SetBool(IAnimations["Relax"], true);

        IHealth = 10;
        IDead = false;
        IEngaged = false;

        IDeathFeeling = DeathFeeling;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IDead)
        {
            if (IHealth <= 0)
            {
                Die();
            }
            else if (IEngaged)
            {
                if (Vector3.Distance(transform.position, ITarget.transform.position) <= 1.5f)
                {
                    NPCAnimator.SetBool(IAnimations["Run"], false);

                    Vector3 TargetVector = new Vector3(ITarget.transform.position.x - transform.position.x, 0.0f, ITarget.transform.position.z - transform.position.z);
                    Vector3 RotationVector = Vector3.RotateTowards(transform.forward, TargetVector, 5.0F * Time.deltaTime, 0.0F);
                    transform.rotation = Quaternion.LookRotation(RotationVector);


                    if (NPCAnimator.GetBool(IAnimations["Attack"]) == false)
                    {
                        NPCAnimator.SetTrigger(IAnimations["Attack"]);
                    }
                }
                else
                {
                    Agent.destination = ITarget.transform.position;
                    if (NPCAnimator.GetBool(IAnimations["Run"]) == false)
                    {
                        NPCAnimator.SetBool(IAnimations["Run"], true);
                    }
                }
            }
        }

    }


    public void SetTarget()
    {
        int l_RandomIndex = Random.Range(0, IOpponents.Count);
        ITarget = IOpponents[l_RandomIndex];

        if (ITarget != null)
        {
            Agent.destination = ITarget.transform.position;
            IEngaged = true;
            NPCAnimator.SetBool(IAnimations["Relax"], false);
        }
    }
    public void Die()
    {
        if(!NPCAnimator.GetBool(IAnimations["Die"]))
            NPCAnimator.SetTrigger(IAnimations["Die"]);

        foreach (KeyValuePair<string, int> l_Animation in IAnimations)
        {
            if (l_Animation.Key != "Die")
            {
                // No error checking needed as no floats in controller
                NPCAnimator.SetBool(IAnimations[l_Animation.Key], false);
            }
        }

        IDead = true;
        Agent.Stop();
        gameObject.AddComponent<NavMeshObstacle>();
        NavMeshObstacle l_Obstacle = GetComponent<NavMeshObstacle>();
        l_Obstacle.shape = NavMeshObstacleShape.Capsule;
        l_Obstacle.radius = 0.5f;
        l_Obstacle.center = new Vector3(0,1,0);
        l_Obstacle.carving = true;
        Debug.Log("Dying");
    }

    public void Enrage()
    {

    }
    public void Flee()
    {

    }
}
