using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public static class Extentions
    {
        public static float EaseInOutQuad(this float x) => x < 0.5 ? 2 * x * x : 1 - (-2 * x + 2) * (-2 * x + 2) / 2;
        public static float EaseOutQuint(this float x) => 1f - Mathf.Pow(1 - x, 5);
        public static float EaseInQuint(this float x) => Mathf.Pow(x, 4);
        public static float EaseInSin(this float x) => 1 - Mathf.Cos(x * Mathf.PI / 2);

        public static Coroutine Start(this IEnumerator x, MonoBehaviour sender) => sender.StartCoroutine(x);

        public static void Shuffle<T>(this IList<T> list)
        {
            var random = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}