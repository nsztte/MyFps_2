using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


namespace MyFps
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string loadToScene = "MainScene01";

        private AudioManager audioManager;

        public GameObject mainMenuUI;
        public GameObject optionUI;
        public GameObject CreditUI;

        public GameObject loadGameButton;

        //Audio
        public AudioMixer audioMixer;
        public Slider bgmSlider;
        public Slider sfxSlider;

        //저장되어 있는 씬 번호
        //private int sceneNumber;
        #endregion

        private void Start()
        {
            //게임 데이터 초기화
            InitGameData();

            //저장된 값이 있으면
            if(PlayerStats.Instance.SceneNumber > 0)
            {
                loadGameButton.SetActive(true);
            }

            //씬 페이드인 효과
            fader.FromFade();

            //참조
            audioManager = AudioManager.Instance;

            //Bgm 플레이
            audioManager.Play("MenuBGM");
        }

        private void InitGameData()
        {
            //게임 설정값: 저장된 옵션값 불러오기
            LoadOptions();

            //게임 플레이 데이터 로드
            PlayData playData = SaveLoad.LoadData();
            PlayerStats.Instance.PlayerStatsInit(playData);
        }


        public void NewGame()
        {
            //데이터 초기화
            PlayerStats.Instance.PlayerStatsInit(null);

            audioManager.Stop(audioManager.Bgmsound);
            audioManager.Play("MenuButton");

            fader.FadeTo(loadToScene);
        }

        public void LoadGame()
        {
            //Debug.Log($"Go to LoadGame {PlayerStats.Instance.SceneNumber}번 씬");

            audioManager.Stop(audioManager.Bgmsound);
            audioManager.Play("MenuButton");

            fader.FadeTo(PlayerStats.Instance.SceneNumber);
        }

        public void Options()
        {
            audioManager.Play("MenuButton");
            ShowOptions();
        }

        public void Credits()
        {
            ShowCredit();
        }

        public void QuitGame()
        {
            //치트
            //PlayerPrefs.DeleteAll();
            //SaveLoad.DeleteFile();

            Application.Quit();
        }

        private void ShowOptions()
        {
            audioManager.Play("MenuButton");
            mainMenuUI.SetActive(false);
            optionUI.SetActive(true);
        }

        public void HideOptions()
        {
            //옵션값 저장
            SaveOptions();

            audioManager.Play("MenuButton");
            optionUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }

        //AudioMix Bgm -40~0
        public void SetBgmVolume(float value)
        {
            audioMixer.SetFloat("BgmVolume", value);
        }

        //AudioMix Sfx -40~0
        public void SetSfxVolume(float value)
        {
            audioMixer.SetFloat("SfxVolume", value);
        }

        //옵션값 저장하기
        private void SaveOptions()
        {
            PlayerPrefs.SetFloat("BgmVolume", bgmSlider.value);
            PlayerPrefs.SetFloat("SfxVolume", sfxSlider.value);
        }

        //옵션값 로드하기
        private void LoadOptions()
        {
            //배경음 볼륨
            float bgmVolume = PlayerPrefs.GetFloat("BgmVolume", 0);
            SetBgmVolume(bgmVolume);        //사운드 볼륨 조절
            bgmSlider.value = bgmVolume;    //UI 슬라이더 셋팅

            //효과음 볼륨
            float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0);
            SetSfxVolume(sfxVolume);        //사운드 볼륨 조절
            sfxSlider.value = sfxVolume;    //UI 슬라이더 셋팅
        }

        private void ShowCredit()
        {
            mainMenuUI.SetActive(false);
            CreditUI.SetActive(true);
        }
    }
}