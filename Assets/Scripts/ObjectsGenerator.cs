using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObjectsGenerator : MonoBehaviour
{
    [SerializeField] private Transform _layerHolder;

    [SerializeField] private GameObject[] _natureObjects;
    [SerializeField] private Transform[] _naturePositions;
    [SerializeField] private float _natureSpawnChance;

    [SerializeField] private GameObject[] _interactiveObjects;
    [SerializeField] private Transform[] _interactivePositions;
    [SerializeField] private float _interactiveSpawnChance;


    [SerializeField] private GameObject _layerPrefab;
    [SerializeField] private Transform _layerStartPosition;
    [SerializeField] private float _timeOut;
    [SerializeField] private float _layerSpeed;
    [SerializeField] private float _layerSpeedChange;
    [SerializeField] private float _layerSpeedChangeTimeOut;
    [SerializeField] private WheelsAnimation _playerWheels;


    private MovingLayer _currLayer;
    private MovingLayer _prevLayer;

    private int _badLineCount = 3;

    private bool _isGameOver = false;

    private void Awake()
    {
        Restart();
    }
    public void Restart()
    {
        _badLineCount = 15;
        foreach (Transform child in _layerHolder)
        {
            GameObject.Destroy(child.gameObject);
        }
        _layerSpeed = 3;
        _isGameOver = false;
        CreateNewLayer();
        StartCoroutine(GenerateRandomNature());
        StartCoroutine(GenerateRandomInteractive());
        StartCoroutine(ChangeSpeed());
    }

    public void CreateNewLayer()
    {
        _prevLayer = _currLayer;
        _currLayer = Instantiate(_layerPrefab, _layerStartPosition.position, Quaternion.identity, _layerHolder).GetComponent<MovingLayer>();

        _currLayer.SetPrevious(_prevLayer);
        _currLayer.SetSpeed(_layerSpeed);
    }


    private IEnumerator GenerateRandomNature()
    {
        if (!_isGameOver)
        {
            yield return new WaitForSeconds(_timeOut / _layerSpeed);
            if (_badLineCount > 0)
            {
                _badLineCount -= 1;
            }
            else {
                for (int i = 0; i < _naturePositions.Length; i++)
                {
                    if (Random.value > 1 - _natureSpawnChance)
                    {
                        Instantiate(_natureObjects[Random.Range(0, _natureObjects.Length)], _naturePositions[i].position, Quaternion.identity, _currLayer.GetNatureSlot());
                    }
                }
            }
            StartCoroutine(GenerateRandomNature());
        }
    }

    private IEnumerator GenerateRandomInteractive()
    {
        if (!_isGameOver)
        {
            yield return new WaitForSeconds(_timeOut / _layerSpeed);
            if (_badLineCount > 0)
            {
                
            }
            else
            {
                for (int i = 0; i < _interactivePositions.Length; i++)
                {
                    if (Random.value > 1 - _interactiveSpawnChance)
                    {
                        Instantiate(_interactiveObjects[Random.Range(0, _interactiveObjects.Length)], _interactivePositions[i].position, Quaternion.identity, _currLayer.GetInteractiveSlot());
                    }
                }
            }
            StartCoroutine(GenerateRandomInteractive());
        }
    }



    private IEnumerator ChangeSpeed()
    {
        if (!_isGameOver)
        {
            _layerSpeed += _layerSpeedChange;
            ResetLayersSpeed();
            yield return new WaitForSeconds(_layerSpeedChangeTimeOut);
            StartCoroutine(ChangeSpeed());
        }
    }

    private void ResetLayersSpeed()
    {
        _currLayer.SetSpeed(_layerSpeed);
        _playerWheels.SetSpeed(_layerSpeed);
    }

    public void SetSpeed(float speed)
    {
        _layerSpeed = speed;
    }

    public void Stop()
    {
        _currLayer.SetSpeed(0);
        _playerWheels.SetSpeed(0.1f);
        _isGameOver = true;
    }
}
