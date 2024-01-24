using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ModsButtonController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Mod Options")]
    [SerializeField] private ModsEnum mod;
    [SerializeField] private int index;
    [SerializeField] private int price;
    [SerializeField] private UnityEvent<ModsEnum, int> onModSelected;
    [SerializeField] private UnityEvent<ModsEnum, int> onModPreview;
    [SerializeField] private UnityEvent onModsRestore;
    [SerializeField] private GameObject selectedHighlight;

    [Header("Price")]
    [SerializeField] private GameObject pricePanel;
    [SerializeField] private TMPro.TextMeshProUGUI priceText;
    [SerializeField] private UnityEvent<int, Action, Action> onModBuy;
    [SerializeField] private UnityEvent<int> onModSell;
    private bool isUnlocked;
    private bool isSelected;

    private void Awake()
    {
        int defaultUnlock = index == 0 ? 1 : 0;
        isUnlocked = PlayerPrefs.GetInt(mod.ToString() + index, defaultUnlock) == 1;

        if (isUnlocked)
        {
            pricePanel.SetActive(false);
        }
        else
        {
            priceText.text = price.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicking");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            BuyMod();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SellMod();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onModPreview.Invoke(mod, index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onModsRestore.Invoke();
    }

    private void SelectMod()
    {
        Debug.Log("Selecting mod");
        onModSelected.Invoke(mod, index);
    }

    private void onEnoughMoney()
    {
        Debug.Log("Enough money");
        isUnlocked = true;
        pricePanel.SetActive(false);
        PlayerPrefs.SetInt(mod.ToString() + index, 1);
        SelectMod();
    }

    private void onNotEnoughMoney()
    {
        Debug.Log("Not enough money");
    }

    private void BuyMod()
    {
        if (isUnlocked)
        {
            SelectMod();
            return;
        }
        onModBuy.Invoke(price, () => onEnoughMoney(), () => onNotEnoughMoney());
    }

    private void SellMod()
    {
        if (!isUnlocked)
        {
            return;
        }
        if (isSelected)
        {
            return;
        }
        if (index == 0)
        {
            return;
        }
        isUnlocked = false;
        pricePanel.SetActive(true);
        priceText.text = price.ToString();
        PlayerPrefs.SetInt(mod.ToString() + index, 0);
        onModSell.Invoke(price / 2);
    }

    public void CheckIfSelected(ModsEnum mod, int index)
    {
        if (this.mod != mod)
        {
            return;
        }
        if (this.index == index)
        {
            isSelected = true;
            selectedHighlight.SetActive(true);
        }
        else
        {
            isSelected = false;
            selectedHighlight.SetActive(false);
        }
    }
}
