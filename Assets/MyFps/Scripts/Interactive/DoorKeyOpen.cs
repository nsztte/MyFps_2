using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MyFps
{
    public class DoorKeyOpen : Interactive
    {
        #region Variables
        public TextMeshProUGUI textBox;

        [SerializeField]
        private string sequence = "You need the key";
        #endregion


        protected override void DoAction()
        {
            if(PlayerStats.Instance.HasPuzzleItem(PuzzleKey.ROOM01_KEY))
            {
                OpenDoor();
            }
            else
            {
                StartCoroutine(LockedDoor());
            }
        }

        void OpenDoor()
        {
            GetComponent<BoxCollider>().enabled = false;

            GetComponent<Animator>().SetBool("IsOpen", true);
            AudioManager.Instance.Play("DoorOpen");
        }

        IEnumerator LockedDoor()
        {
            unInteractive = true;

            //문잠긴 소리
            AudioManager.Instance.Play("DoorLocked");

            yield return new WaitForSeconds(0.5f);

            textBox.gameObject.SetActive(true);
            textBox.text = sequence;

            yield return new WaitForSeconds(2f);

            textBox.gameObject.SetActive(false);
            textBox.text = "";
            unInteractive = false;
        }
    }
}