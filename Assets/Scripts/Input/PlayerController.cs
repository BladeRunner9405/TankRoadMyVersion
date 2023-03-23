using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private PlayerInputActions _playerInput;
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private Animator _tankAnimator;
    [SerializeField] private float _moveTime = 0.2f;
    [SerializeField] private float _cellSize;
    [SerializeField] private int _currX;
    [SerializeField] private int _currY;
    [SerializeField] private float _currWorldX;
    [SerializeField] private float _currWorldY;
    [SerializeField] private ObjectsGenerator _generator;

    [SerializeField] private int _maxX;
    [SerializeField] private int _maxY;

    [SerializeField] private TMP_Text _coinsCount;
    [SerializeField] private TMP_Text _lifeCount;
    [SerializeField] private GameObject _deathMenu;

    public bool _canJump = true;
    public bool _canMove = true;
    private int _coins = 0;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        StartCoroutine(GetObjectsCloser());
        if (!_canMove)
        {
            _canMove = true;
            _tankAnimator.SetTrigger("Restart");
        }
        _health = _maxHealth;
        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 0);
        }
        _coins = PlayerPrefs.GetInt("coins");
        _lifeCount.SetText(_health.ToString());
        _playerInput = new PlayerInputActions();
        _playerInput.TankInput.Enable();

        _playerInput.TankInput.MoveLeft.performed += TryJumpLeft;
        _playerInput.TankInput.MoveRight.performed += TryJumpRight;

        _currWorldX = _currX * _cellSize - 2 * _cellSize;
    }


    private IEnumerator GetObjectsCloser()
    {
        _generator.SetSpeed(93);
        yield return new WaitForSeconds(2);
        _generator.SetSpeed(3);
    }
    private IEnumerator UpdatePosition()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _moveTime)
        {
            float xInterpolantRot = _moveCurve.Evaluate(t);
            float new_xPos = Mathf.LerpUnclamped(_currWorldX, (_currX - 2) * _cellSize, xInterpolantRot);

            transform.position = new Vector3(new_xPos, 0, 0);
            yield return null;
        }
        _currWorldX = _currX * _cellSize - 2 * _cellSize;
        transform.position = new Vector3(_currWorldX, 0, 0);
        _canJump = true;
    }

    public void TryJumpRight(InputAction.CallbackContext context)
    {
        if (_canJump && _canMove)
        {
            if (_currX + 1 < _maxX)
            {
                _tankAnimator.SetTrigger("JumpRight");
                _currX += 1;
                _canJump = false;
                StartCoroutine(UpdatePosition());
            }
        }
    }

    public void TryJumpLeft(InputAction.CallbackContext context)
    {
        if (_canJump && _canMove)
        {
            if (_currX - 1 >= 0)
            {
                _tankAnimator.SetTrigger("JumpLeft");
                _currX -= 1;
                _canJump = false;
                StartCoroutine(UpdatePosition());
            }
        }
    }

    public void CollectCoint(int coins)
    {
        _coins += coins;
        _coinsCount.SetText(_coins.ToString());
        PlayerPrefs.SetInt("coins", _coins);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _lifeCount.SetText(_health.ToString());
        if (_health <= 0)
        {
            _tankAnimator.SetTrigger("Die");
            _generator.Stop();
            _canMove = false;
            _deathMenu.SetActive(true);
        }
    }
}
