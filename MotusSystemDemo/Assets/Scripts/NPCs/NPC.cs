using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public abstract class NPC : MonoBehaviour
{
    public string Name;

    public Transform Head;

    public Motus NPCMotus;
    public string[] CurrentEmotions;
    public float[] CurrentEmotionValues;
    public string[] CurrentMood;

    public Animator NPCAnimator;
    

    // Use this for initialization
    protected void Start()
    {
        Name = gameObject.name;
        NPCMotus = new Motus();

        NPCMotus.SetAction(e_EmotionsState.JOY, e_EmotionsState.JOY, "Entry", delegate { SetFace("Happy3"); });
        NPCMotus.SetAction(e_EmotionsState.NEUTRAL, e_EmotionsState.NEUTRAL, "Entry", delegate { SetFace(); });
        NPCMotus.SetAction(e_EmotionsState.SADNESS, e_EmotionsState.SADNESS, "Entry", delegate { SetFace("Sad"); });


        UpdateEmotions();
        UpdateMood();

        NPCAnimator = GetComponentInChildren<Animator>();
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
        {
            return;
        }

        GameObject l_NewFace = Instantiate(FaceManager.GetFace(p_FaceName));

        l_NewFace.transform.SetParent(Head);
        l_NewFace.transform.position = Head.position;
        l_NewFace.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
    }
}
