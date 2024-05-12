using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        SelectWeapons();
        firstWeapons.ForEach(w => w.onClick += OnClickWeapon);
        secondWeapons.ForEach(w => w.onClick += OnClickWeapon);
        firstWeapons.ForEach(w => w.onBuy += OnBuyWeapon);
        secondWeapons.ForEach(w => w.onBuy += OnBuyWeapon);
        playButton.onClick.AddListener(Play);
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
        moneyText.text = "Î÷êîâ: " + ResourceSystem.instance .money.ToString();
    }

    private void OnClickWeapon(WeaponButton button)
    {
        if(button.IsBuyed == false)
        {
            confirmPopup.OpenButtonWeapon(button);
        }
        else
        {
            if(button.typeWeapon == WeaponButton.TypeWeapon.first)
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
        }
    }

    private void SelectWeapons()
    {
        firstWeapon = firstWeapons.LastOrDefault(w => w.Selected);
        if (firstWeapon == null)
        {
            firstWeapons.ForEach(w => w.Deselect());
            firstWeapon = firstWeapons[0];

        }
        firstWeapon.Select();
        secondWeapon = secondWeapons.LastOrDefault(w => w.Selected);
        if (secondWeapon == null)
        {
            secondWeapons.ForEach(w => w.Deselect());
            secondWeapon = secondWeapons[0];
            secondWeapon.Select();
        }
        secondWeapon.Select();
    } 
}
