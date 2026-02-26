using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.15f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject); 
        }
    }
}