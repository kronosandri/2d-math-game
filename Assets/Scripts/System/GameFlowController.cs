using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro; // Required for TMP_Text

public class GameFlowController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject hudPanel;

    [Header("Game Objects")]
    [SerializeField] private GameObject player;

    [Header("Input Configuration")]
    [SerializeField] private InputActionAsset inputActions;
    
    [Header("Save Data References")]
    public List<string> myFormulaList = new List<string>(); 
    public TMPro.TMP_InputField myTextBox;
    
    private InputAction pauseAction;
    private bool isPaused = false;
    private bool isGameStarted = false;

    void Start()
    {
        startMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        hudPanel.SetActive(false);
        player.SetActive(false);
        Time.timeScale = 0f;

        if (inputActions != null)
        {
            var actionMap = inputActions.FindActionMap("Player");
            pauseAction = actionMap?.FindAction("Pause");
            
            if (pauseAction != null)
            {
                pauseAction.performed += OnPausePerformed;
                pauseAction.Enable();
            }
        }
    }

    private void OnDisable()
    {
        if (pauseAction != null)
        {
            pauseAction.performed -= OnPausePerformed;
            pauseAction.Disable();
        }
    }

private void OnPausePerformed(InputAction.CallbackContext context)
{
    // 1. Existing check
    if (!isGameStarted) return;

    // 2. NEW: Prevent pause if MathManager is open
    if (MathManager.Instance != null && MathManager.Instance.mathPanel.activeSelf)
    {
        Debug.Log("Cannot pause while solving an anomaly!");
        return; 
    }

    TogglePause();
}

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void StartGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (System.IO.File.Exists(path))
        {
            LoadGame();
        }
        else
        {
            Debug.Log("Starting fresh game...");
        }

        isGameStarted = true;
        startMenuPanel.SetActive(false);
        hudPanel.SetActive(true);
        player.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

public void SaveGame()
{
    // Capture all current in-memory progress
    if (myTextBox != null)
        ProgressManager.Instance.UpdateScratchpad(myTextBox.text);
    
    if (player != null)
        ProgressManager.Instance.SavePlayerPosition(player.transform.position);

    // Perform the write
    SaveManager.Instance.SaveGame(ProgressManager.Instance.GetSaveData());
    Debug.Log("Game Saved to Disk.");
}

public void LoadGame()
{
    // 1. Check if the file exists before doing anything
    string path = Application.persistentDataPath + "/savefile.json";
    if (!System.IO.File.Exists(path))
    {
        Debug.Log("No save file found. Starting fresh game.");
        return; // Exit early, let the game use default spawn values
    }

    // 2. Safely load the data
    GameSaveData loadedData = ProgressManager.Instance.GetSaveData();
    
    // 3. Add a null check for the player before moving
    if (player != null)
    {
        player.transform.position = ProgressManager.Instance.GetPlayerPosition();
        player.SetActive(true);
    }
    
    Debug.Log("Progress loaded!");
}

    public void DeleteSave()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Save file deleted.");
        }
    }
}