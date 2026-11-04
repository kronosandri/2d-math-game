using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    void Awake()
    {
        // This ensures the entire Manager suite persists
        DontDestroyOnLoad(gameObject);
    }
}