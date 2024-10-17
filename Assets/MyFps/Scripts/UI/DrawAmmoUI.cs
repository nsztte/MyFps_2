using TMPro;
using UnityEngine;

namespace MyFps
{
    public class DrawAmmoUI : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI ammoCount;
        #endregion

        //private void OnEnable() //활성화되는 딱 한번만 실행
        //{
            
        //}

        // Update is called once per frame
        void Update()   //ammo 수가 계속 바뀌기 떄문에 update에서
        {
            ammoCount.text = PlayerStats.Instance.AmmoCount.ToString();
        }
    }
}