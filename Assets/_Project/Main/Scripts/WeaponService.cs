using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class WeaponService : MonoBehaviour
{
    public List<WeaponButton>  firstWeapons,secondWeapons;
    public WeaponButton firstWeapon, secondWeapon;
    public ConfirmPopup confirmPopup;
    public TextMeshProUGUI moneyText;
    public Button playButton;
    public TextMeshProUGUI startButtonText;
    public static WeaponService instance;
    public static int FirstId,SecondId;
    public bool isLoad;
    private void Awake()
    {
         instance = this; 

    }
    private void Start()
    {
        firstWeapons.ForEach(w=>w.Init());
        secondWeapons.ForEach(w => w.Init());

        firstWeapons.ForEach(w => w.onClick += OnClickWeapon);
        secondWeapons.ForEach(w => w.onClick += OnClickWeapon);
        firstWeapons.ForEach(w => w.onBuy += OnBuyWeapon);
        secondWeapons.ForEach(w => w.onBuy += OnBuyWeapon);
        playButton.onClick.AddListener(Play);
        StartCoroutine(CorSelectWeapon());
        IEnumerator CorSelectWeapon()
        {
            yield return new WaitForSeconds(0.1f);
            SelectWeapons();
        }
    }
     

    

    private void Play()
    {
        if (isLoad == false)
        {
            isLoad = true;
            StartCoroutine(CorStart());
            IEnumerator CorStart()
            {
                startButtonText.text = "ÇÀÃÐÓÇÊÀ...";
                FirstId = firstWeapon.idWeapon;
                SecondId = secondWeapon.idWeapon;
                yield return new WaitForSecondsRealtime(0.01F);
                 SceneManager.LoadScene(LevelButton.SelectScene);
                 
            }
        }
    }

    private void OnBuyWeapon(WeaponButton button)
    {
        if (button.typeWeapon == WeaponButton.TypeWeapon.first)
        {
            firstWeapons.ForEach(w => w.Deselect());
            FirstId = button.idWeapon;
            button.Select();
            firstWeapon = button;
        }
        if (button.typeWeapon == WeaponButton.TypeWeapon.second)
        {
            secondWeapons.ForEach(w => w.Deselect());
            SecondId = button.idWeapon;
            button.Select();
            secondWeapon = button;
        }
        moneyText.text = ResourceSystem.instance .money.ToString();

        foreach (var item in firstWeapons)
        {
            item.ShowState();
        }
        foreach (var item in secondWeapons)
        {
            item.ShowState();
        }
        Saver.instance.Save();
    }

    private void OnClickWeapon(WeaponButton button)
    {
        if(button.IsBuyed == false)
        {
            confirmPopup.OpenButtonWeapon(button);
        }
        else
        {
            
            if (button.typeWeapon == WeaponButton.TypeWeapon.first)
            {
                firstWeapons.ForEach(w => w.Deselect());
               
                button.Select();
           
                firstWeapon = button;
            }
            if (button.typeWeapon == WeaponButton.TypeWeapon.second)
            {
                secondWeapons.ForEach(w => w.Deselect());
                button.Select();
                secondWeapon = button;
            }
         
            Saver.instance.Save();
        }
    }

    private void SelectWeapons()
    {
        firstWeapon = firstWeapons.LastOrDefault(w => w.idWeapon == YandexGame.savesData.selectedFirstWeapon);
        if (firstWeapon == null)
        {
            firstWeapons.ForEach(w => w.Deselect());
            firstWeapon = firstWeapons[0];

        }
        firstWeapon.Select();
        secondWeapon = secondWeapons.LastOrDefault(w => w.idWeapon == YandexGame.savesData.selectedSecondWeapon);
        if (secondWeapon == null)
        {
            secondWeapons.ForEach(w => w.Deselect());
            secondWeapon = secondWeapons[0];
            
        }
        secondWeapon.Select();
      
        Saver.instance.Save();
 
    } 
}
