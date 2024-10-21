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
        #endregion

        private void Start()
        {
            //씬 페이드인 효과
            fader.FromFade();
        }

        public void NewGame()
        {
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