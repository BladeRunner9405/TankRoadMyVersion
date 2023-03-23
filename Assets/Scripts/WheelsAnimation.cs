using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsAnimation : MonoBehaviour
{
    [SerializeField] private Animator _wheelsAnimator;
    [SerializeField] private float _currSpeed;
    [SerializeField] private float _wheelRadius;

    private float _currAngleSpeed;

    private void Awake()
    {
        ResetAngleSpeed();
    }
    public void SetSpeed(float speed)
    {
        _currSpeed = speed;
        ResetAngleSpeed();
    }

    public void ResetAngleSpeed()
    {
        _currAngleSpeed = _currSpeed / _wheelRadius;
        ResetAnimatorSpeed();
    }

    private void ResetAnimatorSpeed()
    {
        _wheelsAnimator.speed = _currAngleSpeed;
    }
}
