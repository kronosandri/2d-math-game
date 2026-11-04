using UnityEngine;

public class MathAnomaly : Interactable
{
    public bool isPractice; 
    public string anomalyID;
    public string question;
    public string answer;

    void Start()
    {
        if (ProgressManager.Instance != null && ProgressManager.Instance.IsAnomalyResolved(anomalyID))
        {
            gameObject.SetActive(false);
        }
    }

    public override void Interact(PlayerInteract player)
    {
        if (isPractice)
        {
            var math = MathGenerator.GenerateProblem();
            MathManager.Instance?.ShowQuestion(math.problem, math.answer, null);
        }
        else
        {
            // Always pulls the latest data from the asset file
            MathManager.Instance?.ShowQuestion(question, answer, this);
        }
    }

    public void Resolve()
    {
        ProgressManager.Instance?.MarkAnomalyResolved(anomalyID);
        gameObject.SetActive(false);
    }
}