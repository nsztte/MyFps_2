using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MyFps
{
    public class Intro : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainScene01";

        //이동
        public CinemachineDollyCart cart;
        [SerializeField] private float cartSpeed = 0.2f;

        private bool[] isArrive;
        [SerializeField] private int wayPointIndex = 0; //이동 목표지점 인덱스

        //연출
        public Animator cameraAnim;
        public GameObject introUI;
        public GameObject theShedLight;
        #endregion

        private void Start()
        {
            //초기화
            cart.m_Speed = 0f;
            wayPointIndex = 0;
            isArrive = new bool[6];

            StartCoroutine(StartIntro());
        }

        private void Update()
        {
            //도착판정
            if(cart.m_Position >= wayPointIndex && isArrive[wayPointIndex] == false)
            {
                //연출
                if(wayPointIndex == isArrive.Length - 1)
                {
                    //마지막 지점
                    StartCoroutine(EndIntro());
                }
                else
                {
                    StartCoroutine(StayIntro());
                }
            }

            //인트로 스킵 esc키 누르면 안트로 강제 종료하고 씬 이동
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GoToMainScene();
            }
        }

        IEnumerator StartIntro()
        {
            isArrive[wayPointIndex] = true;
            wayPointIndex++;

            fader.FromFade();

            AudioManager.Instance.PlayBgm("IntroBgm");

            yield return new WaitForSeconds(1f);

            //카메라 애니메이션
            cameraAnim.SetTrigger("AroundTrigger"); 

            yield return new WaitForSeconds(3f);
            //출발
            cart.m_Speed = cartSpeed;
        }

        IEnumerator StayIntro()
        {
            isArrive[wayPointIndex] = true;
            wayPointIndex++;

            cart.m_Speed = 0f;

            Debug.Log($"{wayPointIndex}번 지점 도착");
            yield return new WaitForSeconds(1f);

            //카메라 애니메이션
            cameraAnim.SetTrigger("AroundTrigger");

            int nowIndex = wayPointIndex - 1;   //현재 위치하고 있는 웨이포인트 인덱스
            switch (nowIndex)
            {
                case 1:
                    yield return new WaitForSeconds(3f);
                    introUI.SetActive(true);
                    break;
                case 2:
                    yield return new WaitForSeconds(3f);
                    introUI.SetActive(false);
                    break;
                case 3:
                    break;
            }

            if(nowIndex == 3)
            {
                theShedLight.SetActive(true);
                yield return new WaitForSeconds(1f);
            }

            //출발
            cart.m_Speed = cartSpeed;
        }

        //
        IEnumerator EndIntro()
        {
            isArrive[wayPointIndex] = true;
            cart.m_Speed = 0f;

            yield return new WaitForSeconds(3f);

            theShedLight.SetActive(false);
            AudioManager.Instance.StopBgm();
            fader.FadeTo(loadToScene);
        }

        private void GoToMainScene()
        {
            AudioManager.Instance.StopBgm();
            fader.FadeTo(loadToScene);
        }
    }
}