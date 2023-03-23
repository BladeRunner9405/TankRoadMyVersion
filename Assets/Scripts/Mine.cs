using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _mine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController _player))
        {
            _explosion.SetActive(true);
            _mine.SetActive(false);
            _player.TakeDamage(1);
        }
    }
}
