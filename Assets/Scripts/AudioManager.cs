using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Sound effects")]
        [SerializeField] private AudioSource _correct;
        [SerializeField] private AudioSource _wrong;
        [SerializeField] private AudioSource _conveyourMove;
        [SerializeField] private AudioSource _sparePartPick;
        [SerializeField] private AudioSource _robot1;
        [SerializeField] private AudioSource _robot2;
        [SerializeField] private AudioSource _coinCollect;
        [SerializeField] private AudioSource _coinPick;

        public void PlayCorrectSoundEffect() => _correct.PlayOneShot(_correct.clip);
        public void PlayWrongSoundEffect() => _wrong.PlayOneShot(_wrong.clip);
        public void PlayConveyourMoveSoundEffect() => _conveyourMove.PlayOneShot(_conveyourMove.clip);
        public void PlaySparePartPickSoundEffect() => _sparePartPick.PlayOneShot(_sparePartPick.clip);
        public void PlayRobot1SoundEffect() => _robot1.PlayOneShot(_robot1.clip);
        public void PlayRobot2SoundEffect() => _robot2.PlayOneShot(_robot2.clip);
        public void PlayCoinCollectSoundEffect() => _coinCollect.PlayOneShot(_coinCollect.clip);
        public void PlayCoinPickSoundEffect() => _coinPick.PlayOneShot(_coinPick.clip);

        [Header("Colors")]
        [SerializeField] private AudioSource _currentColor;
        public List<AudioClip> Colors;

        private float _nextColorDelay;
        private AudioClip _nextClip;

        public void PlayColorSound(int colorNumber)
        {
            colorNumber -= 1;

            if (_currentColor.isPlaying)
            {
                _nextColorDelay = _currentColor.clip.length - _currentColor.time;
                _nextClip = Colors[colorNumber];

                StopAllCoroutines();
                PlayColorSoundDelayed().Start(this);
            }

            else
            {
                _currentColor.clip = Colors[colorNumber];
                _currentColor.Play();
            }
        }

        private IEnumerator PlayColorSoundDelayed()
        {
            yield return new WaitForSecondsRealtime(_nextColorDelay);
            _currentColor.clip = _nextClip;
            _currentColor.Play();
        }
    }
}