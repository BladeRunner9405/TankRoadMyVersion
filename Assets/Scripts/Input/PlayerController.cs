using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private PlayerInputActions _playerInput;
    [SerializeField] private Tank _tank;
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private bool _tutorialComlete;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.TankInput.Enable();

        _playerInput.TankInput.MoveLeft.performed += JumpLeft;
        _playerInput.TankInput.MoveRight.performed += JumpRight;

        _tank.Restart();
    }

    private void JumpLeft(InputAction.CallbackContext context)
    {
        _tank.TryJumpLeft();
        if (!_tutorialComlete)
        {
            _tutorialComlete = _tutorial.TriedLeft();
        }
        
    }

    private void JumpRight(InputAction.CallbackContext context)
    {
        _tank.TryJumpRight();
        if (!_tutorialComlete)
        {
            _tutorialComlete = _tutorial.TriedRight();
        }
    }
}
