using UnityEngine;

[CreateAssetMenu(fileName = "NewFormula", menuName = "GameData/Formula")]
public class FormulaData : ScriptableObject
{
    public string formulaTitle;       // Used by NotebookManager as title
    public string equationText;       // Used by NotebookManager (line 102)
    public string description;        // Used by NotebookManager (line 103)
}