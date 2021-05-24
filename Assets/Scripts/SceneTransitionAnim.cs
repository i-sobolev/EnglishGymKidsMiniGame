using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnglishKids.Conveyour
{
    public class SceneTransitionAnim : MonoBehaviour
    {
        private List<Pinion> _pinions;
        public float PinionHideShowPositionsDelta;

        public UnityEvent OnPinionsShowed;

        [Header("Animation parameters")]
        public bool HideAnimOnStart;
        public float HideDelay;
        public float ShowDelay;

        private void OnEnable()
        {
            _pinions = new List<Pinion>();

            foreach (var pinion in gameObject.GetComponentsInChildren<Transform>())
            {
                if (pinion.Equals(transform))
                    continue;

                var newPinion = new Pinion(pinion, (Vector2.zero - (Vector2)pinion.position).normalized * -PinionHideShowPositionsDelta, pinion.position);
                _pinions.Add(newPinion);
                
                if (!HideAnimOnStart)
                    newPinion.Transform.position = newPinion.HidePosition;
            }
        }

        private void Start()
        {
            if (HideAnimOnStart)
                PlayHideOnStart().Start(this);
        }

        private IEnumerator PlayHideOnStart()
        {
            yield return new WaitForSeconds(0.6f);
            Hide();
        }

        public void Play() => Transition().Start(this);

        private IEnumerator Transition()
        {
            yield return new WaitForSeconds(ShowDelay);
            Show();

            yield return new WaitForSeconds(HideDelay);
            OnPinionsShowed?.Invoke();

            Hide();
        }

        private void Show()
        {
            _pinions.ForEach(pinion =>
            {
                Animator.SmoothRotate(pinion.Transform, Random.Range(-60, 60), 1f).Start(this);
                Animator.SmoothTranslate(pinion.Transform, pinion.ShowPosition, 1f).Start(this);
            });
        }

        private void Hide()
        {
            _pinions.ForEach(pinion =>
            {
                Animator.SmoothRotate(pinion.Transform, Random.Range(-60, 60), 1f).Start(this);
                Animator.SmoothTranslate(pinion.Transform, pinion.HidePosition, 1f).Start(this);
            });
        }
    }

    public struct Pinion
    {
        public Pinion(Transform transform, Vector2 hidePosition, Vector2 showPosition)
        {
            Transform = transform;
            HidePosition = hidePosition;
            ShowPosition = showPosition;
        }

        public Transform Transform;
        public Vector2 HidePosition;
        public Vector2 ShowPosition;
    }
}