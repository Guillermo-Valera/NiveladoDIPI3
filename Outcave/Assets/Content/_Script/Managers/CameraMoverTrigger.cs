using System;
using UnityEngine;

public class CameraMoverTrigger : MonoBehaviour
{
    public Transform currentCameraTargetPosition;
    public float currentCameraTargetSize;

    public Transform previousCameraTargetPosition;
    public float previousCameraTargetSize;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if (other.transform.position.y >= transform.position.y)
        {
            if (Camera.main == null) return;
            Camera.main.transform.position = new Vector3(currentCameraTargetPosition.position.x,
                currentCameraTargetPosition.position.y, currentCameraTargetPosition.position.z);
                
            Camera.main.orthographicSize = currentCameraTargetSize;

        }
        else
        {
            if (Camera.main == null) return;
            Camera.main.transform.position = new Vector3(previousCameraTargetPosition.position.x,
                previousCameraTargetPosition.position.y, previousCameraTargetPosition.position.z);
                
            Camera.main.orthographicSize = previousCameraTargetSize;

        }
    }
}
