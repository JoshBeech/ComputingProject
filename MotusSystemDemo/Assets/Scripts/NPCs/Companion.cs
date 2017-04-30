using UnityEngine;
using System.Collections.Generic;

public class Companion : NPC, IInteractable
{
    public GameObject Player;
    public NavMeshAgent Agent;

    public Vector3 DialoguePosition { get; set; }
    public List<string> ITextLines { get; set; }
    public List<string> IWheelOptions1 { get; set; }
    public List<string> IWheelOptions2 { get; set; }

    public List<string> TextLines = new List<string>();
    public List<string> WheelOptions1 = new List<string>();
    public List<string> WheelOptions2 = new List<string>();

    // Use this for initialization
    new void Start()
    {
        base.Start();

        ITextLines = TextLines;
        IWheelOptions1 = WheelOptions1;
        IWheelOptions2 = WheelOptions2;

        Player = GameObject.Find("Player");
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) > Agent.stoppingDistance - 1.0f)
        {
            Agent.destination = Player.transform.position;
        }

    }

    public void Interact(GameObject p_Player)
    {
        DialoguePosition = transform.position + (transform.forward * 2.3f);
        DialoguePosition = new Vector3(DialoguePosition.x, 0.0f, DialoguePosition.z);
        Debug.Log(DialoguePosition);

        PlayerController l_PlayerController = p_Player.GetComponent<PlayerController>();

        StartCoroutine(l_PlayerController.SwapCamera());
        l_PlayerController.TheDialogueManager.StartDialogue(this, this);
        // TODO: link to after camera turns off
        p_Player.transform.position = DialoguePosition;
        p_Player.transform.LookAt(new Vector3(transform.position.x, 0.0f, transform.position.z));
        l_PlayerController.CanMove = false;
    }
}
