using System;
using Unity.VisualScripting;
using UnityEngine;

public class VelcroData : MonoBehaviour, IGround
{
    PlayerMovement _playerMovement;
    private FixedJoint2D _fixedJoint2D;
    private bool _playerIsConnected;

    private void Start()
    {
        _fixedJoint2D = GetComponent<FixedJoint2D>();
    }

    private void Update()
    {
        if (_playerMovement != null)
        {
            if (_playerMovement.isJumping && _playerIsConnected)
            {
                Debug.Log("go");
                _playerIsConnected = false;
                _fixedJoint2D.connectedBody = null;
            }
        }
    }

    public void OnContact(PlayerMovement playerMovement)
    {
        Debug.Log(playerMovement.name);
        _playerMovement = playerMovement;
        _fixedJoint2D.connectedBody = playerMovement.gameObject.GetComponent<Rigidbody2D>();
        _playerIsConnected = true;
        _playerMovement.isJumping = false;

    }
}
