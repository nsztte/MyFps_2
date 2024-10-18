using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace MyFps
{
    public class PauseUI : MonoBehaviour
    {
        #region Variables
        public GameObject pauseUI;

        private GameObject thePlayer;
        #endregion

        private void Start()
        {
            //참조
            thePlayer = GameObject.Find("Player");
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            pauseUI.SetActive(!pauseUI.activeSelf);

            if(pauseUI.activeSelf)  //pause 창이 오픈 되었을때
            {
                thePlayer.GetComponent<FirstPersonController>().enabled = false;

                //마우스 커서 활성화
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Time.timeScale = 0f;
            }
            else    //pause 창이 닫힐때
            {
                thePlayer.GetComponent<FirstPersonController>().enabled = true;

                //마우스 커서 비활성화
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Time.timeScale = 1f;
            }
        }

        public void Menu()
        {
            Time.timeScale = 1f;
            Debug.Log("go to menu");
        }
    }
}