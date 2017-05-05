using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public class Simple : NPC, ITalkable {

    public DialogueManager IManager { get; set; }
    public bool IInDialogue { get; set; }
    public Vector3 IDialoguePosition { get; set; }
    public List<string> ITextLines { get; set; }
    public List<string> IWheelOptions1 { get; set; }
    public List<string> IWheelOptions2 { get; set; }

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

        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.ANGER, "Entry", delegate {  SetFace("Cross"); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.ANGER, "Exit", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.DISGUST, "Entry", delegate { SetFace("Angry2"); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.DISGUST, "Exit", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.SURPRISE, "Entry", delegate { SetFace("Angry"); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.SURPRISE, "Exit", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.DISGUST, e_EmotionsState.SURPRISE, "Entry", delegate { SetFace("Surprised"); });
        NPCMotus.SetAction(e_EmotionsState.DISGUST, e_EmotionsState.SURPRISE, "Exit", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.ANGER, "Entry", delegate { SetFace("Smirk"); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.ANGER, "Exit", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Entry", delegate { SetFace("Happy2"); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Exit", delegate {  SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.DISGUST, "Entry", delegate { SetFace("Sad"); });
        NPCMotus.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.DISGUST, "Exit", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.TRUST, "Entry", delegate { SetFace("Bored"); });
        NPCMotus.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.TRUST, "Exit", delegate { SetFace(); });
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
            ITextLines.Add("Hello. \n Welcome to the Motus System Demo.");

            // String breakdown:
            // First - Position on wheel 0-2 starting top right going clockwise 
            // and 3-5 top left going anti-clockwise
            // Second - Listner to for dialogue manager to assign to button
            // Third - Text for button
            IWheelOptions1.Add("0:4:Increase Joy");
            IWheelOptions1.Add("1:6:Increase Anticipation");
            IWheelOptions1.Add("2:2:End Dialogue");
            IWheelOptions1.Add("3:5:Increase Sadness");
            IWheelOptions1.Add("4:7:Increase Surprise");
            IWheelOptions1.Add("5:3:Other Actions");

            IWheelOptions2.Add("0:8:Increase Anger");
            IWheelOptions2.Add("1:10:Increase Trust");
            IWheelOptions2.Add("3:9:Increase Fear");
            IWheelOptions2.Add("4:11:Increase Disgust");
            IWheelOptions2.Add("5:3:Other Actions");
        }
    }

    public void UpdateDialogue()
    {
        IManager.RefreshDialogue();
    }
}
