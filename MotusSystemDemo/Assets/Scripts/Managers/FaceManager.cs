using UnityEngine;
using System.Collections.Generic;

public class FaceManager : MonoBehaviour
{
    public static Dictionary<string, GameObject> WhiteFaces = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject> BrownFaces = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject> BlackFaces = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {
        Object[] l_Faces = Resources.LoadAll("Faces/White");

        foreach(Object l_Face in l_Faces)
            WhiteFaces.Add(l_Face.name, (GameObject)l_Face);
        

        l_Faces = Resources.LoadAll("Faces/Brown");

        foreach (Object l_Face in l_Faces)        
            BrownFaces.Add(l_Face.name, (GameObject)l_Face);
        

        l_Faces = Resources.LoadAll("Faces/Black");

        foreach (Object l_Face in l_Faces)        
            BlackFaces.Add(l_Face.name, (GameObject)l_Face);
        
    }

    public static GameObject GetFace(string p_SkinColour, string p_Face)
    {
        if (p_SkinColour.Equals("White"))        
            return WhiteFaces[p_Face];        
        else if (p_SkinColour.Equals("Brown"))
            return BrownFaces[p_Face];        
        else
            return BlackFaces[p_Face];
    }

    void OnDestroy()
    {
        WhiteFaces.Clear();
        BrownFaces.Clear();
        BlackFaces.Clear();
    }
}
