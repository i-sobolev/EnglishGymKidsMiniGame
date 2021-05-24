using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public class AudioClipChanger : ColorChanger
    {
        public AudioClip[] AudioClips;

        public override void ChangeColor(int colorNumber) => GetComponent<AudioManager>().Colors[(int)LinkedRobot] = AudioClips[colorNumber];
    }
}