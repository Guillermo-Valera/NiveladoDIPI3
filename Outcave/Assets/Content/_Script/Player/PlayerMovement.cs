using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    
    [SerializeField]  private float speed;

    public bool isJumping;
    [SerializeField]  private float jumpForceValue =2f;
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
            }
            else
            {
                
                if (!onWall)
                {
                    onWall = true;
                    Flip();
                }

                if (onWall)
                {
                    rb.linearVelocity = new Vector2(0, rb.linearVelocityY / wallSlideFallOff);
                    Debug.Log(rb.linearVelocity/wallSlideFallOff);
                }

            }
        }

        else
        {

            rb.linearVelocity = new Vector2(speed* direction, rb.linearVelocityY);

        }
        
        if (Input.GetKey(KeyCode.Space) && (OnGround() || onWall) && !isJumping)
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
            isJumping = true;
        }


    }
    
    private bool OnLateralCollision()
    {
        
        RaycastHit2D hit;
        
        if (onWall)
        {

            hit = (Physics2D.BoxCast(
                transform.position,
                new Vector2(lateralRaycastLength, lateralRaycastLength),
                0,
                Vector2.right * -direction,0.3f,
                groundLayerMask
            ));

            // return (Physics2D.Raycast(
            //     transform.position,
            //     Vector2.right * -direction,
            //     lateralRaycastLength,
            //     groundLayerMask
            // ));
        }
        else
        {
            hit =Physics2D.BoxCast(
                transform.position,
                new Vector2(lateralRaycastLength, lateralRaycastLength),
                0,
                Vector2.right* direction,0.3f,
                groundLayerMask
            );
            
            // return (Physics2D.Raycast(
            //     transform.position,
            //     Vector2.right * direction,
            //     lateralRaycastLength,
            //     groundLayerMask
            // ));
        }

        if (hit)
        {
            isJumping = false;
            if (hit.collider.gameObject.GetComponentInParent<IGround>() != null)
            {

                hit.collider.gameObject.GetComponentInParent<IGround>().OnContact(this);
            }
        }

        return hit;
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
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, groundRaycastLength, groundLayerMask);
        if (hit)
        {
            isJumping = false;
        }
        return hit;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.GetComponentInParent<IGround>() != null)
        {
            other.collider.gameObject.GetComponentInParent<IGround>().OnContact(this);
        }
    }

    private void OnDrawGizmos()
    {
            //Debug.DrawRay(transform.position, Vector2.right * direction * lateralRaycastLength, Color.red);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(lateralRaycastLength, lateralRaycastLength));
            
            Debug.DrawRay(transform.position, -Vector2.up * groundRaycastLength, Color.azure);
    
    }
}