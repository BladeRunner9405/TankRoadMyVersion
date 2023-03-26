using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coins = 0;
    [SerializeField] private TMP_Text _coinsCount;
    [SerializeField] private TMP_Text _recordCoinsCount;

    [SerializeField] private TMP_Text _coinsCountDeathMenu;
    [SerializeField] private TMP_Text _recordCoinsCountDeathMenu;

    [SerializeField] private GameObject _newRecordTxt;

    private bool _checkedScore;

    private void Start()
    {

    }
    public void CollectCoint(int coins)
    {
        _coins += coins;
        _coinsCount.SetText(_coins.ToString());
        if (_checkedScore)
        {
            CheckRecord();
        }

    }

    public void Restart()
    {
        _checkedScore = false;
        _newRecordTxt.SetActive(false);
        _coins = 0;
        _coinsCount.SetText(_coins.ToString());
        if (!PlayerPrefs.HasKey("record_coins"))
        {
            PlayerPrefs.SetInt("record_coins", 0);
        }
        _recordCoinsCount.SetText(PlayerPrefs.GetInt("record_coins").ToString());
    }

    public void CheckRecord()
    {
        _checkedScore = true;
        int _record = PlayerPrefs.GetInt("record_coins");
        if (_coins > _record)
        {
            PlayerPrefs.SetInt("record_coins", _coins);
            _newRecordTxt.SetActive(true);
            _record = _coins;
        }
        _coinsCountDeathMenu.SetText(_coins.ToString());
        _recordCoinsCountDeathMenu.SetText(_record.ToString());
        
    }
}
