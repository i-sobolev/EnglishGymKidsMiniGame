using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace EnglishKids.Conveyour
{
    public class ColorRandomizer : MonoBehaviour
    {
        private void Awake()
        {
            var r = new System.Random();

            var leftColor = r.Next(0, 3);
            var rightColor = r.Next(0, 3);

            var colorChangers = FindObjectsOfType<ColorChanger>().ToList();

            colorChangers.ForEach(colorChanger =>
            {
                if (colorChanger.LinkedRobot == LinkedRobot.Left)
                    colorChanger.ChangeColor(leftColor);

                else
                    colorChanger.ChangeColor(rightColor);
            });
        }
    }
}