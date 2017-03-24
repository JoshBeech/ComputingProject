using UnityEngine;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    public string CharacterName;
    public List<string> TextLines = new List<string>();
    public List<string> WheelOptions = new List<string>();

    public MotusSystem.Motus MotusTest;
    // Use this for initialization
    void Start()
    {
        MotusTest = new MotusSystem.Motus();
        Debug.Log(MotusTest.GetCurrentEmotionState());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string CheckCurrentState()
    {
        return MotusTest.GetCurrentEmotionState();
    }
}
