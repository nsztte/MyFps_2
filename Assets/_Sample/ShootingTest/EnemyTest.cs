using MyFps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MySample
{
    public class EnemyTest : MonoBehaviour, IDamagable
    {
        #region Variables
        //체력
        [SerializeField] private float maxHealth = 20;  //초기값

        private float currentHealth;

        private bool isDeath = false; //중복 죽음 방지
        #endregion

        private void Start()
        {
            //초기화
            currentHealth = maxHealth;
            isDeath = false;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"currentHealth: {currentHealth}");

            //데미지 효과

            if (currentHealth <= 0 && !isDeath)
            {
                Die();
            }
        }

        void Die()
        {
            isDeath = true;

            //죽음처리

            //보상 - 경험치, 돈

            //효과

            Destroy(gameObject, 3f);
        }
    }
}