using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponButton : MonoBehaviour
{
    public int cost;
    public enum TypeWeapon{first,second}
    public enum StateButton { open, close }
    public int idWeapon;
    public StateButton state;
    public TypeWeapon typeWeapon;
    public bool IsSelect => Selected;
    public Button button;
    public Image toogleImage, lockImage;
    public Outline outline;
    public TextMeshProUGUI costText;

    public Action<WeaponButton> onClick;
    public Action<WeaponButton> onBuy;
    public bool IsBuyed
    {
        get
        {
            if (typeWeapon == TypeWeapon.first)
            {
                for (int i = 0; i < YandexGame.savesData.weaponsFirst.Length; i++)
                {
                    if (idWeapon == i && YandexGame.savesData.weaponsFirst[idWeapon] == -1)
                    {
                        return false; 
                    }
                }
            }
            if (typeWeapon == TypeWeapon.second)
            {
                for (int i = 0; i < YandexGame.savesData.weaponsSecond.Length; i++)
                {
                    if ( idWeapon  == i && YandexGame.savesData.weaponsSecond[idWeapon] == -1)
                    {
                        return false; 
                    }
                }
              
            }

            return true;
        }
    }
    public bool Selected
    {
        get
        {
            if (typeWeapon == TypeWeapon.first)
            {
                return YandexGame.savesData.selectedFirstWeapon == idWeapon;
            }
            else if (typeWeapon == TypeWeapon.first && idWeapon == 0 && YandexGame.savesData.selectedFirstWeapon == 0)
            {
                return true;
            }
            if (typeWeapon == TypeWeapon.second)
            {
               
                return YandexGame.savesData.selectedSecondWeapon == idWeapon;
                
            }
            else if (typeWeapon == TypeWeapon.second && idWeapon == 0 && YandexGame.savesData.selectedSecondWeapon == 0)
            {
                return true;
            }
            return false;
        }
    }
    Coroutine corShowState;
    public void Init()
    {
        button.onClick.AddListener(Click);
        
        ShowState();
    }

    private void Click()
    {
        onClick?.Invoke(this);
        ShowState();
    }
    public void Select()
    {
        if(state == StateButton.open)
        { 
            if (typeWeapon == TypeWeapon.first)
            {
                YandexGame.savesData.selectedFirstWeapon = idWeapon;
            }
            if (typeWeapon == TypeWeapon.second)
            {
                YandexGame.savesData.selectedSecondWeapon = idWeapon;
            } 
        }
        ShowState();
    }
    public void Deselect()
    { 
        if(typeWeapon == TypeWeapon.first)
        YandexGame.savesData.selectedFirstWeapon = 0;
        if (typeWeapon == TypeWeapon.second)
            YandexGame.savesData.selectedSecondWeapon = 0;
        ShowState();
    }
    public void ShowState()
    {
        if (corShowState != null) { StopCoroutine(corShowState); corShowState = null; }
        corShowState = StartCoroutine(CorShow());
        toogleImage.gameObject.SetActive(false);
        IEnumerator CorShow()
        {
            yield return new WaitForSeconds(0.01f);
            if (WeaponService.instance == null) yield break;
            if (IsBuyed)
            {
                state = StateButton.open;
            }
            if (state == StateButton.open)
            {
                lockImage.gameObject.SetActive(false);
                toogleImage.gameObject.SetActive(false);
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
       
    }

    public void Buy()
    {
        if(ResourceSystem.instance.HasMoney(cost))
        {
            state = StateButton.open;
            if(typeWeapon == TypeWeapon.first)
            YandexGame.savesData.weaponsFirst[idWeapon] = idWeapon;
            if (typeWeapon == TypeWeapon.second)
                YandexGame.savesData.weaponsSecond[idWeapon] = idWeapon;

            ResourceSystem.instance.TakeMoney(cost);
            onBuy?.Invoke(this);
        }
     
 
    }
}
