using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public class NPCController : MonoBehaviour
{
    public string CharacterName;
    public List<string> TextLines = new List<string>();
    public List<string> WheelOptions1 = new List<string>();
    public List<string> WheelOptions2 = new List<string>();
    public string[] CurrentEmotions;
    public float[] CurrentEmotionValues;
    public string[] CurrentMood;

    public Motus MotusTest;
    // Use this for initialization
    void Start()
    {
        MotusTest = new Motus();
        //MotusTest.SetAction(delegate { Update(); });
        MotusTest.SetMood(e_EmotionsState.JOY);
        UpdateEmotions();
        UpdateMood();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateEmotions()
    {
        CurrentEmotions = MotusTest.GetCurrentEmotionStates();
        CurrentEmotionValues = MotusTest.GetCurrentEmotionValues();     
    }

    public void UpdateMood()
    {
        CurrentMood = MotusTest.GetCurrentMood();
    }

    public void Reaction(e_EmotionsState p_TargetEmotion, float p_ReactionStrength)
    {
        MotusTest.CreateSensation(p_TargetEmotion, p_ReactionStrength);
        UpdateEmotions();
        UpdateMood();
    }
}
