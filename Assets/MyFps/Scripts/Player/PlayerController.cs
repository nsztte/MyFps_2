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

        //무기
        public GameObject realPistol;
        #endregion

        void Start ()
        {
            //초기화
            currentHealth = maxHealth;

            //무기획득
            if(PlayerStats.Instance.HasGun)
            {
                realPistol.SetActive(true);
            }
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

            //메인씬01에서 총 소지시 총 비활성화
            if(SceneManager.GetActiveScene().name == "MainScene01" && PlayerStats.Instance.HasGun)
            {
                PlayerStats.Instance.SetHasGun(false);
            }

            //게임오버 씬으로 이동
            fader.FadeTo(loadToScene);
        }

        IEnumerator DamageEffect()
        {
            damageFlash.SetActive(true);
            CinemachineShake.Instance.ShakeCamera(1f, 1f);

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