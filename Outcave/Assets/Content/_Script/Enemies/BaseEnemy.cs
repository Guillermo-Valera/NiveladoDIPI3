using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    int direction = 1;
    [SerializeField]  private float speed;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(speed* direction, rb.linearVelocityY);
        if (OnLateralCollision())
        {
            Flip();
            
            Debug.Log(direction + " " + speed);
        }
        
    }
    
    private bool OnLateralCollision()
    {
        return (Physics2D.Raycast(
            transform.position,
            Vector2.right * direction,
            0.6f
           
            
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
}
