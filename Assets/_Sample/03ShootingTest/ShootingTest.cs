using MyFps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    public class ShootingTest : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        //발사 효과
        public ParticleSystem muzzle;
        public AudioSource pistolShot;

        //public Transform camera;
        public Transform firePoint;

        //공격 데미지
        [SerializeField] private float attackDamage = 5f;

        //연사 딜레이
        [SerializeField] private float fireDelay = 0.5f;
        private bool isFire = false;

        //탄착 임팩트 효과
        public GameObject hitImpactPrefab;

        [SerializeField] private float impactForce = 10f;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //참조
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //슛
            if (Input.GetButtonDown("Fire") && !isFire)
            {
                StartCoroutine(Shoot());
            }
        }

        IEnumerator Shoot()
        {
            isFire = true;

            //내앞에 100안에 적이 있으면 적에게 데미지를 준다
            float maxDistance = 100f;
            RaycastHit hit; //hit되는 오브젝트의 정보를 저장
            if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, maxDistance))
            {
                //적에게 데미지를 준다
                Debug.Log($"{hit.transform.name}에게 데미지를 준다");

                //임팩트 효과
                GameObject effectGo = Instantiate(hitImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));   //normal 방향: 부딪혀나오는 방향(직각)
                Destroy(effectGo, 2f);

                if(hit.rigidbody != null )
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);   //-normal: 부딪혀나오는 방향의 반대(힘을 주는 방향) / Impulse: 한번만 힘을 줌
                }



                //데미지 주기
                IDamagable damagable = hit.transform.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(attackDamage);
                }

                //RobotController robot = hit.transform.GetComponent<RobotController>();
                //if (robot != null) //로봇이 아닐 경우 제외
                //{
                //    robot.TakeDamage(attackDamage);
                //}

                //EnemyTest enemy = hit.transform.GetComponent<EnemyTest>();
                //if(enemy != null)
                //{
                //    enemy.TakeDamage(attackDamage);
                //}

                //ZombieTest zombie = hit.transform.GetComponent<ZombieTest>();
                //if (zombie != null)
                //{
                //    zombie.TakeDamage(attackDamage);
                //}
            }

            //슛 효과 - VFX, SFX
            animator.SetTrigger("Fire");

            pistolShot.Play();

            muzzle.gameObject.SetActive(true);
            muzzle.Play();

            yield return new WaitForSeconds(fireDelay);
            muzzle.Stop();
            muzzle.gameObject.SetActive(false);

            isFire = false;
        }

        //Gimo 그리기 : 총 위치에서 앞에 충돌체까지 레이저 쏘는 선 그리기
        private void OnDrawGizmosSelected()
        {
            float maxDistance = 100f;
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, maxDistance);

            Gizmos.color = Color.red;
            if (isHit)
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * hit.distance);
            }
            else
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * maxDistance);
            }
        }
    }
}
