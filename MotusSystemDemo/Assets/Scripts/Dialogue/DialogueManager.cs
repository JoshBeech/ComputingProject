using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public List<string> Speech = new List<string>();
    public List<string> Options = new List<string>();

    public PlayerController ThePlayer;

    public int m_SpeechIndex;
    [SerializeField]
    private Text m_NPCName;
    [SerializeField]
    private Text m_NPCText;
    [SerializeField]
    private List<Button> WheelButtons1 = new List<Button>();
    [SerializeField]
    private List<Button> WheelButtons2 = new List<Button>();

    // Use this for initialization
    void Start()
    {
        m_SpeechIndex = 0;
        foreach(Button l_WheelOption in WheelButtons1)
        {
            l_WheelOption.gameObject.SetActive(false);
        }

        foreach (Button l_WheelOption in WheelButtons2)
        {
            l_WheelOption.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Open Dialogue box
    public void StartDialogue(GameObject p_NPC)
    {
        GetDialogue(p_NPC);
        SetupDialogue(p_NPC.GetComponent<NPCController>());
    }

    // Get dialogue from a given NPC
    public void GetDialogue(GameObject p_NPC)
    {
        NPCController l_NPCController = p_NPC.GetComponent<NPCController>();
        Speech = new List<string>(l_NPCController.TextLines.Count);
        Speech.AddRange(l_NPCController.TextLines);

        Options = new List<string>(l_NPCController.WheelOptions.Count);
        Options.AddRange(l_NPCController.WheelOptions);

        m_NPCName.text = l_NPCController.CharacterName + "\t\t" + l_NPCController.CurrentEmotions[0] + ":"
            + l_NPCController.CurrentEmotions[1] + ":" + l_NPCController.CurrentEmotions[2] + ":"
            + l_NPCController.CurrentEmotions[3];
    }

    // Fill in dialogue box/wheel with information
    public void SetupDialogue(NPCController p_NPCController)
    {
        // Set Text
        m_NPCText.text = Speech[m_SpeechIndex];

        // Split WheelOptions up, Set Wheel option text, Hook up buttons
        foreach (string l_WheelOption in Options)
        {
            string[] l_Breakdown = l_WheelOption.Split(':');
            Button l_WheelButton = WheelButtons1[Int32.Parse(l_Breakdown[0])];
            l_WheelButton.GetComponentInChildren<Text>().text = l_Breakdown[2];
            // Switch case for how to link the button - could use enum for future?
            int l_WheelAction = 0;
            if(Int32.TryParse(l_Breakdown[1], out l_WheelAction))
            {
                switch(l_WheelAction)
                {
                    case 1:
                        l_WheelButton.onClick.AddListener(delegate { ContinueDialogue(); });
                        break;
                    case 2:
                        l_WheelButton.onClick.AddListener(delegate { EndDialogue(); });
                        break;
                    case 4:
                        l_WheelButton.onClick.AddListener(delegate { p_NPCController.Reaction(MotusSystem.e_EmotionsState.JOY); });
                        break;
                    case 5:
                        l_WheelButton.onClick.AddListener(delegate { p_NPCController.Reaction(MotusSystem.e_EmotionsState.SADNESS); });
                        break;
                }
            }
            l_WheelButton.gameObject.SetActive(true);
        }

        // Show box
        transform.GetChild(0).gameObject.SetActive(true);

    }

    private void ChangeWheelOptionsLayer()
    {

    }

    public void ContinueDialogue()
    {
        if (m_SpeechIndex < Speech.Count)
        {
            m_SpeechIndex += 1;
            m_NPCText.text = Speech[m_SpeechIndex];
        }
    }

    // Empty dialogue box and switch back to overview camera
    public void EndDialogue()
    {
        m_SpeechIndex = 0;
        Speech.Clear();
        Speech.TrimExcess();
        Options.Clear();
        m_NPCName.text = "";
        m_NPCText.text = "";
        foreach(Button l_WheelButton in WheelButtons1)
        {
            l_WheelButton.onClick.RemoveAllListeners();
        }

        ThePlayer.SwapCamera();
        transform.GetChild(0).gameObject.SetActive(false);
    }


}
