using UnityEngine;

namespace MyFps
{
    public class PickupItem : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float verticalBobFrequency = 1f;   //이동 속도
        [SerializeField] private float bobbingAmount = 1f;          //이동 거리
        [SerializeField] private float rotateSpeed = 360f;          //회전 속도
                                                                          
        private Vector3 startPosition;                              //시작 위치
        #endregion

        private void Start()
        {
            //처음 위치
            startPosition = transform.position;
        }

        private void Update()
        {
            //위아래 흔들림
            float bobbingAnimationPhase = Mathf.Sin(Time.time * verticalBobFrequency) * bobbingAmount;
            transform.position = startPosition + Vector3.up * bobbingAnimationPhase;
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            //플레이어 체크
            if(other.tag == "Player")
            {
                if(OnPickup() == true)
                {
                    //획득 성공 효과(사운드, 이펙트)

                    //킬
                    Destroy(gameObject);
                }
            }    
        }

        //아이템 획득 성공, 실패 변환
        protected virtual bool OnPickup()
        {

            return true;
        }
    }
}