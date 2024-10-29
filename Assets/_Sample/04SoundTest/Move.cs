using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    public class Move : MonoBehaviour
    {
        #region Variables
        private Rigidbody rb;

        [SerializeField] private float forwardForce = 5f;
        [SerializeField] private float sideForce = 5f;

        private float dx;
        #endregion

        void Start ()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            dx = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            //앞으로 이동
            rb.AddForce(0f, 0f, forwardForce, ForceMode.Acceleration);

            //좌우 이동
            if(dx < 0f) //좌
            {
                rb.AddForce(-sideForce, 0f, 0f, ForceMode.Acceleration);
            }
            if(dx > 0f) //우
            {
                rb.AddForce(sideForce, 0f, 0f, ForceMode.Acceleration);
            }
        }

    }
}