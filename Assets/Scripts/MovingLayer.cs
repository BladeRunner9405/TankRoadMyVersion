using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MovingLayer _prevLayer;

    [SerializeField] private Transform _NatureSlot;
    [SerializeField] private Transform _interactiveSlot;


    public void SetSpeed(float speed)
    {
        _speed = speed;
        if (_prevLayer)
        {
            _prevLayer.SetSpeed(speed);
        }
    }

    public void SetPrevious(MovingLayer _layer)
    {
        _prevLayer = _layer;
    }
    private void Update()
    {
        transform.position -= new Vector3(0, 0, _speed / 60);
    }

    public Transform GetNatureSlot()
    {
        return _NatureSlot;
    }

    public Transform GetInteractiveSlot()
    {
        return _interactiveSlot;
    }
}
