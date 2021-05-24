using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public class ButtonClickAnimation : MonoBehaviour
    {
        private void Awake() => GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PlayPressAnim());

        private void PlayPressAnim()
        {
            StopAllCoroutines();
            Animator.SmoothScale(transform, 0.8f, 0.1f, () => Animator.SmoothScale(transform, 1f, 0.2f).Start(this)).Start(this);
        }
    }
}