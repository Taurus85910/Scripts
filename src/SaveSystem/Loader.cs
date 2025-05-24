using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] ShopController shopController;
    [SerializeField] GameObject shopPanel;
    void Start()
    {
        shopPanel.SetActive(true);
        
        playerMoney.AddMoney(
            SaveService.Instance.SaveData.money);
        //print("Off");
        shopController.LoadCards(SaveService.
            Instance.SaveData.unlockedWeaponIds);
        shopPanel.SetActive(false);
    }

}
