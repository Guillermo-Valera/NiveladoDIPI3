using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public float fireRate = 2f; 
    public float projectileSpeed = 10f; 
    public bool shootRight = true; 
    public Transform firePoint;

    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab not assigned to EnemyShooter!");
            return;
        }

        
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

       
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
           
            if (shootRight)
            {
                projectileScript.direction = Vector2.right; 
            }
            else
            {
                projectileScript.direction = Vector2.left; 
            }

            projectileScript.speed = projectileSpeed; 
        }
      
    }
}