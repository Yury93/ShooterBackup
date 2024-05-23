using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ConfirmPopup : MonoBehaviour
{
    public Button buy, close; 
    public LevelButton levelButton;
    public WeaponButton weaponButton;
    public TextMeshProUGUI costText;
    public void Init()
    {
        close.onClick.AddListener(Close);
        buy.onClick.AddListener(Buy);
        gameObject.SetActive(false);
    }

    private void Buy()
    {
        BuyLevel();
        BuyWeapon();
    }

    private void BuyLevel()
    {
        if (levelButton != null && ResourceSystem.instance.HasMoney(levelButton.cost))
        { 
            if (levelButton.IsBuyed == false)
            {
                levelButton.Buy();
                Close();
            }
            else
            {
                Close();
            }
        }
    }
    public void OpenButtonWeapon(WeaponButton weaponButton)
    {

        if (ResourceSystem.instance.HasMoney(weaponButton.cost))
        {
            this.weaponButton = weaponButton;
            Open();
            costText.text = "ÖÅÍÀ: " + weaponButton.cost;
        }
    }
    private void BuyWeapon()
    {
        if (weaponButton != null && ResourceSystem.instance.HasMoney(weaponButton.cost))
        {
             
            if (weaponButton.IsBuyed == false)
            {
                weaponButton.Buy();
                Close();
            }
            else
            {
                Close();
            }
        }
     
    }
    [ContextMenu("Add money")]
    public void AddMoney()
    {
        ResourceSystem.instance.money = 900;
        Saver.instance.Save();
    }

    public void OpenButtonLevel(LevelButton levelButton)
    {  
        
        if(ResourceSystem.instance.HasMoney(levelButton.cost))
        {
            this.levelButton = levelButton;
            Open();
            costText.text = "ÖÅÍÀ: " + levelButton.cost;
        } 
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
        levelButton = null;
        weaponButton= null; 
    }
}
public class ResourceSystem
{
    public static ResourceSystem instance;
    public int money;
    public void Init()
    {
        instance = this;    
    }
    public  bool HasMoney(int cost)
    {
        if(money >= cost)
        {
            return true;
        }
        return false;
    }
    public void TakeMoney(int cost)
    {
        money -= cost;
    }
    public  void AddMoney(int cost)
    {
        money += cost;
    }
}

