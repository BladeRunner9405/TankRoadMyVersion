using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : Damageable
{
    
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private Animator _tankAnimator;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private float _moveTime = 0.2f;
    [SerializeField] private float _cellSize;
    [SerializeField] private int _currX;
    [SerializeField] private int _currY;
    [SerializeField] private ObjectsGenerator _generator;
    [SerializeField] private int _maxX;
    [SerializeField] private int _maxY;

    private float _currWorldX;
    private float _currWorldY;
    public bool _canJump = true;
    public bool _canMove = true;

    public override void Start()
    {
        base.Start();
        Restart();
    }

    public override void Restart()
    {
        base.Restart();
        _wallet.Restart();
        if (!_canMove)
        {
            _canMove = true;
            _tankAnimator.SetTrigger("Restart");
        }
        _currWorldX = _currX * _cellSize - 2 * _cellSize;
    }

    public void TryJumpRight()
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

    public void TryJumpLeft()
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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (isDead && _canMove)
        {
            _tankAnimator.SetTrigger("Destroying");
            _generator.Stop();
            _canMove = false;
            _wallet.CheckRecord();
        }
    }
}
