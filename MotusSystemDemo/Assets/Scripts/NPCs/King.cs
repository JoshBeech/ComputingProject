using System;
using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public class King : NPC, ITalkable
{
    public DialogueManager IManager { get; set; }
    public Vector3 IDialoguePosition { get; set; }
    public bool IInDialogue { get; set; }
    public List<string> ITextLines { get; set; }
    public List<string> IWheelOptions1 { get; set; }
    public List<string> IWheelOptions2 { get; set; }

    [Header("King Variables")]
    public Transform Temple;
    public event EventHandler<KingRescuedEventArgs> KingRescued;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        ITextLines = new List<string>();
        IWheelOptions1 = new List<string>();
        IWheelOptions2 = new List<string>();
        SetDialogue("Default");

        KingRescued += f_KingRescued;
    }
    
    protected virtual void OnRescue(KingRescuedEventArgs e)
    {
        EventHandler<KingRescuedEventArgs> handler = KingRescued;

        if (handler != null)
            handler(this, e);
    }

    public void Interact(GameObject p_Player)
    {
        IDialoguePosition = transform.position + (transform.forward * 2.3f);
        IDialoguePosition = new Vector3(IDialoguePosition.x, 0.0f, IDialoguePosition.z);

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

        KingRescuedEventArgs l_Args = new KingRescuedEventArgs();
        OnRescue(l_Args);
        transform.SetParent(Temple);
        transform.localPosition = new Vector3(5.0f, 0, 4.0f);
        transform.localEulerAngles = Vector3.zero;
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

        if (p_DialogueToSet.Equals("Default"))
        {
            ITextLines.Add("Hi there, thank you for rescuing me");
            ITextLines.Add("Now I shall return to my kingdom");

            // String breakdown:
            // First - Position on wheel 0-5 starting top right going clockwise
            // Second - Listner to for dialogue manager to assign to button
            // Third - Text for button
            IWheelOptions1.Add("0:1:Talk");
            IWheelOptions1.Add("2:2:Leave");
        }
    }

    public void UpdateDialogue()
    {
        IManager.RefreshDialogue();
    }
}
