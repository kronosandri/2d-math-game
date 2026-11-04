using System;

public static class GameEvents
{
    // Event for the anomaly counter
    public static Action<int> OnAnomalyResolved;

    // Event for formula unlocking
    public static Action OnFormulaUnlocked;

    // Helper method to trigger the formula event
    public static void TriggerFormulaUnlocked()
    {
        OnFormulaUnlocked?.Invoke();
    }
}