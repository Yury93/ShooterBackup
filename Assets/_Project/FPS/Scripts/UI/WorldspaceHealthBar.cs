using System;
using Unity.FPS.AI;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class WorldspaceHealthBar : MonoBehaviour
    {
        [Tooltip("Health component to track")] public Health Health;

        [Tooltip("Image component displaying health left")]
        public Image HealthBarImage;

        [Tooltip("The floating healthbar pivot transform")]
        public Transform HealthBarPivot;

        [Tooltip("Whether the health bar is visible when at full health or not")]
        public bool HideFullHealthBar = true;
        EnemyController enemyController;
        private void Start()
        {
            enemyController = GetComponent<EnemyController>();
            
                enemyController.onDamaged += OnDamage;
           
            HealthBarImage.fillAmount = 1;
            if (HideFullHealthBar)
                HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);

            
        }

        private void OnDamage()
        {
           
                HealthBarImage.fillAmount = Health.CurrentHealth / Health.MaxHealth;
                if (HideFullHealthBar)
                    HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);
            
        }
        void Update()
        { 
            HealthBarPivot.LookAt(Camera.main.transform.position);
 
        }
    }
}