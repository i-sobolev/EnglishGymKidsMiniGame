using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EnglishKids.Conveyour
{
    public class Robot : MonoBehaviour
    {
        private List<SparePart> _spareParts;
        private int _requiredPartsNumber;

        public UnityEvent OnPartInsert;
        public UnityEvent OnAssemblyComplete;

        private void Awake() => _spareParts = GetComponentsInChildren<SparePart>().ToList();

        private void Start()
        {
            _requiredPartsNumber = _spareParts.Count;

            _spareParts.ForEach(sparePart =>
            {
                sparePart.OnPasteSuccess.AddListener(() =>
                {
                    _requiredPartsNumber--;
                    OnPartInsert?.Invoke();

                    if (_requiredPartsNumber < 1)
                        Complete();
                });
            });
        }

        public void Complete() => OnAssemblyComplete?.Invoke();
    }
}