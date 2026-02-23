using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] private Transform proyectile;
    [SerializeField] private float speed = 2;
    
    void Start()
    {
        
    }

    void Update()
    {
        proyectile.Translate(Vector3.right * (speed *Time.deltaTime));
    }
}
