using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

namespace EnglishKids.Conveyour
{
    public class SkeletonSkinChanger : ColorChanger
    {
        public string[] SkinNames;

        public override void ChangeColor(int colorNumber)
        {
            var skeleton = GetComponent<SkeletonGraphic>();
            skeleton.initialSkinName = SkinNames[colorNumber];
            skeleton.Initialize(true);
            gameObject.SetActive(false);
        }
    }
}