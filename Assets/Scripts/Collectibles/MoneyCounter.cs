using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;
    private int money;

    private void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        moneyText.text = money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
        PlayerPrefs.SetInt("Money", money);
    }

    public void RemoveMoney(int amount, Action onEnoughMoney, Action onNotEnoughMoney)
    {
        Debug.Log("Trying to remove " + amount + " from " + money);
        if (money < amount)
        {
            Debug.Log("Not enough money");
            onNotEnoughMoney.Invoke();
            return;
        }
        Debug.Log("Enough money");
        money -= amount;
        moneyText.text = money.ToString();
        PlayerPrefs.SetInt("Money", money);
        onEnoughMoney.Invoke();
    }

#if UNITY_EDITOR
    [EasyButtons.Button]
    public void Motherlode()
    {
        AddMoney(5);
    }
#endif
}
