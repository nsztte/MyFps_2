using UnityEngine;

namespace MyFps
{
    //파일에 저장할 게임 플레이 데이터 목록
    [System.Serializable]   //직렬화해야 이진화 가능
    public class PlayData
    {
        public int sceneNumber;
        public int ammoCount;
        public bool hasGun;

        //생성자 - PlayerStats에 있는 데이터로 초기화
        public PlayData()
        {
            sceneNumber = PlayerStats.Instance.SceneNumber;
            ammoCount = PlayerStats.Instance.AmmoCount;
            hasGun = PlayerStats.Instance.HasGun;
        }
    }
}