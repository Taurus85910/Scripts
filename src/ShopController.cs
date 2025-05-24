using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private List<ShopCard> shopCards = new List<ShopCard>();
    private void Start()
    {
        foreach (Transform element in gameObject.transform)
        {
            if (element.gameObject.TryGetComponent<ShopCard>(out ShopCard shopCard))
            {
                shopCards.Add(shopCard);
            }
        }
    }
    public void SetSelectColor()
    {
        foreach (ShopCard shopCard in shopCards)
        {
            if (shopCard.GetIsBought())
            {
                shopCard.ChangeColorToSelect();
            }
        }
    }

    public void LoadCards(List<int> boughtCards)
    {

        foreach (ShopCard shopCard in shopCards)
        {
            if (boughtCards.Contains(shopCard.GetId()))
            {
                print(shopCard.GetId());
                shopCard.SetIsBought(true);
            }
        }
        
        SetSelectColor();
    }

    public List<int> GetBoughtCards()
    {
        List<int> boughtCards = new List<int>();
        foreach (ShopCard shopCard in shopCards)
        {
            if (shopCard.GetIsBought())
            {
                boughtCards.Add(shopCard.GetId());
            }
        }
        
        return boughtCards;
    }
}


