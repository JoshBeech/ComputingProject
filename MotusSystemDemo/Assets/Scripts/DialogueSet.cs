using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DialogueSet
{
    public XmlDocument DialogueDocument = new XmlDocument();

    public List<string> TextLines = new List<string>();
    public List<string> WheelLayer1 = new List<string>();
    public List<string> WheelLayer2 = new List<string>();

    public DialogueSet(string p_DocumentPath)
    {
        DialogueDocument.Load(p_DocumentPath);

    }

    public void SetDialogue(string p_SetToLoad)
    {
      
    }
}

