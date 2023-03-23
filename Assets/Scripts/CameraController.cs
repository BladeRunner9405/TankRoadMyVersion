using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _offset;
    [SerializeField] private float _maxX;

    private float _offsetSmoothing;
    private Transform _currPosition;

    private void Start()
    {
        _currPosition = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {

        if (target)
        {
            float _newX = target.position.x;
            if (Mathf.Abs(_newX) > _maxX) {
                _newX = _maxX * Mathf.Sign(_newX);
            }
            Vector3 newPosition = new Vector3(_newX, _currPosition.position.y, _currPosition.position.z);

            _offsetSmoothing = _offset * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newPosition, _offsetSmoothing);
        }
    }
}
