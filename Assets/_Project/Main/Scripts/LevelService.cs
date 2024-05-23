
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;
using YG;

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
        if(string.IsNullOrEmpty(LevelButton.SelectScene))
        {
            LevelButton.SelectScene = levels[0].nameLoaded;
        }
        Debug.Log(LevelButton.SelectScene);
    }

    IEnumerator Start()
    {
        sensivitySettings.Init(this);
        settings.onClick.AddListener(sensivitySettings.Open);
        levels.ForEach(l => l.onClick += (l) => ClickLevel(l));

       
        levels.ForEach(l => l.ShowState());
        confirmPopup.Init();
        resourceText.text =   resourceSystem.money.ToString();

        yield return new WaitForSeconds(0.1f);
       var level = levels.FirstOrDefault(l=>l.idButton ==  YandexGame.savesData.selectedLevel);
        if(level != null) 
        LevelButton.SelectScene  = level.nameLoaded;
    }

    public void ClickLevel(LevelButton button)
    {
         
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
        resourceText.text =   resourceSystem.money.ToString();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            Camera.main.GetComponent<AudioListener>().enabled = false;
        }
    }
}