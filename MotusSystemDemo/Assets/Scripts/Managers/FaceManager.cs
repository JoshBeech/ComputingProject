using UnityEngine;
using System.Collections.Generic;

public class FaceManager : MonoBehaviour
{
    public static Dictionary<string, GameObject> WhiteFaces = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {
        Object[] l_Faces = Resources.LoadAll("Faces/White");

        foreach(Object l_Face in l_Faces)
        {
            WhiteFaces.Add(l_Face.name, (GameObject)l_Face);
        }
    }

    public static GameObject GetFace(string p_Face)
    {
        return WhiteFaces[p_Face];
    }
}
