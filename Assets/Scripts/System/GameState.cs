using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    // Singleton instance accessible from any script
    public static GameStateManager Instance { get; private set; }

    public enum GameState
    {
        Exploring,
        Dialogue,
        Puzzle,
        Paused
    }

    // Event for the Observer Pattern - other scripts can subscribe to this
    public static event Action<GameState> OnStateChanged;

    [Header("Current Status")]
    [SerializeField] private GameState _currentState = GameState.Exploring;

    // Public property to read the state 
    public GameState CurrentState => _currentState;

    void Start()
    {
        // Singleton Pattern Enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    /// <summary>
    /// Changes the game state and notifies all listeners.
    /// </summary>
    public void SetState(GameState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        
        // Trigger the event for any script listening (like UI or Camera)
        OnStateChanged?.Invoke(_currentState);
        
        Debug.Log($"<color=cyan>[GameStateManager]</color> State changed to: {newState}");
    }

    /// <summary>
    /// Returns true if the player is in free-roam mode.
    /// </summary>
    public bool CanMove()
    {
        return _currentState == GameState.Exploring;
    }

    /// <summary>
    /// Returns true if the game is currently in a menu, dialogue, or puzzle.
    /// </summary>
    public bool IsBusy()
    {
        return _currentState != GameState.Exploring;
    }
}