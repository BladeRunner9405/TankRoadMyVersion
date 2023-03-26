using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObjectsGenerator : MonoBehaviour
{
    [SerializeField] private Tank _tank;
    [SerializeField] private WheelsAnimation _playerWheels;
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


    private MovingLayer _currLayer;
    private MovingLayer _prevLayer;
    private bool isGameOver = false;

    public float _currLayerSpeed;
    public float _currLayerSpeedChange;

    private void Awake()
    {
        Restart();
    }
    public void Restart()
    {
        foreach (Transform child in _layerHolder.transform)
        {
            Destroy(child.gameObject);
        }
        isGameOver = false;
        _currLayerSpeed = _layerSpeed;
        _currLayerSpeedChange = _layerSpeedChange;
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
        _currLayer.SetSpeed(_currLayerSpeed);
    }


    private IEnumerator GenerateRandomNature()
    {
        if (!isGameOver)
        {
            yield return new WaitForSeconds(_timeOut / _currLayerSpeed);
            GenerateNatureLayer();
            StartCoroutine(GenerateRandomNature());
        }
    }

    private IEnumerator GenerateRandomInteractive()
    {
        if (!isGameOver)
        {
            yield return new WaitForSeconds(_timeOut / _currLayerSpeed);
            GenerateInteractiveLayer();
            StartCoroutine(GenerateRandomInteractive());
        }
    }

    private void GenerateNatureLayer()
    {
        for (int i = 0; i < _naturePositions.Length; i++)
        {
            if (Random.value > 1 - _natureSpawnChance)
            {
                Instantiate(_natureObjects[Random.Range(0, _natureObjects.Length)], _naturePositions[i].position, Quaternion.identity, _currLayer.GetNatureSlot());
            }
        }
    }

    private void GenerateInteractiveLayer()
    {
        for (int i = 0; i < _interactivePositions.Length; i++)
        {
            if (Random.value > 1 - _interactiveSpawnChance)
            {
                Instantiate(_interactiveObjects[Random.Range(0, _interactiveObjects.Length)], _interactivePositions[i].position, Quaternion.identity, _currLayer.GetInteractiveSlot());
            }
        }
    }

    private IEnumerator ChangeSpeed()
    {
        yield return new WaitForSeconds(_layerSpeedChangeTimeOut);
        if (!isGameOver)
        {
            _playerWheels.SetSpeed(_currLayerSpeed);
            _currLayerSpeed += _currLayerSpeedChange;
        }
        else
        {
            _currLayerSpeed *= 0.75f;
            if (_currLayerSpeed < 2 && _currLayerSpeed != 0)
            {
                _currLayerSpeed = 0;
            }
        }
        ResetLayersSpeed();
        StartCoroutine(ChangeSpeed());
    }

    private void ResetLayersSpeed()
    {
        _currLayer.SetSpeed(_currLayerSpeed);
    }

    public void SetSpeed(float speed)
    {
        _currLayerSpeed = speed;
    }

    public void Stop()
    {
        isGameOver = true;
        _playerWheels.SetSpeed(0.1f);
    }
}
