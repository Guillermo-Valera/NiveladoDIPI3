using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    
    [SerializeField]  private float speed;
    
    [SerializeField]  private float jumpForceValue;
    [SerializeField]  private float jumpForceValueMax;
    [SerializeField]  private float jumpForceMultiplier;
    
    [SerializeField]  int direction = 1;
    [SerializeField]  private LayerMask groundLayerMask;
    [SerializeField]  private float groundRaycastLength = 1f;

    [SerializeField] private bool onWall;
    [SerializeField] private float wallSlideFallOff = 1.75f;
    
    [SerializeField]  private float lateralRaycastLength = 0.8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (OnLateralCollision())
        {
            if (OnGround())
            {
                onWall = false;
                Flip();
                Debug.Log("Not flippin wall");
            }
            else
            {
                
                if (!onWall)
                {
                    onWall = true;
                    Flip();
                    Debug.Log("Flippin Wall");
                }

                if (onWall)
                {
                    rb.linearVelocity = new Vector2(0, rb.linearVelocityY / 2);
                }

            }
            
            Debug.Log(rb.linearVelocity);
        }

        else
        {
            rb.linearVelocity = new Vector2(speed* direction, rb.linearVelocityY);
        }
        
        if (Input.GetKey(KeyCode.Space) && (OnGround() || onWall))
        {
            jumpForceValue += Time.deltaTime * jumpForceMultiplier;
            if (OnGround())
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocityY/wallSlideFallOff);
            }

            
            if (jumpForceValue >= jumpForceValueMax)
            {
                jumpForceValue = jumpForceValueMax;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForceValue, ForceMode2D.Impulse);
            rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            jumpForceValue = 0;
            onWall = false;
        }


    }
    
    private bool OnLateralCollision()
    {
        Debug.Log("Colliding With Walls");
        if (onWall)
        {
            return (Physics2D.Raycast(
                transform.position,
                Vector2.right * -direction,
                lateralRaycastLength,
                groundLayerMask
            ));
        }
        else
        {
            return (Physics2D.Raycast(
                transform.position,
                Vector2.right * direction,
                lateralRaycastLength,
                groundLayerMask
            ));
        }

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
        return Physics2D.Raycast(transform.position, -transform.up, groundRaycastLength, groundLayerMask);
    }

    private void OnDrawGizmos()
    {
            Debug.DrawRay(transform.position, Vector2.right * direction * lateralRaycastLength, Color.red);
            
            Debug.DrawRay(transform.position, -Vector2.up * groundRaycastLength, Color.azure);
    
    }
}