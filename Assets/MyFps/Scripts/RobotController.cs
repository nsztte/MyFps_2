using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFps
{
    //로봇 상태
    public enum RobotState
    {
        R_Idle,
        R_Walk,
        R_Attack,
        R_Death
    }


    //로봇 Enemy 관리 클레스
    public class RobotController : MonoBehaviour
    {
        #region Variables
        public GameObject thePlayer;

        private Animator animator;

        //로봇 현재 상태
        private RobotState currentState;
        //로봇 이전 상태
        private RobotState beforeState;

        //체력
        [SerializeField] private float maxHealth = 20;  //초기값

        private float currentHealth;

        private bool isDeath = false; //중복 죽음 방지

        //이동
        [SerializeField] private float moveSpeed = 5f;   //속도

        //공격
        [SerializeField] private float attackRange = 1.5f;  //공격 가능 범위
        [SerializeField] private float attackDamage = 5f;   //공격 뎀지
        [SerializeField] private float attackTimer = 2f;    //공격 속도
        private float countdown = 0f;
        #endregion

        private void Start()
        {
            //참조
            animator = GetComponent<Animator>();
            //초기화
            currentHealth = maxHealth;
            isDeath = false;
            countdown = attackTimer; //시작시 2초 후 공격

            SetState(RobotState.R_Idle);
        }

        private void Update()
        {

            if (isDeath)
                return;

            //타겟 지정
            Vector3 dir = thePlayer.transform.position - transform.position;
            float distance = Vector3.Distance(thePlayer.transform.position, transform.position);

            //공격 상태로 전환
            if (distance <= attackRange)
            {
                SetState(RobotState.R_Attack);
            }

            //로봇 상태 구현
            switch (currentState)
            {
                case RobotState.R_Idle:
                    break;
                case RobotState.R_Walk:     //플레이어를 향해 걷기(이동)
                    transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);  //Translate 이용시 높이(y축)가 같아야 함
                    transform.LookAt(thePlayer.transform);
                    break;
                case RobotState.R_Attack:
                    if(distance > attackRange)
                    {
                        SetState(RobotState.R_Walk);
                    }
                    break;
                //case RobotState.R_Death:
                //    break;
            }
        }

        //2초마다 공격
        //private void AttackOnTimer()
        //{
        //    if (countdown < 0f)
        //    {
        //        //공격
        //        Attack();

        //        //초기화
        //        countdown = attackTimer;
        //    }
        //    countdown -= Time.deltaTime;
        //}

        private void Attack()
        {
            //플레이어
            PlayerController player = thePlayer.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
            }
        }

        //로봇의 상태 변경
        public void SetState(RobotState newState)
        {
            //현재 상태 체크
            if(currentState == newState)
                return;

            //이전 상태 저장
            beforeState = currentState;
            //상태 변경
            currentState = newState;

            //상태 변경에 따른 구현 내용
            animator.SetInteger("RobotState", (int)newState);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"Robot Remain Health: {currentHealth}");

            if(currentHealth <= 0 && !isDeath)
            {
                Die();
            }
        }

        private void Die()
        {
            isDeath = true;

            Debug.Log("로봇 죽음");
            SetState(RobotState.R_Death);

            //충돌체 제거
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}