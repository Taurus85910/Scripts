using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] ShopController shopController;
    void Start()
    {
        
        playerMoney.AddMoney(SaveService.Instance.SaveData.money);
        print("Off");
        shopController.LoadCards(SaveService.Instance.SaveData.unlockedWeaponIds);
    }

}
