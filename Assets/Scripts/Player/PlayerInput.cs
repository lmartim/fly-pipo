using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;

    private bool _isMoving;
    private float _touchDirection;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        _playerController.Moving(Input.GetAxisRaw("Horizontal"));

        if (Input.GetButtonDown("Vertical")) _playerController.Fly();
        if (Input.GetButtonDown("Jump")) StartCoroutine(_playerController.Action());

        if (_isMoving)
        {
            _playerController.Moving(_touchDirection);
        }
    }

    public void TouchIsMoving(bool isMoving)
    {
        _isMoving = isMoving;
    }

    public void TouchDirection(float direction)
    {
        _touchDirection = direction;
    }
}
