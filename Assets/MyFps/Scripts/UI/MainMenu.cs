using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainScene01";

        private AudioManager audioManager;
        #endregion

        private void Start()
        {
            //씬 페이드인 효과
            fader.FromFade();

            //참조
            audioManager = AudioManager.Instance;

            //Bgm 플레이
            audioManager.Play("MenuBGM");
        }

        public void NewGame()
        {
            audioManager.Stop(audioManager.Bgmsound);
            audioManager.Play("MenuButton");

            fader.FadeTo(loadToScene);
        }

        public void LoadGame()
        {
            Debug.Log("LoadGame");
        }

        public void Options()
        {
            Debug.Log("Options");
        }

        public void Credits()
        {
            Debug.Log("Credits");
        }

        public void QuitGame()
        {
            Debug.Log("QuitGame");
            Application.Quit();
        }
    }
}