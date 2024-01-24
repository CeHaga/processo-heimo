using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    private int money;
    private TMPro.TextMeshProUGUI moneyText;
    private void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        moneyText = GetComponent<TMPro.TextMeshProUGUI>();
        moneyText.text = money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
        PlayerPrefs.SetInt("Money", money);
    }
}
