using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    
    [SerializeField] float timeForDestroying = 4f;
    [SerializeField] float timeForReconstructing = 10f;

    [SerializeField] bool isBreaking = false;
    
    [SerializeField] Collider2D _collider;
    
    [SerializeField] SpriteRenderer _spriteRenderer;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.transform.position.y + " / " + transform.position.y+0.5f);
        if (other.gameObject.CompareTag("Player") && other.transform.position.y >= transform.position.y+ 0.5f && !isBreaking)
        {
            Debug.Log("Falling platform");
            isBreaking = true;
            StartCoroutine(DestroyPlatform());
        }
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(timeForDestroying);
        isBreaking = false;
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        
        StartCoroutine(ReconstructPlatform());
    }

    private IEnumerator ReconstructPlatform()
    {
        yield return new WaitForSeconds(timeForReconstructing);
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
    }
    
    
}
