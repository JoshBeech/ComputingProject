using System;
using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public abstract class NPC : MonoBehaviour
{
    [Header("Base NPC settings")]
    public string Name;
    public string SkinColour;
    public GameObject King;

    public Transform Head;
    public NavMeshAgent Agent;

    public Motus NPCMotus;
    public string[] CurrentEmotions;
    public float[] CurrentEmotionValues;
    public string[] CurrentMood;

    public Animator NPCAnimator;
    public Dictionary<string, int> NPCAnimations = new Dictionary<string, int>();

    
    // Use this for initialization
    protected void Start()
    {
        Name = gameObject.name;
        NPCMotus = new Motus();

        NPCMotus.SetAction(e_EmotionsState.NEUTRAL, e_EmotionsState.NEUTRAL, "Entry", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.SURPRISE, "Entry", delegate { SetFace("Happy3"); });

        UpdateEmotions();
        UpdateMood();

        NPCAnimator = GetComponentInChildren<Animator>();

        if (King != null)
        {
            King.GetComponent<King>().KingRescued += f_KingRescued;
        }
    }


    public void UpdateEmotions()
    {
        CurrentEmotions = NPCMotus.GetCurrentEmotionStates();
        CurrentEmotionValues = NPCMotus.GetCurrentEmotionValues();
    }

    public void UpdateMood()
    {
        CurrentMood = NPCMotus.GetCurrentMood();
    }

    public void Reaction(e_EmotionsState p_TargetEmotion, float p_ReactionStrength)
    {
        NPCMotus.CreateSensation(p_TargetEmotion, p_ReactionStrength);
        UpdateEmotions();
        UpdateMood();
    }

    /// <summary>
    /// Sets the facial expression of the NPC.
    /// leave parameter blank to remove the equiped expression if one is present
    /// </summary>
    /// <param name="p_FaceName">Name of the facial expression to change to</param>
    public void SetFace(string p_FaceName = "")
    {
        if (Head.childCount >= 2)
        {
            foreach (Transform l_Child in Head)
            {
                if (l_Child.tag == "Face")
                    Destroy(l_Child.gameObject);
            }
        }

        if (p_FaceName == "")        
            return;
        

        GameObject l_NewFace = Instantiate(FaceManager.GetFace(SkinColour,p_FaceName));

        l_NewFace.transform.SetParent(Head);
        l_NewFace.transform.position = Head.position;
        l_NewFace.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
    }

    public void f_KingRescued(object sender, KingRescuedEventArgs e)
    {
        Reaction(e_EmotionsState.SURPRISE, 1.0f);
        Reaction(e_EmotionsState.JOY, 1.5f);
    }
}
