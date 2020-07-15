using System;
using UnityEngine;
using UnityEngine.Events;
using ACTools.General.Interfaces;

namespace ACTools.General
{
    [AddComponentMenu("ACTools/General/Timer")]
    [Serializable]
    public class Timer : MonoBehaviour, ITrackTime
    {
        [SerializeField] private bool countInStart = false;
        public bool CountInStart { get => countInStart; set => countInStart = value; }
        public bool Counting { get; set; } = false;

        [Space]

        [SerializeField] private float startValue = 10f;
        public float StartValue { get => startValue; set => startValue = value; }

        [ReadOnly] [SerializeField] private float value = 0f;
        public float Value => value;

        private float displayValue = 0f;
        public float DisplayValue { get => displayValue; }

        [Header("Events")]
        public UnityEvent OnStart;
        public FloatEvent OnTick;
        public UnityEvent OnFinish;

        private void Awake()
        {
            value = startValue;
        }

        private void Start()
        {
            if (CountInStart)
                StartCounting();
        }

        private void Update()
        {
            if (Counting)
            {
                if (value >= 0)
                {
                    value -= Time.deltaTime;
                    if (displayValue - 1 >= value)
                    {
                        displayValue = Mathf.Ceil(value);
                        OnTick.Invoke(displayValue);
                    }
                }
                else
                {
                    Counting = false;
                    OnFinish.Invoke();
                }
            }
        }

        public void StartCounting()
        {
            Counting = true;
            OnStart.Invoke();
        }

        public void ContinueCounting()
        {
            Counting = true;
        }

        public void StopCounting()
        {
            Counting = false;
        }

        public void ResetValue()
        {
            value = startValue;
        }
    }
}