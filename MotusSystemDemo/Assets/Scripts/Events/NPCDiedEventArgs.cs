using System;
using MotusSystem;

public class NPCDiedEventArgs : EventArgs
{
    public e_EmotionsState Emotion { get; set; }
}
