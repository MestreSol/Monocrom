using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
    public People people;
    [TextArea(3,10)]
    public string[] sentences;
    public List<DialogueOptions> options = new List<DialogueOptions>();
}