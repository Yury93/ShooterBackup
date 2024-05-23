using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Saver : MonoBehaviour
{
    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += Load;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= Load;
    public static Saver instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       
        // ��������� ���������� �� ������
        if (YandexGame.SDKEnabled == true)
        {
            // ���� ����������, �� ��������� ��� ����� ��� ��������
            Load();

            // ���� ������ ��� �� �����������, �� ����� �� ���������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }
    }

    // ��� ����� ��� ��������, ������� ����� ����������� � ������
    public void Load()
    {
        // �������� ������ �� ������� � ������ � ���� ��� �����
        // ��������, �� ����� �������� � ��������� UI.Text ������� � ������ �����:
        if (LevelService.instance)
        {
            LevelService.instance.resourceText.text = YandexGame.savesData.money.ToString();
            ResourceSystem.instance.money = YandexGame.savesData.money;
        }
    }

    // ��������, ��� ��� ����� ��� ����������
    public void Save()
    {
        // ���������� ������ � ������
        // ��������, �� ����� ��������� ���������� ����� ������:
        YandexGame.savesData.money = ResourceSystem.instance.money;
        Debug.Log(YandexGame.savesData.ValueSlider);
        //Debug.Log($"SAVES - first: {YandexGame.savesData.selectedFirstWeapon}/ second: {YandexGame.savesData.selectedSecondWeapon}");
        //Debug.Log($"SAVES - cur level: {YandexGame.savesData.selectedLevel}");
        //for (int i = 0; i < YandexGame.savesData.levels.Length; i++)
        //{
        //    Debug.Log($"Bought level: {YandexGame.savesData.levels[i]}");
        //}
        // ������ ������� ���������buyed ������
        YandexGame.SaveProgress();
    }
    [ContextMenu("ResetSaves")]
    public void ResetSaves()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
