using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Audio
{
    public class AudioController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private List<AudioClip> _soundClips;
        [SerializeField] private GameObject _soundParent;
        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _sound;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundsSlider;

        #endregion Inspector variables

        #region private variables

        private float soundVolume;
        private float musicVolume;
        private List<AudioSource> spawnedSounds = new List<AudioSource>();

        #endregion private variables

        #region public functions

        public void ChangeVolumeMusic()
        {
            musicVolume = _musicSlider.value;
            _music.volume = musicVolume;
        }
        
        public void ChangeVolumeSounds()
        {
            soundVolume = _soundsSlider.value;
            _sound.volume = soundVolume;
        }

        public void PlayMusic() => _music.Play();

        public void PlaySound(int id)
        {
            if (_sound.clip == null)
            {
                _sound.clip = _soundClips[id];
                _sound.Play();
                if (!_sound.isPlaying)
                {
                    _sound.clip = null;
                }
            }
            else
            {
                AudioSource finded = null;
                if (spawnedSounds.Count > 0)
                {
                    foreach (var item in spawnedSounds)
                    {
                        if (!item.isPlaying)
                        {
                            finded = item;
                            break;
                        }
                    }

                    if (finded != null)
                    {
                        finded.clip = _soundClips[id];
                        finded.volume = soundVolume;
                        finded.Play();
                    }
                    else
                    {
                        var newSound = new GameObject("New Sound");
                        newSound.AddComponent<AudioSource>();
                        newSound.transform.SetParent(_soundParent.transform);
                        newSound.transform.position = Vector3.zero;
                        var newSoundSource = newSound.GetComponent<AudioSource>();
                        newSoundSource.volume = soundVolume;
                        newSoundSource.playOnAwake = false;
                        newSoundSource.clip = _soundClips[id];
                        newSoundSource.Play();
                        spawnedSounds.Add(newSoundSource);
                    }
                }
            }
        }

        #endregion public functions
    }
}
