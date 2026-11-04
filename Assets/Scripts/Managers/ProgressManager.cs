using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ProgressManager : MonoBehaviour
{
    private static ProgressManager _instance;
    public static ProgressManager Instance => _instance;
    private static bool isQuitting = false;
    private GameSaveData currentSave;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public GameSaveData GetSaveData()
    {
        if (isQuitting) return currentSave;
        
        if (currentSave == null)
        {
            if (SaveManager.Instance != null)
            {
                currentSave = SaveManager.Instance.LoadGame();
            }
            
            if (currentSave == null) currentSave = new GameSaveData();
        }
        return currentSave;
    }

    public bool HasMetNPC(string name) => GetSaveData().metNPCs.Contains(name.Trim().ToLower());
    public void MarkNPCMet(string name) 
    { 
        var data = GetSaveData(); 
        string n = name.Trim().ToLower(); 
        if(!data.metNPCs.Contains(n)) { data.metNPCs.Add(n); }
    }

    public void UnlockFormula(string title)
    {
        var data = GetSaveData();
        string t = title.Trim().ToLower();
        if (!data.unlockedFormulas.Contains(t))
        {
            data.unlockedFormulas.Add(t);
            GameEvents.TriggerFormulaUnlocked();
        }
    }
    public bool IsFormulaUnlocked(string title) => GetSaveData().unlockedFormulas.Contains(title.Trim().ToLower());

    public void UpdateScratchpad(string newText)
    {
        var data = GetSaveData();
        data.scratchpadText = newText;
       
    }
    public bool IsAnomalyResolved(string name) => GetSaveData().resolvedAnomalies.Contains(name);
    
    public void MarkAnomalyResolved(string anomalyName)
    {
        var data = GetSaveData();

        if (!data.resolvedAnomalies.Contains(anomalyName))
        {
            data.resolvedAnomalies.Add(anomalyName);

            
            GameEvents.OnAnomalyResolved?.Invoke(data.resolvedAnomalies.Count);
            Debug.Log($"Resolved: {anomalyName}. (In-Memory Only)");
        }
    }

    public void SavePlayerPosition(Vector3 position)
    {
        var data = GetSaveData();
        data.playerX = position.x;
        data.playerY = position.y;
        data.playerZ = position.z;

    }

    public Vector3 GetPlayerPosition()
    {
        var data = GetSaveData();
        Vector3 pos = new Vector3(data.playerX, data.playerY, data.playerZ);
        
        if (float.IsNaN(pos.x) || float.IsNaN(pos.y))
        {
            return new Vector3(0, 0, 0); 
        }
        return pos;
    }
}