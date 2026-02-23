using UnityEngine;

public class ProyectileEnemy : MonoBehaviour
{
    [SerializeField] private GameObject proyectile;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRate;
    void Start()
    {
        
    }


    void Update()
    {
        spawnRate *= Time.deltaTime;
        if (Time.deltaTime >= spawnRate)
        {
            Instantiate(proyectile, spawnPoint.position, spawnPoint.rotation);
            spawnRate = 0f;
        }
    }
}
