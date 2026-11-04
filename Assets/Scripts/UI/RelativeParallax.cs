using UnityEngine;

public class RelativeParallax : MonoBehaviour
{
    public Transform player;

    // By adding [SerializeField], this becomes adjustable in the Inspector
    // even though the variable is private to this script.
    [SerializeField] [Range(-1f, 1f)] 
    private float parallaxMultiplier = 0.2f; 

    private Vector3 startPosition;
    private Vector3 startPlayerPosition;

    void Start()
    {
        startPosition = transform.position;
        startPlayerPosition = player.position;
    }

    void LateUpdate()
    {
        float movementX = (player.position.x - startPlayerPosition.x) * parallaxMultiplier;

        transform.position = new Vector3(startPosition.x + movementX, startPosition.y, transform.position.z);
    }
}