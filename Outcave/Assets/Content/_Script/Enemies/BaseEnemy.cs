using UnityEngine;

public class BaseEnemy: MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private Transform targetPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        targetPoint = pointB;
    }

    void Update()
    {
        MoveTowardsTarget();
        CheckDistance();
    }

    void MoveTowardsTarget()
    {
        
        float direction = (targetPoint.position.x > transform.position.x) ? 1 : -1;
        
        
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        
        if (direction > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void CheckDistance()
    {
        
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
    }

    
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);
        }
    }
}