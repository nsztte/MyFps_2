using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MyFps {
    public class FullExitEye : Interactive
    {
        #region Variabels
        public GameObject emptyPicture;
        public GameObject fullPicture;

        public Animator exitAnimator;
        public GameObject exitTrigger;

        public TextMeshProUGUI textBox;
        [SerializeField] private string puzzleStr = "You need more eye pictures";
        #endregion

        protected override void DoAction()
        {
            if (PlayerStats.Instance.HasPuzzleItem(PuzzleKey.LEFTEYE_KEY) && PlayerStats.Instance.HasPuzzleItem(PuzzleKey.RIGHTEYE_KEY))
            {
                StartCoroutine(OpenExitWall());
            }
            else
            {
                StartCoroutine(LockedExitWall());
            }
        }

        IEnumerator OpenExitWall()
        {
            //완성본 그림 보이기
            emptyPicture.SetActive(false);
            fullPicture.SetActive(true);

            //출구 열기
            exitAnimator.SetBool("IsOpen", true);
            yield return new WaitForSeconds(0.5f);

            //Exit Trigger 활성화
            exitTrigger.SetActive(true);
        }

        IEnumerator LockedExitWall()
        {
            //메세지 출력
            unInteractive = true;

            textBox.gameObject.SetActive(true);
            textBox.text = puzzleStr;

            yield return new WaitForSeconds(2f);

            textBox.text = "";
            textBox.gameObject.SetActive(false);

            unInteractive = false;
        }
    }
}