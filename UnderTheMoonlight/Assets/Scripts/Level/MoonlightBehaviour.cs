using System;
using System.Collections;
using UnderTheMoonlight.Characters;
using UnityEngine;
using UnityEngine.Events;

namespace UnderTheMoonlight.Level
{
    public class MoonlightBehaviour : MonoBehaviour
    {
        // Components
        protected Animator animator = null;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider2D;

        // Events
        public WerewolfTransformation EnterMoonlight;
        public WerewolfTransformation ExitMoonlight;

        // Params
        [SerializeField] public bool isMoonlightActive = true;
        [SerializeField] public bool useTimer = false;

        [SerializeField, Range(1f, 120f)] public float moonlightActiveTime = 1f;
        [SerializeField, Range(1f, 120f)] public float moonlightInactiveTime = 1f;

        // Variables
        private float _currentTimerCount = 0f;
        
        private static readonly int IsActive = Animator.StringToHash("IsActive");
        private static readonly int StartActive = Animator.StringToHash("StartActive");
        private static readonly int GoToStart = Animator.StringToHash("GoToStart");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetBool(IsActive, isMoonlightActive);
            animator.SetBool(StartActive, isMoonlightActive);
            animator.SetTrigger(GoToStart);
        }

        private void Start()
        {
            if (useTimer)
            {
                Debug.Log("here");
                StartCoroutine(MoonlightTimer());
            }
        }

        private IEnumerator MoonlightTimer()
        {
            _currentTimerCount = isMoonlightActive ? moonlightActiveTime : moonlightInactiveTime;

            yield return new WaitForSeconds(_currentTimerCount);

            ToggleMoonlight();
            StartCoroutine(MoonlightTimer());
        }
    
        private void ToggleMoonlight()
        {
            isMoonlightActive = !isMoonlightActive;
            animator.SetBool(IsActive, isMoonlightActive);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isMoonlightActive && other.TryGetComponent(out WerewolfMovement werewolf))
                EnterMoonlight?.Invoke(werewolf);
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out WerewolfMovement werewolf))
                ExitMoonlight?.Invoke(werewolf);
        }
    }

    [Serializable]
    public class WerewolfTransformation : UnityEvent<WerewolfMovement> { }
}