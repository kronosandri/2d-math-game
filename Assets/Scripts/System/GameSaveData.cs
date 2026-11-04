using System;
using System.Collections.Generic;
[System.Serializable]
public class GameSaveData
{
    public List<string> resolvedAnomalies = new List<string>();
    public List<string> metNPCs = new List<string>();
    public List<string> unlockedFormulas = new List<string>();
    public string scratchpadText;
    public float playerX, playerY, playerZ;
}