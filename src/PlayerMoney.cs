using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int currentMoney = 0;
    void Start()
    {
        //currentMoney = 20;
        UpdateMoneyUI();
    }
    private void UpdateMoneyUI()
    {
        text.text = currentMoney.ToString();
    }
    public void AddMoney(int money)
    {   
        this.currentMoney += money;
        UpdateMoneyUI();
    }
    public void ReduceMoney(int money)
    {
        this.currentMoney -= money;
        UpdateMoneyUI();
    }
    public int GetMoney() => this.currentMoney;
}
