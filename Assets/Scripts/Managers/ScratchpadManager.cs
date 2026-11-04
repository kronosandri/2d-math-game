using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ScratchpadManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject panel;

    public void LoadFromSave(string content)
    {
        if (inputField != null) inputField.text = content;
    }

    public string GetText() => inputField != null ? inputField.text : "";

    void Update()
    {
        // 1. Only allow opening/closing if NOT currently in Dialogue
        // Use your GameStateManager to keep the "one-state-at-a-time" rule
        if (GameStateManager.Instance != null && 
            GameStateManager.Instance.CurrentState == GameStateManager.GameState.Dialogue) 
            return;

        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            Toggle();
        }
    }

public void Toggle()
{
    if (panel == null) return;

    bool isActive = !panel.activeSelf;
    panel.SetActive(isActive);

    if (isActive)
    {
        GameStateManager.Instance?.SetState(GameStateManager.GameState.Paused);

        // 1. Clean the text
        inputField.text = inputField.text.TrimEnd();

        // 2. Set focus to the object first
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(inputField.gameObject);

        // 3. Set the caret position
        inputField.caretPosition = inputField.text.Length;
        inputField.selectionAnchorPosition = inputField.text.Length;
        inputField.selectionFocusPosition = inputField.text.Length;

        // 4. FORCE the refresh so the cursor appears immediately at the end
        inputField.ForceLabelUpdate();
        inputField.ActivateInputField(); 
    }
    else
    {
        inputField.text = inputField.text.TrimEnd();
        GameStateManager.Instance?.SetState(GameStateManager.GameState.Exploring);
    }
}
}