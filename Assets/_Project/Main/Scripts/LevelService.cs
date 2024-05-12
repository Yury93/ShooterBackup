using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.UI;

public class LevelService : MonoBehaviour
{
    public List<LevelButton> levels;
    public static LevelService instance;
    public ResourceSystem resourceSystem;
    public ConfirmPopup confirmPopup;
    public TextMeshProUGUI resourceText;
    public GameObject WeaponService;
    public Button nextButton;
    public SensivitySettings sensivitySettings;
    public Button settings;

    private void Awake()
    {
        instance = this;
        resourceSystem = new ResourceSystem();
        resourceSystem.Init();
        nextButton.onClick.AddListener(ClickNext);
    }

    private void ClickNext()
    {
       WeaponService.gameObject.SetActive(true);
        gameObject.SetActive(false);
        if(LevelButton.SelectScene.IsNullOrEmpty())
        {
            LevelButton.SelectScene = levels[0].nameLoaded;
        }
        Debug.Log(LevelButton.SelectScene);
    }

    void Start()
    {
        sensivitySettings.Init();
        settings.onClick.AddListener(sensivitySettings.Open);
        levels.ForEach(l => l.onClick += (l) => ClickLevel(l));
        levels[0].isSelect = true;
        levels.ForEach(l => l.ShowState());
        confirmPopup.Init();
        resourceText.text = "Очков: "+ resourceSystem.money.ToString();
    }

    public void ClickLevel(LevelButton button)
    {
        levels.ForEach(l =>
        {
            if (button.nameScene != l.nameScene && button.state == LevelButton.StateLevel.open)
            {
                l.isSelect = false;
            }
        });
            if(button.state == LevelButton.StateLevel.closed)
            {
                confirmPopup.OpenButtonLevel(button);
            }
            if (button.state == LevelButton.StateLevel.closed && button.IsBuyed == false && ResourceSystem.instance.HasMoney(button.cost) == false)
            {
                Debug.Log("NON RESOURCES");
            }
                levels.ForEach((l) => l.ShowState());
            button.ShowState();
        //});
        resourceText.text = "Очков: " + resourceSystem.money.ToString();
    }
}