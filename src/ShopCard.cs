using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [SerializeField] private ShopController shopController;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject weaponStorage;
    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] private AttackHandler attackHandler;
    [SerializeField] private Image buttonImg;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private int cost;
    [SerializeField] private bool isBought = false;
    [SerializeField] private int id;
    
    public bool GetIsBought() => isBought;
    public bool SetIsBought(bool val) => isBought = val;
    public int GetId() => id;
    public void DisableAllWeapons()
    {
        foreach (Transform iter in weaponStorage.transform)
        {
            iter.gameObject.SetActive(false);
        }
    }

    public void ChangeColorToSelect()
    {
        buttonImg.color = Color.green;
    }
    private void BuyWeapon()
    {
        if (cost > playerMoney.GetMoney())
        {
            return;
        }
        
        playerMoney.ReduceMoney(cost);
        ChangeColorToSelect();
        buttonText.text = "Select";
        isBought = true;
    }

    private void SelectWeapon()
    {
        DisableAllWeapons();
        weapon.SetActive(true);
        attackHandler.UpdateWeapon();
        shopController.SetSelectColor();
        buttonImg.color = new Color32(74, 189, 104, 255);
    }

    public void OnButtonClick()
    {
        if (isBought)
        {
            SelectWeapon();
        }
        else
        {
            BuyWeapon();
        }
    }
    
    
}
