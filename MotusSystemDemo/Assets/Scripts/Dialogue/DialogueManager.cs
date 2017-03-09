using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public List<string> Speech = new List<string>();
    public List<string> Options = new List<string>();

    private int m_SpeechIndex;
    private Text m_NPCName;
    private Text m_NPCText;
    private List<Button> WheelOptions = new List<Button>();

    // Use this for initialization
    void Start()
    {
        m_SpeechIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Open Dialogue box
    public void StartDialogue(GameObject p_NPC)
    {
        GetDialogue(p_NPC);
        SetupDialogue();
    }

    // Get dialogue from a given NPC
    public void GetDialogue(GameObject p_NPC)
    {
        NPCController l_NPCController = p_NPC.GetComponent<NPCController>();
        Speech = new List<string>(l_NPCController.TextLines.Count);
        Speech.AddRange(l_NPCController.TextLines);

        Options = new List<string>(l_NPCController.WheelOptions.Count);
        Options.AddRange(l_NPCController.WheelOptions);

        m_NPCName.text = l_NPCController.CharacterName;
    }

    // Fill in dialogue box/wheel with information
    public void SetupDialogue()
    {
        // Set Text
        m_NPCText.text = Speech[m_SpeechIndex];
        // Split WheelOptions up 
        // Set Wheel option text
        // Hook up buttons
        foreach(string l_WheelOption in Options)
        {
            string[] l_Breakdown = l_WheelOption.Split(':');
            WheelOptions[Int32.Parse(l_Breakdown[0])].GetComponentInChildren<Text>().text = l_Breakdown[2];
            // Switch case for how to link the button
        }

        // Show box
    }

    public void ContinueDialogue()
    {
        m_SpeechIndex++;
        m_NPCText.text = Speech[m_SpeechIndex];
    }

    // Empty dialogue box and switch back to overview camera
    public void EndDialogue()
    {
        m_SpeechIndex = 0;
        Speech.Clear();
        Options.Clear();
    }
}
