using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    // The panel we want to move (The MathPanel)
    [SerializeField] private RectTransform panelToMove;
    
    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        
        // If you don't drag the panel into the slot, it will move itself
        if (panelToMove == null)
            panelToMove = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Brings this panel to the very front of the UI layer
        panelToMove.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moves the panel based on mouse movement
        // We divide by scaleFactor so it moves correctly on different screen sizes
        panelToMove.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}