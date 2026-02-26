using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WindBlower : MonoBehaviour
{
    public enum Direction { Left, Right }
    [SerializeField] private Direction side = Direction.Right;
    
    [SerializeField] private float windStrength = 10f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private Vector2 capsuleSize = new Vector2(1f, 2f);
    [SerializeField] private Color windColor = new Color(0.5f, 0.8f, 1f, 0.3f);

    private LineRenderer lineRenderer;
    private Vector2 moveDir;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = capsuleSize.y; 
        lineRenderer.endWidth = capsuleSize.y;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.sortingOrder = 5;
        
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(windColor, 0f), new GradientColorKey(windColor, 1f) },
            new GradientAlphaKey[] { new GradientAlphaKey(windColor.a, 0f), new GradientAlphaKey(0f, 1f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    void FixedUpdate()
    {
        moveDir = (side == Direction.Right) ? Vector2.right : Vector2.left;

       
        RaycastHit2D[] hits = Physics2D.CapsuleCastAll(
            transform.position, 
            capsuleSize, 
            CapsuleDirection2D.Vertical, 
            0f, 
            moveDir, 
            distance
        );

        foreach (var hit in hits)
        {
       
            if (hit.transform == transform) continue;

          
            if (hit.collider.CompareTag("Player"))
            {
                Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(moveDir * windStrength, ForceMode2D.Force);
                }
            }
        }
    }

    void Update()
    {
        moveDir = (side == Direction.Right) ? Vector2.right : Vector2.left;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + (Vector3)moveDir * distance);
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmoDir = (side == Direction.Right) ? Vector2.right : Vector2.left;
        Gizmos.color = Color.cyan;
        Vector3 center = transform.position + (Vector3)gizmoDir * (distance / 2);
        Gizmos.DrawWireCube(center, new Vector3(distance, capsuleSize.y, 0.1f));
    }
}