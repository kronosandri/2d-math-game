using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance 
{ 
    get 
    { 
        if (_instance == null) 
        {
            _instance = Object.FindAnyObjectByType<SaveManager>();
        }
        return _instance; 
    } 
}
    private string savePath;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
        savePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame(GameSaveData data) 
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public GameSaveData LoadGame()
    {
        if (!File.Exists(savePath)) return new GameSaveData();
        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<GameSaveData>(json);
    }
}