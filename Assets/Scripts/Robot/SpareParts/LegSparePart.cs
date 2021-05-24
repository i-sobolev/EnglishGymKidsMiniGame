using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.Conveyour
{
    public class LegSparePart : SparePart
    {
        public Vector2 SecondSlot { get; private set; }
        public LegSparePart SecondLeg;
        protected enum InsertedInSlot { None, FirstSlot, SecondSlot}
        protected InsertedInSlot CurrentSlot;

        private void Start()
        {
            CurrentSlot = InsertedInSlot.None;
            SecondSlot = SecondLeg.Slot;
        }

        public override void InsertInSlot()
        {
            bool firstSlotAvailable = CheckDistanceTo(Slot) && (SecondLeg.CurrentSlot.Equals(InsertedInSlot.FirstSlot) || SecondLeg.CurrentSlot.Equals(InsertedInSlot.None));
            bool secondSlotAvailable = CheckDistanceTo(SecondSlot) && (SecondLeg.CurrentSlot.Equals(InsertedInSlot.SecondSlot) || SecondLeg.CurrentSlot.Equals(InsertedInSlot.None));

            if (firstSlotAvailable || secondSlotAvailable)
            {
                OnPasteSuccess?.Invoke();

                if (firstSlotAvailable)
                {
                    Animator.SmoothTranslateLocal(transform, Slot).Start(this);
                    CurrentSlot = InsertedInSlot.FirstSlot;
                }

                if (secondSlotAvailable)
                {
                    Animator.SmoothTranslateLocal(transform, SecondSlot).Start(this);
                    CurrentSlot = InsertedInSlot.SecondSlot;
                }

                DestroyDragDropComponent();
            }

            else
            {
                OnPasteFailed?.Invoke();
            }
        }
    }
}
    
