using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 14f;
    public float crouchSpeed = 4f;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private SpriteRenderer sprite;
    private Animator anim; // Controlled below
    private float horizontalInput;

    private bool isGrounded;
    // REMOVED: Duplicate horizontalInput declaration from here

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        
        // FIX: Find the Animator component sitting on the child SpriteHolder object
        anim = GetComponentInChildren<Animator>();

        // Save original collider stats for crouching
        originalColliderSize = col.size;
        originalColliderOffset = col.offset;
    }

    void Update()
    {
        // 1. GLOBAL STATE CHECK
    bool isScratchpadOpen = FindAnyObjectByType<ScratchpadManager>()?.panel.activeSelf ?? false;

    if ((GameStateManager.Instance != null && !GameStateManager.Instance.CanMove()) || isScratchpadOpen)
    {
        horizontalInput = 0;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        UpdateAnimationState(false);
        return;
    }

        if (Keyboard.current == null) return;

        // 2. INPUT COLLECTION
        horizontalInput = 0;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1;

        // ANIMATION FIX: Tell the animator whether we are trying to move or stay still
        bool isMoving = Mathf.Abs(horizontalInput) > 0.1f;
        UpdateAnimationState(isMoving);

        // 3. JUMPING
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 4. CROUCHING LOGIC
        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            col.size = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
            col.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y * 0.25f));
        }
        else
        {
            col.size = originalColliderSize;
            col.offset = originalColliderOffset;
        }

        FlipSprite();
    }

    void FixedUpdate()
    {
        // 5. PHYSICS EXECUTION
        if (GameStateManager.Instance != null && !GameStateManager.Instance.CanMove())
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        float speed = Keyboard.current.leftCtrlKey.isPressed ? crouchSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (horizontalInput > 0) sprite.flipX = false;
        else if (horizontalInput < 0) sprite.flipX = true;
    }

    // Helper method to safely update the Animator parameter
    void UpdateAnimationState(bool moving)
    {
        if (anim != null)
        {
            anim.SetBool("isWalking", moving);
        }
    }

private void OnCollisionStay2D(Collision2D collision)
{
    // Check all contact points
    foreach (ContactPoint2D contact in collision.contacts)
    {
        // If the normal is pointing mostly UP (Y > 0.5), we are on the ground
        if (contact.normal.y > 0.5f)
        {
            isGrounded = true;
            return; // We found the ground, no need to keep checking
        }
    }
}

private void OnCollisionExit2D(Collision2D collision)
{
    // When we leave a collision, assume we are in the air 
    // (the next FixedUpdate or CollisionStay will correct it if we are still on floor)
    isGrounded = false;
}
}