using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public class ImageSpriteChanger : ColorChanger
    {
        public Sprite[] Sprites;

        public override void ChangeColor(int colorNumber) => GetComponent<UnityEngine.UI.Image>().sprite = Sprites[colorNumber];
    }
}