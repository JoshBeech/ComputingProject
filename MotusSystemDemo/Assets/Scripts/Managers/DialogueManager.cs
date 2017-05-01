using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public List<string> Speech = new List<string>();
    public List<string> Options = new List<string>();
    public List<string> Options2 = new List<string>();

    public PlayerController ThePlayer;

    public int m_SpeechIndex;
    [SerializeField]
    private Text m_NPCName;
    [SerializeField]
    private Text m_NPCText;
    [SerializeField]
    private List<Button> WheelButtons = new List<Button>();

    private int m_OptionLayer = 1;
    private NPC m_CurrentNPCClass;
    private ITalkable m_CurrentNPCInterface;

    // Use this for initialization
    void Start()
    {
        m_SpeechIndex = 0;
        foreach(Button l_WheelOption in WheelButtons)
        {
            l_WheelOption.gameObject.SetActive(false);
        }
    }

    // Open Dialogue box
    public void StartDialogue(NPC p_NPCType, ITalkable p_NPCInterface)
    {
        m_CurrentNPCClass = p_NPCType;
        m_CurrentNPCInterface = p_NPCInterface;
        GetDialogue();
        SetupDialogue();
    }

    // Get dialogue from a given NPC
    public void GetDialogue()
    {
        //IInteractable l_NPCInterface = p_NPC.GetComponent<ShopKeeper>();
        Speech = new List<string>(m_CurrentNPCInterface.ITextLines.Count);
        Speech.AddRange(m_CurrentNPCInterface.ITextLines);

        Options = new List<string>(m_CurrentNPCInterface.IWheelOptions1.Count);
        Options.AddRange(m_CurrentNPCInterface.IWheelOptions1);

        if(m_CurrentNPCInterface.IWheelOptions2.Count > 0)
        {
            Options2 = new List<string>(m_CurrentNPCInterface.IWheelOptions2.Count);
            Options2.AddRange(m_CurrentNPCInterface.IWheelOptions2);
        }

        RefreshTitle();
    }

    // Fill in dialogue box/wheel with information
    public void SetupDialogue()
    {
        // Set Text
        m_NPCText.text = Speech[m_SpeechIndex];

        SetupWheelOptions();

        // Show box
        transform.GetChild(0).gameObject.SetActive(true);

    }

    private void SetupWheelOptions()
    {
        List<string> l_OptionList = new List<string>();

        if (m_OptionLayer == 1)
            l_OptionList = new List<string>(Options);
        else if (m_OptionLayer == 2)
            l_OptionList = new List<string>(Options2);

        // Clear all listerns from buttons so new ones can be assigned
        foreach (Button l_WheelButton in WheelButtons)
        {
            l_WheelButton.onClick.RemoveAllListeners();
        }

        // Split WheelOptions up, Set Wheel option text, Hook up buttons
        foreach (string l_WheelOption in l_OptionList)
        {
            string[] l_Breakdown = l_WheelOption.Split(':');
            Button l_WheelButton = WheelButtons[Int32.Parse(l_Breakdown[0])];
            l_WheelButton.GetComponentInChildren<Text>().text = l_Breakdown[2];
            // Switch case for how to link the button - could use enum for future?
            int l_WheelAction = 0;
            if (Int32.TryParse(l_Breakdown[1], out l_WheelAction))
            {
                switch (l_WheelAction)
                {
                    case 1:
                        l_WheelButton.onClick.AddListener(delegate { ContinueDialogue(); });
                        break;
                    case 2:
                        l_WheelButton.onClick.AddListener(delegate { EndDialogue(); });
                        break;
                    case 3:
                        l_WheelButton.onClick.AddListener(delegate { ChangeWheelOptionsLayer(); });
                        break;
                    case 4:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.JOY, 0.3f);
                            RefreshTitle();
                        });
                        break;
                    case 5:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.SADNESS, -0.3f);
                            RefreshTitle();
                        });
                        break;
                    case 6:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.ANTICIPATION, 0.3f);
                            RefreshTitle();
                        });
                        break;
                    case 7:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.SURPRISE, -0.3f);
                            RefreshTitle();
                            m_CurrentNPCClass.SetFace("Surprised");
                        });
                        break;
                    case 8:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.ANGER, 0.3f);
                            RefreshTitle();
                            m_CurrentNPCClass.SetFace("Angry3");
                        });
                        break;
                    case 9:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.FEAR, -0.3f);
                            RefreshTitle();
                        });
                        break;
                    case 10:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.TRUST, 0.3f);
                            RefreshTitle();
                        });
                        break;
                    case 11:
                        l_WheelButton.onClick.AddListener(delegate {
                            m_CurrentNPCClass.Reaction(MotusSystem.e_EmotionsState.DISGUST, -0.3f);
                            RefreshTitle();
                        });
                        break;
                }
            }
            l_WheelButton.gameObject.SetActive(true);
        }
    }

    private void ChangeWheelOptionsLayer()
    {
        if (m_OptionLayer == 1)
            m_OptionLayer = 2;
        else if (m_OptionLayer == 2)
            m_OptionLayer = 1;

        SetupWheelOptions();
    }

    private void RefreshTitle()
    {
        m_NPCName.text = String.Format("{0}\t\t {1}:{2}:{3}", m_CurrentNPCClass.Name,
            m_CurrentNPCClass.CurrentMood[0], m_CurrentNPCClass.CurrentMood[1], m_CurrentNPCClass.CurrentMood[2]);
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
        foreach(Button l_WheelButton in WheelButtons)
        {
            l_WheelButton.onClick.RemoveAllListeners();
        }
        
        StartCoroutine(ThePlayer.SwapCamera());
        ThePlayer.CanMove = true;
        m_CurrentNPCInterface.LeaveDialogue();
        transform.GetChild(0).gameObject.SetActive(false);
    }


}
