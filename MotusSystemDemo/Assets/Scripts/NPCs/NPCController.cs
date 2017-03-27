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

    public Motus MotusTest;
    // Use this for initialization
    void Start()
    {
        MotusTest = new Motus();
        UpdateEmotions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateEmotions()
    {
        CurrentEmotions = MotusTest.GetCurrentEmotionStates();        
    }

    public void Reaction(e_EmotionsState p_TargetEmotion)
    {
        MotusTest.CreateSensation(p_TargetEmotion);
        UpdateEmotions();
    }
}
