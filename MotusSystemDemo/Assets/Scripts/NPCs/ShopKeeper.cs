using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using MotusSystem;

public class ShopKeeper : NPC, ITalkable
{
    public DialogueManager IManager { get; set; }
    public bool IInDialogue { get; set; }
    public Vector3 IDialoguePosition { get; set; }
    public List<string> ITextLines { get; set; }
    public List<string> IWheelOptions1 { get; set; }
    public List<string> IWheelOptions2 { get; set; }

    public XmlDocument NPCDialogue;

    new void Start()
    {
        base.Start();

        IManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        IInDialogue = false;
        IDialoguePosition = transform.position + (transform.forward * 2.3f);

        ITextLines = new List<string>(); 
        IWheelOptions1 = new List<string>();
        IWheelOptions2 = new List<string>();

        SetDialogue("Default");

        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.ANGER, "Entry", delegate { SetDialogue("Angered"); UpdateDialogue(); SetFace("Angry2"); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.ANGER, "Exit", delegate { SetDialogue("Default"); UpdateDialogue(); SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Entry", delegate { SetDialogue("Happy"); UpdateDialogue(); SetFace("Happy2"); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Exit", delegate { SetDialogue("Default"); UpdateDialogue(); SetFace(); });
    }

    public void Interact(GameObject p_Player)
    {        
        PlayerController l_PlayerController = p_Player.GetComponent<PlayerController>();

        StartCoroutine(l_PlayerController.SwapCamera());
        l_PlayerController.TheDialogueManager.StartDialogue(this, this);
        IInDialogue = true;
        // TODO: link to after camera turns off
        p_Player.transform.position = IDialoguePosition;
        p_Player.transform.LookAt(transform.position);
        l_PlayerController.CanMove = false;
    }

    public void LeaveDialogue()
    {
        IInDialogue = false;
    }

    public void SetDialogue(string p_DialogueToSet)
    {
        ITextLines.Clear();
        IWheelOptions1.Clear();
        IWheelOptions2.Clear();

        if (p_DialogueToSet.Equals("Default"))
        {
            ITextLines.Add("Welcome to my shop, how can I help you?");
            ITextLines.Add("Thank you for your purchase");

            // String breakdown:
            // First - Position on wheel 0-2 starting top right going clockwise 
            // and 3-5 top left going anti-clockwise
            // Second - Listner to for dialogue manager to assign to button
            // Third - Text for button
            IWheelOptions1.Add("0:1:Buy Item");
            IWheelOptions1.Add("2:2:Leave");
            IWheelOptions1.Add("5:3:Extra Actions");

            IWheelOptions2.Add("0:4:Do Favour");
            IWheelOptions2.Add("1:4:Compliment");
            IWheelOptions2.Add("3:8:Shout At");
            IWheelOptions2.Add("5:3:Default Actions");
        }
        else if (p_DialogueToSet.Equals("Happy"))
        {
            ITextLines.Add("Thank you so much. I'd like to offer you discounted prices");

            IWheelOptions1.Add("0:1:Buy Item");
            IWheelOptions1.Add("2:2:Leave");
            IWheelOptions1.Add("5:3:Extra Actions");

            IWheelOptions2.Add("3:8:Shout At");
            IWheelOptions2.Add("5:3:Default Actions");
        }
        else if (p_DialogueToSet.Equals("Angered"))
        {
            ITextLines.Add("You are no longer welcome at my shop. \n\n I think it's time for you to leave.");

            IWheelOptions1.Add("2:2:Leave");
            IWheelOptions1.Add("3:9:Apologise");
        }
    }

    public void UpdateDialogue()
    {
        IManager.RefreshDialogue();
    }
}
