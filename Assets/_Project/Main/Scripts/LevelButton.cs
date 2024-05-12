using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public enum StateLevel
    {
        closed,
        open
    }
    public Image lockImage, selectImage;
    public int idButton;
    public StateLevel state;
    public string nameScene;
    public string nameLoaded;
    public Button button;
    public Outline outline;
    public bool isSelect;
    public static string SelectScene;
    public int cost;

    public Action<LevelButton> onClick;
    public const string KEY_SAVED = "LEVELBUTTON";
    public bool IsBuyed => PlayerPrefs.GetInt(KEY_SAVED + idButton) == idButton;
    public TextMeshProUGUI costText;

    private void Start()
    {
        button.onClick.AddListener(Click);
    }

    private void Click()
    {
        if(state == StateLevel.open)
        {
            SelectScene = nameLoaded;
            isSelect = true;
        }
        ShowState();
        onClick?.Invoke(this);
    }

    public void ShowState()
    {
       int savedKey = PlayerPrefs.GetInt(KEY_SAVED + idButton);

        if(savedKey == idButton)
        {
            state = StateLevel.open;
        }

        if (state == StateLevel.open)
        {
           lockImage.gameObject.SetActive(false); 
            if(isSelect)
            {
                selectImage.gameObject.SetActive(true);
                outline.effectColor = Color.green;
            }
            else
            {
                selectImage.gameObject.SetActive(false);
                outline.effectColor = Color.black;
            }
            costText.color = Color.black;
            costText.text = nameScene;
        }
        else
        {

            costText.text = cost.ToString();
            costText.color = Color.red;
            if ( ResourceSystem.instance.HasMoney( cost))
            {
                lockImage.gameObject.SetActive(false);
                outline.effectColor = Color.yellow;
                selectImage.gameObject.SetActive(false);
                costText.color = Color.green;
            }
            else
            {
                lockImage.gameObject.SetActive(true);
                selectImage.gameObject.SetActive(false);
                outline.effectColor = Color.black;
            }
        }
    }
    public void Buy()
    {
        PlayerPrefs.SetInt(KEY_SAVED + idButton, idButton);
        ResourceSystem.instance.TakeMoney(cost);
        SelectScene = nameLoaded;
        isSelect = true;
        state = StateLevel.open;
        ShowState();
        LevelService.instance.ClickLevel(this);
    }
}
