using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private TMP_Text _lifeCount;
    [SerializeField] private GameObject _deathMenu;

    private int _health;
    [SerializeField] private int _maxHealth;

    public bool isDead = false;

    public virtual void Start()
    {
        Restart();
    }

    public virtual void Restart()
    {
        _health = _maxHealth;
        isDead = false;
        if (_lifeCount)
        {
            _lifeCount.SetText(_health.ToString());
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            if (_lifeCount)
            {
                _lifeCount.SetText(_health.ToString());
            }
            if (_health <= 0)
            {
                _health = 0;
                if (_deathMenu)
                {
                    _deathMenu.SetActive(true);
                    isDead = true;
                }
            }
        }
    }
}
