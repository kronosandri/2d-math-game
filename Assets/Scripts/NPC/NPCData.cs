using UnityEngine;

[System.Serializable]
public class NPCData
{
    public string npcName;
    [TextArea(3, 10)] public string[] firstMeetingLines;
    [TextArea(3, 10)] public string[] repeatingLines;
    
    [Header("Formula Reward")]
    public bool givesFormula;
    public FormulaData formula; 
}