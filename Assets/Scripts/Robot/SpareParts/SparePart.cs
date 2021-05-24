using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace EnglishKids.Conveyour
{
    public class SparePart : MonoBehaviour
    {
        private const float _allowableInsertDistance = 45;

        public UnityEvent OnPasteSuccess;
        public UnityEvent OnPasteFailed;

        public Vector2 Slot { get; protected set; }

        private void Awake() => Slot = transform.localPosition;

        public virtual void InsertInSlot()
        {
            if (CheckDistanceTo(Slot))
            {
                OnPasteSuccess?.Invoke();
                Animator.SmoothTranslateLocal(transform, Slot).Start(this);

                DestroyDragDropComponent();
            }

            else
            {
                OnPasteFailed?.Invoke();
            }
        }

        protected void DestroyDragDropComponent()
        {
            Destroy(GetComponent<DragDrop>());
            Destroy(GetComponent<EventTrigger>());
        }

        public bool CheckDistanceTo(Vector2 slotPosition) => Vector2.Distance(slotPosition, transform.localPosition) < _allowableInsertDistance;
    }
}