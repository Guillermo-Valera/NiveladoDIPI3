using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;
    public string playerTag = "Player"; 

    void Start()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * speed; 
        }
        else
        {
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(collision.gameObject);
           
        }

        
        Destroy(gameObject);
    }
}