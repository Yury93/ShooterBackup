using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SensivitySettings : MonoBehaviour
{
    public Slider slider;
    public static float ValueSlider;
    public Button close, save;

    public void Init(LevelService levelService)
    {
        close.onClick.AddListener(Close);
        save.onClick.AddListener(Save);
        //Debug.LogError("HAS SAVES SETTING?");
       
            levelService.StartCoroutine(CorStartSettings());
   
        IEnumerator CorStartSettings()
        {
            yield return new WaitForSeconds(0.1f);
            ValueSlider = YandexGame.savesData.ValueSlider;
            if (ValueSlider == -1)
            {
                slider.value = 0.5f;
                if (Application.isMobilePlatform)
                {
                    ValueSlider = slider.value * 10;
                }
                else
                {
                    ValueSlider = slider.value * 1000;
                }
                YandexGame.savesData.ValueSlider = ValueSlider;

                Saver.instance.Save();
            }
        }
         

    
        gameObject.SetActive(false);
    }

    public void Open()
    {
        if (Application.isMobilePlatform)
        {
            slider.value = ValueSlider / 10;
        }
        else
        {
            slider.value = ValueSlider / 1000;
        }
        gameObject.SetActive(true);
    }

    private void Save()
    {
        if (Application.isMobilePlatform)
        {
            ValueSlider = slider.value * 10;
        }
        else
        {
            ValueSlider = slider.value * 1000;
        }
        YandexGame.savesData.ValueSlider = ValueSlider;

        Saver.instance.Save();
        Close();
    }

    private void Close()
    {
        if (Application.isMobilePlatform)
        {
            slider.value = ValueSlider / 10;
        }
        else
        {
            slider.value = ValueSlider / 1000;
        }
        gameObject.SetActive(false);
    }
}
