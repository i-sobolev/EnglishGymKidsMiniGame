using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public class ButtonVisibilitySwitcher : MonoBehaviour
    {
        private void Start() => transform.localScale = Vector2.zero;

        public void Show()
        {
            if (transform.localScale.Equals(Vector2.zero))
                Animator.SmoothScale(transform, 1, 0.3f).Start(this);
        }

        public void Hide() => Animator.SmoothScale(transform, 0, 0.3f, () => gameObject.SetActive(false)).Start(this);
    }
}