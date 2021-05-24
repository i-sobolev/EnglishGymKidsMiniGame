using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public class TextChanger : ColorChanger
    {
        public string[] Words;

        public override void ChangeColor(int colorNumber) => GetComponent<TMPro.TextMeshProUGUI>().SetText(Words[colorNumber]);
    }
}