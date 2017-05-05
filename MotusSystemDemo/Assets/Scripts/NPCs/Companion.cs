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

    // Companion Variables
    public GameObject Player;
    //public NavMeshAgent Agent;
    public Transform House;
    public bool CompanionActive = true;
    public bool InsideWall = true;

    private PlayerController m_PlayerController;
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

        CombatScript = GetComponent<Combat>();
        CombatScript.enabled = false;

        NPCAnimations = CombatScript.NPCAnimations;
        NPCAnimator.SetBool(NPCAnimations["Relax"], false);
        NPCAnimator.SetBool(NPCAnimations["Idle"], true);

        NPCMotus.SetAction(MotusSystem.e_EmotionsState.ANGER, MotusSystem.e_EmotionsState.DISGUST, "Entry", delegate { LeavePlayer(); });
        NPCMotus.SetAction(MotusSystem.e_EmotionsState.ANGER, MotusSystem.e_EmotionsState.DISGUST, "Exit", delegate { FollowPlayer(); });

        NPCMotus.SetAction(MotusSystem.e_EmotionsState.SADNESS, MotusSystem.e_EmotionsState.FEAR, "Entry", 
            delegate { ChangeSteering(1.0f, 1.0f, 2.0f);
                SetFace("Sad");
        });
        NPCMotus.SetAction(MotusSystem.e_EmotionsState.SADNESS, MotusSystem.e_EmotionsState.FEAR, "Exit",
    delegate {
        ChangeSteering(2.0f, 2.0f, 4.0f);
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
                if (CompanionActive)
                {
                    if (Vector3.Distance(transform.position, Player.transform.position) > Agent.stoppingDistance - 1.0f)
                    {
                        Agent.destination = Player.transform.position;

                        if (m_PlayerController.IsMoving && m_PlayerController.IsSprinting)
                        {
                            //ChangeSteering(4.0f, 2.0f, 6.0f);                        
                        }
                        else if (m_PlayerController.IsMoving && !m_PlayerController.IsSprinting)
                        {
                            //ChangeSteering(2.0f, 2.0f, 4.0f);
                        }
                    }

                    if (Agent.velocity == Vector3.zero)
                    {
                        NPCAnimator.SetBool(NPCAnimations["Walk"], false);
                        NPCAnimator.SetBool(NPCAnimations["Run"], false);
                    }

                }
                else
                {
                    if (Agent.velocity == Vector3.zero && Agent.remainingDistance < 1f)
                    {
                        NPCAnimator.SetBool(NPCAnimations["Idle"], true);
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

        StartCoroutine(l_PlayerController.SwapCamera());
        l_PlayerController.TheDialogueManager.StartDialogue(this, this);
        IInDialogue = true;
        Talking = IInDialogue;
        
        // TODO: link to after camera turns off
        p_Player.transform.position = IDialoguePosition;
        p_Player.transform.LookAt(new Vector3(transform.position.x, 0.0f, transform.position.z));
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
        m_PlayerController.Companion = gameObject;
        Agent.destination = Player.transform.position;
        Agent.stoppingDistance = 4.0f;
    }

    public void LeavePlayer()
    {
        CompanionActive = false;
        m_PlayerController.Companion = null;
        // Check if outside castle wall,
        // if yes head to wall then warp inside then head to house
        Agent.destination = House.position;
        Agent.stoppingDistance = 1.0f;
        Agent.Stop();
    }

    public void ChangeSteering(float p_NewSpeed, float p_NewAcceleration, float p_NewStoppingDistance)
    {
        Agent.speed = p_NewSpeed;
        Agent.acceleration = p_NewAcceleration;
        Agent.stoppingDistance = p_NewStoppingDistance;

        if(Agent.speed > 3.0f && !NPCAnimator.GetBool("Run"))
        {
            NPCAnimator.SetBool("Walk", false);
            NPCAnimator.SetBool("Run", true);
        }
        else if(Agent.speed > 0.0f && !NPCAnimator.GetBool("Walk"))
        {
            NPCAnimator.SetBool("Walk", true);
            NPCAnimator.SetBool("Run", false);
        }
    }

    public void EnterCombat()
    {
        CombatScript.enabled = true;
    }
}
