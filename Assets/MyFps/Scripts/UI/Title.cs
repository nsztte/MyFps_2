using System.Collections;
using UnityEngine;

namespace MyFps
{
    public class Title : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainMenu";

        private bool isAnykey = false;
        public GameObject anykeyUI;
        #endregion

        private void Start()
        {
            //페이드인 효과
            fader.FromFade();
            //초기화
            isAnykey = false;

            StartCoroutine(TitleProcess());
        }


        private void Update()
        {
            if(Input.anyKey && isAnykey)
            {
                GotoMenu();
            }
        }

        //3초 뒤에 anykey show, 10초뒤에 자동 넘김
        IEnumerator TitleProcess()
        {
            yield return new WaitForSeconds(4f);
            isAnykey = true;
            anykeyUI.SetActive(true);

            yield return new WaitForSeconds(10f);
            GotoMenu();
        }

        private void GotoMenu()
        {
            StopAllCoroutines();

            fader.FadeTo(loadToScene);
        }
    }
}
