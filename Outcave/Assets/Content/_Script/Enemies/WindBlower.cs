using System.Collections;
using UnityEngine;

public class WindBlower : MonoBehaviour
{
    [SerializeField] private float distance = 1.5f;
    [SerializeField] private Vector2 capsuleSize = new Vector2(1f, 2f);
    [SerializeField] private LayerMask enemyLayer; 
    [SerializeField] private Vector2 direction = new Vector2(1f, 0f);
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PerformAttack();
    }
    
    void PerformAttack(Vector2 direction)
    {
       
        RaycastHit2D hit = Physics2D.CapsuleCast(
            transform.position, 
            capsuleSize, 
            CapsuleDirection2D.Vertical, 
            0f, 
            direction, 
            distance, 
            enemyLayer
        );

        
        if (hit.collider != null)
        {
            hit.rigidbody.AddForce(direction, ForceMode2D.Impulse);
            
        }
    }
}
