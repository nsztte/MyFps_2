using UnityEngine;

namespace MyFps
{
    //AmmoBox 아이템 획득
    public class PickupAmmoBox : Interactive
    {
        #region Variables
        //AmmoBox 아이템 획득시 지급하는 ammo 갯수
        [SerializeField] private int giveAmmo = 7;
        #endregion

        protected override void DoAction()
        {
            //
            Debug.Log("탄환 7개를 지급하였습니다.");
            PlayerStats.Instance.AddAmmo(giveAmmo);

            //킬
            Destroy(gameObject);
        }
    }
}