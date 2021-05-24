using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace EnglishKids.Conveyour
{
    [RequireComponent(typeof(EventTrigger))]
    public class DragDrop : MonoBehaviour
    {
        private Vector2 _basePosition;
        private float _baseAngle;

        public bool IsPicked { get; private set; }
        public bool IsReturned { get; private set; }

        private void Awake()
        {
            IsReturned = true;

            _basePosition = transform.position;
            _baseAngle = transform.eulerAngles.z;
        }

        public void Drag()
        {
            IsPicked = true;
            StopAllCoroutines();
            FollowTouch().Start(this);
            Animator.SmoothRotate(transform, 0, 0.5f).Start(this);
        }

        public void Drop()
        {
            IsPicked = false;
            IsReturned = false;
            StopAllCoroutines();
        }

        public void Return()
        {
            Animator.SmoothTranslate(transform, _basePosition, 1, () => { IsReturned = true; }).Start(this);
            Animator.SmoothRotate(transform, _baseAngle).Start(this);
        }

        public void SetReturnPositionAndAngle(Vector2 position, float angle)
        {
            _basePosition = position;
            _baseAngle = angle;

            transform.position = position;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void OnDestroy()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        public void SetPosition(Vector2 position)
        {
            if (!IsPicked && IsReturned)
                transform.position = position;
        }

        private IEnumerator FollowTouch()
        {
            var startOffset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            var transformOffset = Vector2.up * 1.5f;

            float lerp = 0;

            while (true)
            {
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position) + Vector2.Lerp(startOffset, transformOffset, lerp.EaseInOutQuad());

                if (lerp < 1)
                    lerp += Time.deltaTime / 0.3f;

                yield return null;
            }
        }
    }
}