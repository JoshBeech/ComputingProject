using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public class NPCController : MonoBehaviour
{
    public string CharacterName;
    public Vector3 DialoguePosition; 
    public List<string> TextLines = new List<string>();
    public List<string> WheelOptions1 = new List<string>();
    public List<string> WheelOptions2 = new List<string>();
    public string[] CurrentEmotions;
    public float[] CurrentEmotionValues;
    public string[] CurrentMood;

    public Transform Head;

    public Motus MotusTest;
    // Use this for initialization
    void Start()
    {
        DialoguePosition = transform.position + (transform.forward * 2.3f);
        MotusTest = new Motus();
        //MotusTest.SetAction(delegate { Update(); });
        MotusTest.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Entry", delegate { SetFace("Happy3"); });
        MotusTest.SetAction(e_EmotionsState.NEUTRAL, e_EmotionsState.NEUTRAL, "Entry", delegate { SetFace(); });
        MotusTest.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.SADNESS, "Entry", delegate { SetFace("Sad"); });

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

    /// <summary>
    /// Sets the facial expression of the NPC.
    /// leave parameter blank to remove the equiped expression if one is present
    /// </summary>
    /// <param name="p_FaceName"></param>
    public void SetFace(string p_FaceName = "")
    {
        if(Head.childCount >= 2)
        {
            foreach(Transform l_Child in Head)
            {
                if (l_Child.tag == "Face")
                    Destroy(l_Child.gameObject);
            }
        }

        if(p_FaceName == "")
        {
            return;
        }

        GameObject l_NewFace = Instantiate(FaceManager.GetFace(p_FaceName));

        l_NewFace.transform.SetParent(Head);
        l_NewFace.transform.position = Head.position;
        l_NewFace.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
    }
}
