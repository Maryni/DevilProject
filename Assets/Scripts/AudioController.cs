using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Audio
{
    public class AudioController : MonoBehaviour
    {
        #region Inspector variables
        
        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _sound;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        
        #endregion Inspector variables

        #region private variables

        private float musicVolume;
        private float soundVolume;

        #endregion private variables

        #region public functions
        
        public void ChangeVolumeMusic()
        {
            musicVolume = _musicSlider.value;
            _music.volume = musicVolume;
        }
        
        public void ChangeVolumeSounds()
        {
            soundVolume = _soundSlider.value;
            _sound.volume = soundVolume;
        }


        public void PlayMusic() => _music.Play();

        public void PlaySound() => _sound.Play();

        #endregion public functions
    }
}
