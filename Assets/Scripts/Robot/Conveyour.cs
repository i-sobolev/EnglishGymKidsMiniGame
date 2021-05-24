using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace EnglishKids.Conveyour
{
    public class Conveyour : MonoBehaviour
    {
        [SerializeField] private List<Transform> _partSlots;
        private List<SparePart> _spareParts;

        private int _currentSparePartsCount = 0;

        public UnityEvent OnMoving;
        public UnityEvent OnPartsAreOver;
        public UnityEvent OnAllCoinsCollected;

        [SerializeField] private UnityEngine.UI.Button _coinTemplate;
        private int _coinsNumber = 5;

        private void Start()
        {
            _spareParts = FindObjectsOfType<SparePart>().ToList();

            _spareParts.ForEach(sparePart =>
            {
                sparePart.transform.position = new Vector2(100, 100);
                sparePart.OnPasteFailed.AddListener(() => _coinsNumber -= _coinsNumber > 1 ? 1 : 0);
            });

            _spareParts.Shuffle();

            DeliverNewParts();
        }

        public void DeliverNewParts()
        {
            if (_spareParts.Count < 1)
            {
                OnPartsAreOver?.Invoke();
                DeliverCoins().Start(this);
                return;
            }

            OnMoving?.Invoke();
            Move().Start(this);
        }

        private IEnumerator DeliverCoins()
        {
            yield return new WaitForEndOfFrame();

            var wallet = FindObjectOfType<Wallet>();

            for (int i = 0; i < _coinsNumber; i++)
            {
                var coin = Instantiate(_coinTemplate, _partSlots[i]);
                coin.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => 
                { 
                    wallet.PickCoin(coin);
                    _coinsNumber -= 1;

                    if (_coinsNumber < 1)
                        OnAllCoinsCollected?.Invoke();
                });
            }

            float lerp = 0;

            var tr = transform as RectTransform;
            var target = tr.anchoredPosition;
            var start = new Vector2(0, 1120);

            while (lerp < 1)
            {
                tr.anchoredPosition = Vector2.Lerp(start, target, lerp.EaseInOutQuad());

                lerp += Time.deltaTime / 2.5f;
                yield return null;
            }
        }

        private IEnumerator Move()
        {
            yield return new WaitForEndOfFrame();

            var parts = new List<SparePartComponents>();

            for (int i = 0; i < _partSlots.Count && _spareParts.Count > 0; i++)
            {
                var part = new SparePartComponents(_spareParts[0], _spareParts[0].GetComponent<DragDrop>());
                part.DragDropElement.SetReturnPositionAndAngle(_partSlots[i].position, Random.Range(35, -35));
                parts.Add(part);

                _currentSparePartsCount++;

                part.SparePart.OnPasteSuccess.AddListener(() =>
                {
                    _currentSparePartsCount--;

                    if (_currentSparePartsCount < 1)
                        DeliverNewParts();
                });

                _spareParts.RemoveAt(0);
            }

            float lerp = 0;

            var tr = transform as RectTransform;
            var target = tr.anchoredPosition;
            var start = new Vector2(0, 1120);

            while (lerp < 1)
            {
                for (int i = 0; i < parts.Count; i++)
                    parts[i].DragDropElement.SetPosition(_partSlots[i].transform.position);

                tr.anchoredPosition = Vector2.Lerp(start, target, lerp.EaseInOutQuad());

                lerp += Time.deltaTime / 2.5f;
                yield return null;
            }
        }
    }

    struct SparePartComponents
    {
        public SparePartComponents(SparePart sparePart, DragDrop dragDropElement)
            => (SparePart, DragDropElement) = (sparePart, dragDropElement);

        public SparePart SparePart;
        public DragDrop DragDropElement;
    }
}