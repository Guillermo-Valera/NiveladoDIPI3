using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    
    [SerializeField]  private float speed;
    
    [SerializeField]  private float jumpForceValue;
    [SerializeField]  private float jumpForceMultiplier;
    
    int direction = 1;
    [SerializeField]  private LayerMask groundLayerMask;
    [SerializeField]  private float groundRaycastLength = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed* direction, rb.linearVelocityY);

        if (OnLateralCollision())
        {
            Flip();
            
            Debug.Log(direction + " " + speed);
        }
        
        if (Input.GetKey(KeyCode.Space) && OnGround())
        {
            jumpForceValue += Time.deltaTime * jumpForceMultiplier;
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            
            if (jumpForceValue >= 25)
            {
                jumpForceValue = 25;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && OnGround())
        {
            rb.AddForce(transform.up * jumpForceValue, ForceMode2D.Impulse);
            rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            jumpForceValue = 0;
        }


    }
    
    private bool OnLateralCollision()
    {
        return (Physics2D.Raycast(
            transform.position,
            Vector2.right * direction,
            0.6f,
            groundLayerMask
        ));
    }
    
    void Flip()
    {
        direction = -direction;
        
        // Girar visualmente el sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    private bool OnGround()
    {
        return Physics2D.Raycast(transform.position, -transform.up, 1f, groundLayerMask);
    }

    private void OnDrawGizmos()
    {
    
            // Establece el color del Gizmo
            Gizmos.color = Color.azure;
    
            // Dibuja la línea desde la posición actual hasta el objetivo
            Debug.DrawRay(transform.position, Vector2.right * direction * 1, Color.red);
    
    }
}