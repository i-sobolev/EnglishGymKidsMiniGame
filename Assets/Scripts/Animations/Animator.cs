using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnglishKids.Conveyour
{
    public static class Animator
    {
        public static IEnumerator SmoothRotate(Transform transform, float targetAngle, float time = 1, UnityAction actionOnEnd = null)
        {
            var startAngle = transform.eulerAngles.z;
            float lerp = 0;

            while (lerp < 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(startAngle, targetAngle, lerp.EaseInOutQuad()));
                lerp += Time.deltaTime / time;

                yield return null;
            }

            yield return null;
        }

        public static IEnumerator SmoothTranslate(Transform transform, Vector2 targetPosition, float time = 1, UnityAction onEnd = null)
        {
            var startPosition = transform.position;
            float lerp = 0;

            while(lerp < 1)
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, lerp.EaseOutQuint());
                lerp += Time.deltaTime / time;

                yield return null;
            }

            onEnd?.Invoke();
        }

        public static IEnumerator SmoothTranslateEaseIn(Transform transform, Vector2 targetPosition, float time = 1, UnityAction onEnd = null)
        {
            var startPosition = transform.position;
            float lerp = 0;

            while (lerp < 1)
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, lerp.EaseInSin());
                lerp += Time.deltaTime / time;

                yield return null;
            }

            onEnd?.Invoke();
        }

        public static IEnumerator SmoothTranslateLocal(Transform transform, Vector2 targetPosition, float time = 1, UnityAction onEnd = null)
        {
            var startPosition = transform.localPosition;
            float lerp = 0;

            while (lerp < 1)
            {
                transform.localPosition = Vector2.Lerp(startPosition, targetPosition, lerp.EaseOutQuint());
                lerp += Time.deltaTime / time;

                yield return null;
            }

            onEnd?.Invoke();
        }

        public static IEnumerator SmoothScale(Transform transform, float targetScale, float time = 1, UnityAction onEnd = null)
        {
            var startScale = transform.localScale;
            var targetScaleVector = new Vector2(targetScale, targetScale);

            float lerp = 0;

            while (lerp < 1)
            {
                transform.localScale = Vector2.Lerp(startScale, targetScaleVector, lerp.EaseInOutQuad());
                lerp += Time.deltaTime / time;

                yield return null;
            }

            onEnd?.Invoke();
        }
    }
}
