using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class MathManager : MonoBehaviour
{
    private static MathManager _instance;
    public static MathManager Instance { get { return _instance; } }

    [Header("UI References")]
    public GameObject mathPanel;
    public TextMeshProUGUI problemText;
    public TMP_InputField inputField;
    public TextMeshProUGUI feedbackText;
    public GameObject blackOverlay;

    private string currentCorrectAnswer;
    private MathAnomaly currentAnomaly; // Null if it is a Practice mode

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);

        if (mathPanel != null) mathPanel.SetActive(false);
    }

    void Update()
    {
        // Close on ESC
        if (mathPanel != null && mathPanel.activeSelf && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ClosePanel();
        }
    }

    public void ClosePanel()
    {
        if (mathPanel != null) mathPanel.SetActive(false);
        if (blackOverlay != null) blackOverlay.SetActive(false);

        // NOTE: We no longer force cursor visibility off, 
        // allowing it to persist as the game state requires.
        PlayerInteract player = FindAnyObjectByType<PlayerInteract>();
        player?.UnlockInteraction();
    }

    public void ShowQuestion(string question, string answer, MathAnomaly anomaly)
    {
        currentAnomaly = anomaly;
        currentCorrectAnswer = answer;

        if (mathPanel != null) mathPanel.SetActive(true);
        if (blackOverlay != null) blackOverlay.SetActive(true);

        if (problemText != null) problemText.text = question;
        if (inputField != null)
        {
            inputField.text = ""; 
            inputField.ActivateInputField();
            inputField.Select();
        }
        if (feedbackText != null) feedbackText.text = ""; 
    }

public void CheckAnswer()
{
    if (inputField == null) return;
    
    // Clean the input and the answer
    string playerInputStr = inputField.text.Trim().ToLower();
    string correctAnsStr = currentCorrectAnswer.Trim().ToLower();

    // Direct string comparison is safer for algebra
    if (playerInputStr == correctAnsStr)
    {
        feedbackText.text = "Correct!";
        feedbackText.color = Color.green;
        StartCoroutine(SuccessSequence());
    }
    else
    {
        feedbackText.text = "Calculation Error.";
        feedbackText.color = Color.red;
        inputField.text = "";
        inputField.ActivateInputField();
    }
}

    private IEnumerator SuccessSequence()
    {
        yield return new WaitForSeconds(0.5f);
        
        if (currentAnomaly != null)
        {
            // STORY MODE: Resolve and Close
            currentAnomaly.Resolve(); 
            ClosePanel();
        }
                else
        {
            // PRACTICE MODE: Call the new generator
            var nextMath = MathGenerator.GenerateProblem();
            ShowQuestion(nextMath.problem, nextMath.answer, null);
        }

    }
}