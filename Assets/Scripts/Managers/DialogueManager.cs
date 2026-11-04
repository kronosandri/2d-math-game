using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance 
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<DialogueManager>();
            }
            return _instance;
        }
    }

    [Header("UI References")]
    public GameObject dialogueFrame;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [Header("Settings")]
    public float typingSpeed = 0.04f;

    private string[] lines;
    private int index;
    private bool isActive;
    private bool isTyping;

    private PlayerInteract player;
    private Coroutine typingCoroutine;

    void Awake()
    {   

        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);

        if (dialogueFrame != null) dialogueFrame.SetActive(false);
    }



    void Update()
    {
        if (!isActive) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EndDialogue();
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (isTyping) FinishLineInstantly();
            else AdvanceDialogue();
        }
    }

    public void StartDialogue(PlayerInteract p, string[] dialogue, string nName)
    {
        player = p;
        lines = dialogue;
        index = 0;
        isActive = true;

        if(nameText != null) nameText.text = nName; 
        if(dialogueFrame != null) dialogueFrame.SetActive(true);
        
        // Smart access to GameStateManager
        GameStateManager.Instance?.SetState(GameStateManager.GameState.Dialogue);

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(lines[index]));
    }

    private void AdvanceDialogue()
    {
        index++;
        if (index < lines.Length)
        {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeLine(lines[index]));
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void FinishLineInstantly()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialogueText.text = lines[index];
        isTyping = false;
    }

    public void EndDialogue()
    {
        isActive = false;
        isTyping = false;
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        if(dialogueFrame != null) dialogueFrame.SetActive(false);
        dialogueText.text = "";

        GameStateManager.Instance?.SetState(GameStateManager.GameState.Exploring);
        StartCoroutine(PostDialogueDelay());
    }

    private IEnumerator PostDialogueDelay()
    {
        yield return new WaitForEndOfFrame();
        if (player != null) player.UnlockInteraction();
        player = null;
    }
}