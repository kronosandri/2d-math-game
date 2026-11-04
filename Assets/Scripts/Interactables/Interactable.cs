using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // By passing 'player' here, we avoid using FindFirstObjectByType later
    public abstract void Interact(PlayerInteract player);
}
