using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] ShopController shopController;

    public void OnButtonClick()
    {
        SaveService.Instance.SaveData.money = playerMoney.GetMoney();
        SaveService.Instance.SaveData.unlockedWeaponIds = shopController.GetBoughtCards();
        SaveService.Instance.Save();
    }
}
