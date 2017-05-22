using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Companion : NPC, ITalkable
{
    // Interactable variables 
    public DialogueManager IManager { get; set; }
    public bool IInDialogue { get; set; }
    public Vector3 IDialoguePosition { get; set; }
    public List<string> ITextLines { get; set; }
    public List<string> IWheelOptions1 { get; set; }
    public List<string> IWheelOptions2 { get; set; }

    public bool Talking;
    public List<string> TextLines = new List<string>();
    public List<string> WheelOptions1 = new List<string>();
    public List<string> WheelOptions2 = new List<string>();

    [Header("Companion NPC Attributes")]
    public GameObject Player;
    //public NavMeshAgent Agent;
    public Transform House;
    public bool CompanionActive = true;
    public bool InsideWall = true;

    public float[] WalkSpeedSettings;
    public float[] SprintSpeedSettings;

    private PlayerController m_PlayerController;
    private Transform Target;
    public Combat CombatScript;


    // Use this for initialization
    new void Start()
    {
        base.Start();

        IManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        IInDialogue = false;
        Talking = IInDialogue;
        ITextLines = TextLines;
        IWheelOptions1 = WheelOptions1;
        IWheelOptions2 = WheelOptions2;

        Player = GameObject.Find("Player");
        m_PlayerController = Player.GetComponent<PlayerController>();
        m_PlayerController.Companion = gameObject;
        Agent = GetComponent<NavMeshAgent>();
        Agent.destination = Player.transform.position;
        House = GameObject.Find("CompanionHouse").transform;

        WalkSpeedSettings = new float[3];
        SprintSpeedSettings = new float[3];

        SetMovementModeParameters("Default");

        FollowPlayer();

        CombatScript = GetComponent<Combat>();
        CombatScript.enabled = false;

        NPCAnimations = CombatScript.NPCAnimations;
        NPCAnimator.SetBool(NPCAnimations["Relax"], false);
        NPCAnimator.SetBool(NPCAnimations["Idle"], true);

        NPCMotus.SetAction(MotusSystem.e_EmotionsState.ANGER, MotusSystem.e_EmotionsState.DISGUST, "Entry", delegate { LeavePlayer(); SetFace("Angry"); });
        NPCMotus.SetAction(MotusSystem.e_EmotionsState.ANGER, MotusSystem.e_EmotionsState.DISGUST, "Exit", delegate { FollowPlayer(); SetFace(); });

        NPCMotus.SetAction(MotusSystem.e_EmotionsState.SADNESS, MotusSystem.e_EmotionsState.FEAR, "Entry", 
            delegate {
                SetMovementModeParameters("Sad");
                SetFace("Sad");
        });
        NPCMotus.SetAction(MotusSystem.e_EmotionsState.SADNESS, MotusSystem.e_EmotionsState.FEAR, "Exit",
    delegate {
        SetMovementModeParameters("Default");
        SetFace();
    });
    }

    // Update is called once per frame
    void Update()
    {
        if (!CombatScript.enabled && !CombatScript.Dead)
        {
            if (!IInDialogue)
            {
                if (Vector3.Distance(transform.position, Target.position) > Agent.stoppingDistance - 1.0f)
                {
                    Agent.destination = Target.position;
                }

                if (Vector3.Distance(transform.position, Target.position) > 5.0f)
                    SetMovementMode(true);
                else if (Vector3.Distance(transform.position, Target.position) < Agent.stoppingDistance)
                    SetMovementMode(false);

                if (Agent.velocity == Vector3.zero)
                {
                    NPCAnimator.SetBool(NPCAnimations["Walk"], false);
                    NPCAnimator.SetBool(NPCAnimations["Run"], false);
                }
                else if (Agent.velocity.magnitude < 3.0f)
                {
                    NPCAnimator.SetBool(NPCAnimations["Walk"], true);
                    NPCAnimator.SetBool(NPCAnimations["Run"], false);
                }
                else
                {
                    NPCAnimator.SetBool(NPCAnimations["Walk"], false);
                    NPCAnimator.SetBool(NPCAnimations["Run"], true);
                }

                if (!CompanionActive)
                {
                    Agent.stoppingDistance = 1.0f;

                    if (Agent.velocity == Vector3.zero && Agent.remainingDistance < 1f)
                    {                     
                        Vector3 RotationVector = Vector3.RotateTowards(transform.forward, House.transform.forward, 5.0F * Time.deltaTime, 0.0F);
                        transform.rotation = Quaternion.LookRotation(RotationVector);
                    }
                }                
            }
            else
            {
                Agent.Stop();
            }
        }
        else
        {
            if (CombatScript.Engaged == false)
            {
                CombatScript.enabled = false;
            }
        }

    }

    public void Interact(GameObject p_Player)
    {
        IDialoguePosition = transform.position + (transform.forward * 2.3f);
        IDialoguePosition = new Vector3(IDialoguePosition.x, 0.0f, IDialoguePosition.z);

        PlayerController l_PlayerController = p_Player.GetComponent<PlayerController>();

        StartCoroutine(l_PlayerController.StartDialogue(IDialoguePosition, new Vector3(transform.position.x, 0.0f, transform.position.z)));
        l_PlayerController.TheDialogueManager.StartDialogue(this, this);
        IInDialogue = true;
        Talking = IInDialogue;      
        l_PlayerController.CanMove = false;
    }

    public void LeaveDialogue()
    {
        Agent.Resume();
        IInDialogue = false;
        Talking = IInDialogue;
    }

    public void FollowPlayer()
    {
        CompanionActive = true;
        Target = Player.transform;
        m_PlayerController.Companion = gameObject;
        Agent.destination = Target.position;
        Agent.stoppingDistance = 4.0f;
    }

    public void LeavePlayer()
    {
        CompanionActive = false;
        Target = House;
        m_PlayerController.Companion = null;
        // Check if outside castle wall,
        // if yes head to wall then warp inside then head to house
        Agent.Stop();
        Agent.destination = Target.position;
        Agent.stoppingDistance = 1.0f;
    }

    public void SetMovementModeParameters(string p_SpeedMode)
    {
        if (p_SpeedMode.Equals("Default"))
        {
            WalkSpeedSettings[0] = 2.0f;
            WalkSpeedSettings[1] = 3.0f;
            WalkSpeedSettings[2] = 4.0f;

            SprintSpeedSettings[0] = 5.0f;
            SprintSpeedSettings[1] = 4.0f;
            SprintSpeedSettings[2] = 6.0f;
        }
        else if (p_SpeedMode.Equals("Sad"))
        {
            WalkSpeedSettings[0] = 1.0f;
            WalkSpeedSettings[1] = 1.0f;
            WalkSpeedSettings[2] = 2.0f;

            SprintSpeedSettings = WalkSpeedSettings;
        }
    }

    public void SetMovementMode(bool p_IsSprinting)
    {
        if(p_IsSprinting)
        {
            Agent.speed = SprintSpeedSettings[0];
            Agent.acceleration = SprintSpeedSettings[1];
            Agent.stoppingDistance = SprintSpeedSettings[2];
        }
        else
        {
            Agent.speed = WalkSpeedSettings[0];
            Agent.acceleration = WalkSpeedSettings[1];
            Agent.stoppingDistance = WalkSpeedSettings[2];
        }
    }

    public void EnterCombat()
    {
        CombatScript.enabled = true;
    }
}
