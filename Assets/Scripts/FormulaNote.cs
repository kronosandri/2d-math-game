using UnityEngine;

public class FormulaNote : Interactable
{
    [Header("Formula Setup")]
    // Type the exact name of the formula here (e.g., "Pythagorean Theorem")
    public string formulaTitle; 

    public override void Interact(PlayerInteract player)
    {
        // Unlock it in the background cleanly!
        NotebookManager.Instance.UnlockExistingFormula(formulaTitle);
        
        // (Optional) Add your notification or destruction logic here:
        Destroy(gameObject); 
    }
}