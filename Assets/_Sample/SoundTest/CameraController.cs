using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MySample
{
    public class CameraController : MonoBehaviour
    {
        #region Variables
        public Transform thePlayer;

        [SerializeField] private Vector3 offset;
        #endregion

        private void LateUpdate()   //카메라 위치는 LateUpdate에서
        {
            this.transform.position = thePlayer.position + offset;
        }
    }
}