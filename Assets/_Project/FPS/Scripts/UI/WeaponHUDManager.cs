using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

namespace Unity.FPS.UI
{
    public class WeaponHUDManager : MonoBehaviour
    {
        // public bool isMobile = true;
         public bool isMobile => Application.isMobilePlatform;
        [Tooltip("UI panel containing the layoutGroup for displaying weapon ammo")]
        public RectTransform AmmoPanel;

        [Tooltip("Prefab for displaying weapon ammo")]
        public GameObject AmmoCounterPrefab;

        PlayerWeaponsManager m_PlayerWeaponsManager;
        List<AmmoCounter> m_AmmoCounters = new List<AmmoCounter>();

         public List<GameObject> pcGUI;
        public List<GameObject> mobileGUI;
        public DynamicJoystick rotationJoystick;
        public Joystick moveJoystick;

        public static WeaponHUDManager instance;
        public EventButton jumpButton, shotButton,weaponFirst,weaponSecond;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {

   
            m_PlayerWeaponsManager = FindObjectOfType<PlayerWeaponsManager>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerWeaponsManager, WeaponHUDManager>(m_PlayerWeaponsManager,
                this);

            WeaponController activeWeapon = m_PlayerWeaponsManager.GetActiveWeapon();
            if (activeWeapon)
            {
                AddWeapon(activeWeapon, m_PlayerWeaponsManager.ActiveWeaponIndex);
                ChangeWeapon(activeWeapon);
            }

            m_PlayerWeaponsManager.OnAddedWeapon += AddWeapon;
            m_PlayerWeaponsManager.OnRemovedWeapon += RemoveWeapon;
            m_PlayerWeaponsManager.OnSwitchedToWeapon += ChangeWeapon;
            if(Application.isMobilePlatform || isMobile)
            EnableMobileGUI();
            else
                EnablePcGUI();
        }

        private void EnablePcGUI()
        {
            foreach (var item in pcGUI)
            {
                item.gameObject.SetActive(true);
            }
            foreach (var item in mobileGUI)
            {
                item.gameObject.SetActive(false);
            }
            if (FinalPopup.IsGameOver == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        private void EnableMobileGUI()
        {
            foreach (var item in pcGUI)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in mobileGUI)
            {
                item.gameObject.SetActive(true);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void AddWeapon(WeaponController newWeapon, int weaponIndex)
        {
            GameObject ammoCounterInstance = Instantiate(AmmoCounterPrefab, AmmoPanel);
            AmmoCounter newAmmoCounter = ammoCounterInstance.GetComponent<AmmoCounter>();
            DebugUtility.HandleErrorIfNullGetComponent<AmmoCounter, WeaponHUDManager>(newAmmoCounter, this,
                ammoCounterInstance.gameObject);

            newAmmoCounter.Initialize(newWeapon, weaponIndex);

            m_AmmoCounters.Add(newAmmoCounter);
        }

        void RemoveWeapon(WeaponController newWeapon, int weaponIndex)
        {
            int foundCounterIndex = -1;
            for (int i = 0; i < m_AmmoCounters.Count; i++)
            {
                if (m_AmmoCounters[i].WeaponCounterIndex == weaponIndex)
                {
                    foundCounterIndex = i;
                    Destroy(m_AmmoCounters[i].gameObject);
                }
            }

            if (foundCounterIndex >= 0)
            {
                m_AmmoCounters.RemoveAt(foundCounterIndex);
            }
        }

        void ChangeWeapon(WeaponController weapon)
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(AmmoPanel);
        }
        private void OnApplicationFocus(bool focus)
        {
            if(focus)
            {
                Camera.main.GetComponent<AudioListener>().enabled = true;
            }
            else
            {
                Camera.main.GetComponent<AudioListener>().enabled = false;
            }
        }
    }
}