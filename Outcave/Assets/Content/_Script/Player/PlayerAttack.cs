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

        if (new Vector2(hor, ver).magnitude > 0)
        {
            lastDirection = new Vector2(hor, ver).normalized;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        Vector3 spawnPos = transform.position + (Vector3)(lastDirection * attackDistance);
        GameObject attack = Instantiate(attackPrefab, spawnPos, Quaternion.identity);
        attack.transform.localScale = new Vector3(2f, 2f, 1f);
    }
}