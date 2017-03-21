using UnityEngine;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    public string CharacterName;
    public List<string> TextLines = new List<string>();
    public List<string> WheelOptions = new List<string>();

    // Use this for initialization
    void Start()
    {
        MotusSystem.TestClass HelloTest = new MotusSystem.TestClass();
        Debug.Log(HelloTest.SayHello());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
