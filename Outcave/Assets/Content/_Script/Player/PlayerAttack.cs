using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float attackDistance = 1.2f;
    
    private Vector2 lastDirection = Vector2.right;

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (new Vector2(hor, ver).sqrMagnitude > 0) 
        {
            lastDirection = new Vector2(hor, ver).normalized;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        
        Vector3 spawnPos = transform.position + (Vector3)(lastDirection * attackDistance);
        
      
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        
       
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

      
        GameObject attack = Instantiate(attackPrefab, spawnPos, rotation);
        
      
        attack.transform.localScale = new Vector3(2f, 1f, 1f);
    }
}