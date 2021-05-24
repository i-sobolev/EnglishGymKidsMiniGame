using UnityEngine;
using UnityEngine.Events;

namespace EnglishKids.Conveyour
{
    public class Wallet : MonoBehaviour
    {
        private int _counter;

        public UnityEvent OnCoinPick;
        public UnityEvent OnCoinCollect;

        public TMPro.TextMeshProUGUI counterText;

        private void Awake()
        {
            transform.localScale = Vector2.zero;
            counterText.SetText(_counter.ToString());
        }

        public void PickCoin(UnityEngine.UI.Button coin)
        {
            OnCoinPick?.Invoke();

            Animator.SmoothTranslateEaseIn(coin.transform, transform.position, 0.6f, 
                () => { 
                    CollectCoin();
                    Destroy(coin.gameObject);
                }).Start(coin);
        }

        public void Show() => Animator.SmoothScale(transform, 1.2f, 0.5f);

        private void CollectCoin()
        {
            StopAllCoroutines();

            OnCoinCollect?.Invoke();
            
            UpdateCounter();
            PlayCollectAnim();
        }

        private void PlayCollectAnim() => Animator.SmoothScale(transform, 1.2f, 0.05f, () => Animator.SmoothScale(transform, 1f, 0.1f).Start(this)).Start(this);

        private void UpdateCounter()
        {
            _counter += 1;
            counterText.SetText(_counter.ToString());
        }
    }
}