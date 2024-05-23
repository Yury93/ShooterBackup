using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Saver : MonoBehaviour
{
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += Load;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= Load;
    public static Saver instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то выполняем Ваш метод для загрузки
            Load();

            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    // Ваш метод для загрузки, который будет запускаться в старте
    public void Load()
    {
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        if (LevelService.instance)
        {
            LevelService.instance.resourceText.text = YandexGame.savesData.money.ToString();
            ResourceSystem.instance.money = YandexGame.savesData.money;
        }
    }

    // Допустим, это Ваш метод для сохранения
    public void Save()
    {
        // Записываем данные в плагин
        // Например, мы хотил сохранить количество монет игрока:
        YandexGame.savesData.money = ResourceSystem.instance.money;
        Debug.Log(YandexGame.savesData.ValueSlider);
        //Debug.Log($"SAVES - first: {YandexGame.savesData.selectedFirstWeapon}/ second: {YandexGame.savesData.selectedSecondWeapon}");
        //Debug.Log($"SAVES - cur level: {YandexGame.savesData.selectedLevel}");
        //for (int i = 0; i < YandexGame.savesData.levels.Length; i++)
        //{
        //    Debug.Log($"Bought level: {YandexGame.savesData.levels[i]}");
        //}
        // Теперь остаётся сохранитьbuyed данные
        YandexGame.SaveProgress();
    }
    [ContextMenu("ResetSaves")]
    public void ResetSaves()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
