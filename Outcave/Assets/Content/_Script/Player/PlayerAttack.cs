using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float distance = 1.5f;
    [SerializeField] private Vector2 capsuleSize = new Vector2(1f, 2f);
    [SerializeField] private LayerMask enemyLayer; 
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector2 Dir = new Vector2(hor, ver).normalized;

        
        if (Dir.magnitude > 0)
        {
            PerformAttack(Dir);
        }
    }

    void PerformAttack(Vector2 direction)
    {
       
        RaycastHit2D hit = Physics2D.CapsuleCast(
            rb.position, 
            capsuleSize, 
            CapsuleDirection2D.Vertical, 
            0f, 
            direction, 
            distance, 
            enemyLayer
        );

        
        if (hit.collider != null)
        {
            Debug.Log("Hit" + hit.collider.name);
            
        }
    }
}