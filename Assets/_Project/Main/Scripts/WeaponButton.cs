using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public int cost;
    public enum TypeWeapon{first,second}
    public enum StateButton { open, close }
    public int idWeapon;
    public const string KEY_SAVED = "WEAPON_BUTTON";
    public const string KEY_Selected = "WEAPON_selected";
    public StateButton state;
    public TypeWeapon typeWeapon;
    public bool IsSelect;
    public Button button;
    public Image toogleImage, lockImage;
    public Outline outline;
    public TextMeshProUGUI costText;

    public Action<WeaponButton> onClick;
    public Action<WeaponButton> onBuy;
    public bool IsBuyed => PlayerPrefs.GetInt(KEY_SAVED + typeWeapon + idWeapon) == idWeapon;
    public bool Selected => PlayerPrefs.GetInt(KEY_Selected + typeWeapon + idWeapon) == 5;
    public void Init()
    {
        button.onClick.AddListener(Click);
        
        ShowState();
    }

    private void Click()
    {
        onClick?.Invoke(this);
       
    }
    public void Select()
    {
        if(state == StateButton.open)
        { 
            IsSelect = true;
            PlayerPrefs.SetInt(KEY_Selected + typeWeapon + idWeapon, 5);
            ShowState();
        }
    }
    public void Deselect()
    { 
        IsSelect = false;
        PlayerPrefs.SetInt(KEY_Selected + typeWeapon + idWeapon, 0);
        ShowState();
    }
    public void ShowState()
    {
        toogleImage.gameObject.SetActive(false);
        if(IsBuyed)
        {
            state = StateButton.open;
        }
        if (state == StateButton.open)
        {
            lockImage.gameObject.SetActive(false);
            if (IsSelect == false)
                outline.effectColor = Color.black;
            else
            {
                outline.effectColor = Color.green;
                toogleImage.gameObject.SetActive(true);
            }
            costText.gameObject.SetActive(false);
        }
        else
        {

            lockImage.gameObject.SetActive(true);
            outline.effectColor = Color.black;
            costText.text = cost.ToString();
            costText.color = Color.red;
            if (ResourceSystem.instance.HasMoney(cost))
            {
                lockImage.gameObject.SetActive(false);
                outline.effectColor = Color.yellow;
                costText.color = Color.green;
            }

        }
    }

    public void Buy()
    {
        if(ResourceSystem.instance.HasMoney(cost))
        {
            state = StateButton.open;
            PlayerPrefs.SetInt(KEY_SAVED + typeWeapon + idWeapon, idWeapon);
            ResourceSystem.instance.TakeMoney(cost);
            onBuy?.Invoke(this);
        }
     
        ShowState();
    }
}
