using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFps
{
    public class HEnemyZoneOutTrigger : MonoBehaviour
    {
        #region Variables
        public Transform gunMan;

        public GameObject enemyZoneIn;
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("적 제자리로 돌아가라");
            if (other.tag == "Player")
            {
                if (gunMan != null)
                {
                    gunMan.GetComponent<Enemy>().GoStartPosition();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("In Trigger 활성화");
                this.gameObject.SetActive(false);
                enemyZoneIn.SetActive(true);
            }
        }
    }
}