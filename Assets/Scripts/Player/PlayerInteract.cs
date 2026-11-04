using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Required for Coroutines

public class PlayerInteract : MonoBehaviour
{
    private Interactable currentInteractable;
    private bool interactionLocked;

    void Update()
    {
        if (GameStateManager.Instance == null) return;
        
        // If we are in Dialogue or Puzzle mode, don't allow new interactions
        if (!GameStateManager.Instance.CanMove()) return;

        if (Keyboard.current == null) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttemptInteraction();
        }
    }

    private void AttemptInteraction()
    {
        if (currentInteractable != null && !interactionLocked)
        {
            interactionLocked = true;
            currentInteractable.Interact(this);
        }
    }

    // Updated this to use a Cooldown
    public void UnlockInteraction()
    {
        StartCoroutine(InteractionCooldown());
    }

    private IEnumerator InteractionCooldown()
    {
        // Wait for a tiny fraction of a second
        // This prevents the "Enter" key from immediately triggering "E" logic
        yield return new WaitForSeconds(0.2f);
        interactionLocked = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            if (currentInteractable == interactable)
            {
                currentInteractable = null;
            }
        }
    }
}