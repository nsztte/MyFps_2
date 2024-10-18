using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyFps
{
    public class DoorCellExit : Interactive
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainScene02";

        //action
        private Animator animator;
        private Collider m_Collider;
        public AudioSource audioSource;

        public AudioSource bgm01;
        #endregion

        private void Start()    //부모 클래스에서 Start가 있으면 실행 순서 고려해야됨
        {
            //참조
            animator = GetComponent<Animator>();
            m_Collider = GetComponent<BoxCollider>();
        }

        protected override void DoAction()
        {
            //문 여는 애니메이션
            animator.SetBool("IsOpen", true);
            m_Collider.enabled = false;

            //사운드 플레이
            audioSource.Play();

            ChangeScene();
        }

        void ChangeScene()
        {
            //씬 마무리 - bgm stop
            bgm01.Stop();

            //다음씬으로 이동
            fader.FadeTo(loadToScene);
        }
    }
}