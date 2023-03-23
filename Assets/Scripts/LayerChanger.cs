using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    [SerializeField] private ObjectsGenerator _objectsGenerator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovingLayer>(out MovingLayer _layer))
        {
            _objectsGenerator.CreateNewLayer();
        }
    }
}
