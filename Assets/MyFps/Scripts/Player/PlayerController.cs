using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFps
{
    public class PlayerController : MonoBehaviour, IDamagable
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string loadToScene = "GameOver";

        //체력
        [SerializeField] private float maxHealth = 20f;
        private float currentHealth;

        private bool isDeath = false;

        //데미지 효과
        public GameObject damageFlash;      //데미지 플래쉬 효과
        public AudioSource hurt01;          //데지미 사운드
        public AudioSource hurt02;
        public AudioSource hurt03;
        #endregion

        void Start ()
        {
            //초기화
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"플레이어 남은 체력: {currentHealth}");

            //데미지 효과
            StartCoroutine(DamageEffect());


            if (currentHealth <= 0 && !isDeath)
            { 
                Die();
            }
        }

        private void Die()
        {
            isDeath = true;
            fader.FadeTo(loadToScene);
        }

        IEnumerator DamageEffect()
        {
            damageFlash.SetActive(true);

            int randNumber = Random.Range(1, 4);
            if(randNumber == 1)
            {
                hurt01.Play();
            }
            else if(randNumber == 2)
            {
                hurt02.Play();
            }
            else
            {
                hurt03.Play();
            }

            yield return new WaitForSeconds(1f);
            damageFlash.SetActive(false);
        }
    }
}