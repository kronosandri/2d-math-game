using UnityEngine;
using TMPro;

public class AnomalyCounterUI : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    private void Start()
    {
        // Set initial count on start
        UpdateCounter(ProgressManager.Instance.GetSaveData().resolvedAnomalies.Count);
    }

    private void OnEnable()
    {
        GameEvents.OnAnomalyResolved += UpdateCounter;
    }

    private void OnDisable()
    {
        GameEvents.OnAnomalyResolved -= UpdateCounter;
    }

    private void UpdateCounter(int count)
    {
        if (counterText != null)
            counterText.text = "Anomalies Solved: " + count;
    }
}