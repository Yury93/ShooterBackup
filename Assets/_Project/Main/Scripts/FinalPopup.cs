using System;
using TMPro; 
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class FinalPopup : MonoBehaviour
{
    public TextMeshProUGUI rewardText;
    public int reward;
    public Button adv,menu; 
    public bool isLoad;
    public static bool IsGameOver;
   public void Init()
    {
        menu.onClick.AddListener(LoadMain);
        adv.onClick.AddListener(ShowAdv);
        gameObject.SetActive(false);
          IsGameOver = false;
        YandexGame.RewardVideoEvent += OnRewardVideoEvent;
        YandexGame.OpenVideoEvent += DeactiveListener;
        YandexGame.CloseVideoEvent += ActiveListener;
    }

    private void ActiveListener()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;
    }

    private void DeactiveListener()
    {
       Camera.main.GetComponent<AudioListener>().enabled = false;
    }

    private void OnRewardVideoEvent(int obj)
    {
        reward = reward * 2;
        rewardText.text = reward.ToString();
    }

    private void ShowAdv()
    {
        adv.gameObject.SetActive(false);
       if(isLoad) { return; }
        YandexGame.RewVideoShow(2);
    }

    private void LoadMain()
    { if(isLoad) { return; }
        YandexGame.RewardVideoEvent -= OnRewardVideoEvent;
        adv.gameObject.SetActive(false);
        isLoad = true;
        ResourceSystem.instance.AddMoney(reward);
        YandexGame.savesData.enemyDied = 0;
        Saver.instance.Save();
        SceneManager.LoadScene("Main");
    }

     

    internal void Open()
    {
        IsGameOver = true;
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
       reward = YandexGame.savesData.enemyDied;
        rewardText.text = reward.ToString();
        if (reward == 0) adv.gameObject.SetActive(false); 
    }
    private void OnDestroy()
    {
        YandexGame.RewardVideoEvent -= OnRewardVideoEvent;
        YandexGame.OpenVideoEvent -= DeactiveListener;
        YandexGame.CloseVideoEvent -= ActiveListener;
    }
}
