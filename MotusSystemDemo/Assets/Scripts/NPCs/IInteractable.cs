using UnityEngine;
using System.Collections.Generic;

public interface IInteractable 
{
    bool IInDialogue { get; set; }
    Vector3 IDialoguePosition { get; set; }
    List<string> ITextLines { get; set; }
    List<string> IWheelOptions1 { get; set; }
    List<string> IWheelOptions2 { get; set; }

    void Interact(GameObject p_Player);

    void LeaveDialogue();
}
