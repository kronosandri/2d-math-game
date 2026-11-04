using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class NotebookManager : MonoBehaviour
{
    private static NotebookManager _instance;
    public static NotebookManager Instance 
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<NotebookManager>();
            }
            return _instance;
        }
    }

    [Header("UI Panels")]
    public GameObject formulaPanel;

    [Header("References")]
    public TextMeshProUGUI formulaText;
    public GameObject notificationIcon;

    [Header("Data")]
    public List<FormulaData> allFormulas = new List<FormulaData>();

    void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);

        if (formulaPanel != null) formulaPanel.SetActive(false);
    }

    void OnEnable() => GameEvents.OnFormulaUnlocked += UpdateFormulaUI;
    void OnDisable() => GameEvents.OnFormulaUnlocked -= UpdateFormulaUI;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.nKey.wasPressedThisFrame)
        {
            ToggleFormulaLibrary();
        }
    }

    public void ToggleFormulaLibrary()
    {
        if (formulaPanel == null) return;

        bool isActive = !formulaPanel.activeSelf;
        formulaPanel.SetActive(isActive);
        
        if (isActive)
        {
            UpdateFormulaUI();
            if (notificationIcon != null) notificationIcon.SetActive(false);
        }
    }

    public void UnlockExistingFormula(string title)
    {
        string cleanTargetTitle = title.Trim().ToLower();
        
        foreach (var formula in allFormulas)
        {
            if (formula == null) continue;

            string assetTitle = formula.formulaTitle.Trim().ToLower();
            
            if (assetTitle.Contains(cleanTargetTitle) || cleanTargetTitle.Contains(assetTitle))
            {
                ProgressManager.Instance?.UnlockFormula(formula.formulaTitle);
                
                UpdateFormulaUI();
                return;
            }
        }
        Debug.LogError($"[Notebook] FAILED! Could not find '{cleanTargetTitle}'.");
    }

    public void UpdateFormulaUI()
    {
        if (formulaText == null) return;

        formulaText.text = ""; 

        string displayContent = "<size=120%><color=#fa254c><b>MATHEMATICAL FORMULAS</b></color></size>\n________________________________\n\n";
        bool foundAny = false;

        foreach (var formula in allFormulas)
        {
            if (formula != null && (ProgressManager.Instance?.IsFormulaUnlocked(formula.formulaTitle) ?? false))
            {
                displayContent += $"<color=#2e2e2e><b>{formula.formulaTitle.ToUpper()}</b></color>\n";
                displayContent += $"<color=#fa254c><b>Formula: {formula.equationText}</b></color>\n";
                displayContent += $"{formula.description}\n\n____________________\n\n";
                foundAny = true;
            }
        }

        if (!foundAny) 
        {
            displayContent += "<i>Your notebook is currently empty...</i>";
        }

        formulaText.text = displayContent;
    }
}