using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFps
{
    public class BreakableObject : MonoBehaviour, IDamagable
    {
        #region Variables
        public GameObject fakeObject;       //온전한 오브젝트
        public GameObject breakObject;      //깨진 오브젝트
        public GameObject effectObject;     //깨지는 움직임 효과

        public GameObject hiddenItem;       //숨겨진 아이템

        private bool isBreak = false;

        [SerializeField] private bool unBreakable = false;   //true면 안깨짐
        #endregion


        //총을 맞으면
        public void TakeDamage(float damage)
        {
            //깨짐 여부 체크
            if (unBreakable)
                return;

            if (!isBreak)
            {
                //health 필요 x (원샷원킬)
                StartCoroutine(BreakObject());
            }

        }

        //페이크 -> 브레이크
        //깨지는 사운드 재생
        IEnumerator BreakObject()
        {
            isBreak = true;
            this.GetComponent<Collider>().enabled = false;

            fakeObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);

            AudioManager.Instance.Play("PotterySmash");
            breakObject.SetActive(true);
            
            //이펙트 오브젝트
            if(effectObject != null)
            {
                effectObject.SetActive(true);

                yield return new WaitForSeconds(0.05f);
                effectObject.SetActive(false);
            }

            //숨겨진 아이템 있으면 아이템 보여주기
            if (hiddenItem != null)
            {
                hiddenItem.SetActive(true);
            }
        }
    }
}