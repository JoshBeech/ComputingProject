using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MotusSystem;

public class QuestGiver : NPC, ITalkable
{
    public DialogueManager IManager { get; set; }

    public Vector3 IDialoguePosition{ get; set; }

    public bool IInDialogue { get; set; }

    public List<string> ITextLines { get; set; }

    public List<string> IWheelOptions1 { get; set; }

    public List<string> IWheelOptions2 { get; set; }

    [Header("Quest Options")]
    public bool IsQuestActive = false;
    public bool CanGiveQuest = true;

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

        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.ANGER, "Entry", delegate { SetDialogue("Angered"); UpdateDialogue(); SetFace("Angry"); });
        NPCMotus.SetAction(e_EmotionsState.ANGER, e_EmotionsState.ANGER, "Exit", delegate { SetDialogue("Default"); UpdateDialogue(); SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.SADNESS, "Entry", delegate { SetDialogue("Sad"); UpdateDialogue(); SetFace("Sad"); });
        NPCMotus.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.SADNESS, "Exit", delegate { SetDialogue("Default"); UpdateDialogue(); SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Entry", delegate { SetDialogue("Happy"); UpdateDialogue(); SetFace("Happy"); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Exit", delegate { SetDialogue("Default"); UpdateDialogue(); SetFace(); });
    }

    private void QuestGiver_KingRescued(object sender, KingRescuedEventArgs e)
    {
        Debug.Log("King has been rescued");
        Reaction(e_EmotionsState.SURPRISE, 1.0f);
        Reaction(e_EmotionsState.JOY, 1.5f);
    }

    public void Interact(GameObject p_Player)
    {
        PlayerController l_PlayerController = p_Player.GetComponent<PlayerController>();

        StartCoroutine(l_PlayerController.StartDialogue(IDialoguePosition, transform.position));
        l_PlayerController.TheDialogueManager.StartDialogue(this, this);
        IInDialogue = true;
        l_PlayerController.CanMove = false;
    }

    public void LeaveDialogue()
    {
        IInDialogue = false;
    }

    public void GiveQuest()
    {
        IsQuestActive = true;
        CanGiveQuest = false;
    }

    public void DenyQuest()
    {
        CanGiveQuest = false;
    }


    /// <summary>
    /// Clear current dialogue text and set new ones,
    /// Update dialogue manager to match
    /// </summary>
    /// <param name="p_DialogueToSet"></param>
    public void SetDialogue(string p_DialogueToSet)
    {
        ITextLines.Clear();
        IWheelOptions1.Clear();
        IWheelOptions2.Clear();  
        
        if(p_DialogueToSet.Equals("Default"))
        {
            ITextLines.Add("Hi there, I have a quest for you. \n\n Would you like to hear more?");
            ITextLines.Add("Our King has been captured, will you go rescue him?");

            // String breakdown:
            // First - Position on wheel 0-5 starting top right going clockwise
            // Second - Listner to for dialogue manager to assign to button
            // Third - Text for button
            IWheelOptions1.Add("0:1:Talk");
            IWheelOptions1.Add("1:12:Accept");
            IWheelOptions1.Add("2:2:Leave");
            IWheelOptions1.Add("3:5:Refuse");
            IWheelOptions1.Add("4:8:Harsh Refuse");
        }
        else if (p_DialogueToSet.Equals("Happy"))
        {
            ITextLines.Add("Thank you so much.");

            IWheelOptions1.Add("0:2:Leave");
        }
        else if (p_DialogueToSet.Equals("Sad"))
        {
            ITextLines.Add("Please return if you reconsider");
            ITextLines.Add("Help us, you're our only hope");

            IWheelOptions1.Add("0:1:Talk");
            IWheelOptions1.Add("2:2:Leave");
            IWheelOptions1.Add("3:12:Accept");

        }
        else if(p_DialogueToSet.Equals("Angered"))
        {
            ITextLines.Add("Well no need to be so rude.");
            ITextLines.Add("I think it's time for you to leave");

            IWheelOptions1.Add("0:1:Talk");
            IWheelOptions1.Add("2:2:Leave");
            IWheelOptions1.Add("3:9:Apologise");
        }
        else if(p_DialogueToSet.Equals("Quest Completed"))
        {
            ITextLines.Add("You saved the King \n\n We are eternally grateful");

            IWheelOptions1.Add("0:2:Leave");
        }
    }

    public void UpdateDialogue()
    {
        IManager.RefreshDialogue();
    }
}
