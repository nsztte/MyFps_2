using UnityEngine;
using UnityEngine.Audio;

namespace MyFps
{
    //오디오를 관리하는 클래스
    public class AudioManager : Singleton<AudioManager>
    {
        #region Variables
        public Sound[] sounds;

        private string bgmSound = "";       //현재 플레이되는 배경음
        public string Bgmsound
        {
            get { return bgmSound; }
        }

        public AudioMixer audioMixer;
        #endregion

        protected override void Awake()
        {   
            //Singleton 구현부
            base.Awake();

            //AudioMixer 그룹 찾아오기
            AudioMixerGroup[] audioMixerGroups = audioMixer.FindMatchingGroups("Master");

            //AudioManager 초기화
            foreach (var sound in sounds)
            {
                sound.source = this.gameObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;

                if (sound.loop) //loop 돌면 bgm 아니면 sfx
                {
                    //Bgm
                    sound.source.outputAudioMixerGroup = audioMixerGroups[1];
                }
                else
                {
                    //Sfx
                    sound.source.outputAudioMixerGroup = audioMixerGroups[2];
                }
            }
        }

        public void Play(string name)
        {
            Sound sound = null;
            
            //매개변수 이름과 같은 클립 찾기
            foreach(var s in sounds)
            {
                if(s.name == name)
                {
                    sound = s;
                    break;
                }
            }

            //매개변수 이름과 같은 클립이 없으면
            if(sound == null)
            {
                Debug.Log($"Cannot Find {name}");
                return;
            }

            sound.source.Play();
        }

        public void Stop(string name)
        {
            Sound sound = null;

            //매개변수 이름과 같은 클립 찾기
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;

                    if(s.name == bgmSound)
                    {
                        bgmSound = "";
                    }

                    break;
                }
            }

            //매개변수 이름과 같은 클립이 없으면
            if (sound == null)
            {
                Debug.Log($"Cannot Find {name}");
                return;
            }

            sound.source.Stop();
        }

        //배경음 재생
        public void PlayBgm(string name)
        {
            //배경음 이름 체크
            if (bgmSound == name)
                return;

            //현재 플레이되는 배경음 정지
            StopBgm();

            Sound sound = null;

            foreach (var s in sounds)
            {
                if(s.name == name)
                {
                    bgmSound = s.name;
                    sound = s;
                    break;
                }
            }

            //매개변수 이름과 같은 클립이 없으면
            if (sound == null)
            {
                Debug.Log($"Cannot Find {name}");
                return;
            }

            sound.source.Play();
        }

        public void StopBgm()
        {
            Stop(bgmSound);
        }
    }
}