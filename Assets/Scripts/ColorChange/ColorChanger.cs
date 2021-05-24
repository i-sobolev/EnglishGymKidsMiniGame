using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public abstract class ColorChanger : MonoBehaviour
    {
        public LinkedRobot LinkedRobot;
        public abstract void ChangeColor(int colorNumber);
    }

    public enum LinkedRobot { Left, Right }
}