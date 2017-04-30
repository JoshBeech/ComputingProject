﻿using UnityEngine;
using System.Collections.Generic;

public class ShopKeeper : NPC, IInteractable
{
    public Vector3 DialoguePosition { get; set; }
    public List<string> ITextLines { get; set; }
    public List<string> IWheelOptions1 { get; set; }
    public List<string> IWheelOptions2 { get; set; }

    // Local versions for dialogue text so they can be editted in inspector
    // Since properties can't
    public List<string> TextLines = new List<string>();
    public List<string> WheelOptions1 = new List<string>();
    public List<string> WheelOptions2 = new List<string>();

    new void Start()
    {
        base.Start();
        DialoguePosition = transform.position + (transform.forward * 2.3f);

        ITextLines = TextLines;
        IWheelOptions1 = WheelOptions1;
        IWheelOptions2 = WheelOptions2;
    }

    public void Interact(GameObject p_Player)
    {        
        PlayerController l_PlayerController = p_Player.GetComponent<PlayerController>();

        StartCoroutine(l_PlayerController.SwapCamera());
        l_PlayerController.TheDialogueManager.StartDialogue(this, this);
        // TODO: link to after camera turns off
        p_Player.transform.position = DialoguePosition;
        p_Player.transform.LookAt(transform.position);
        l_PlayerController.CanMove = false;
    }
}
