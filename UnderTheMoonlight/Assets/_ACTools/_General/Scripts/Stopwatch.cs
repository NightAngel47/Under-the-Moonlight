using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ACTools.General.Interfaces;

namespace ACTools.General
{
    [AddComponentMenu("ACTools/General/Stopwatch")]
    [Serializable]
    public class Stopwatch : MonoBehaviour, ITrackTime
    {
        [SerializeField] private bool countInStart = false;
        public bool CountInStart { get => countInStart; set => countInStart = value; }
        public bool Counting { get; set; } = false;

        [Space]

        [ReadOnly] [SerializeField] private float value = 0f;
        public float Value => value;

        private float displayValue = 0f;
        public float DisplayValue { get => displayValue; }

        [Space]

        [ReadOnly]
        [SerializeField]
        private List<float> laps = new List<float>();
        public float[] LapArray => laps.ToArray();
        private float currentLappedTime = 0;

        [Header("Events")]
        public UnityEvent OnStart;
        public FloatEvent OnTick;

        private void Start()
        {
            if (CountInStart)
                StartCounting();
        }

        private void Update()
        {
            if (Counting)
            {
                value += Time.deltaTime;
                if (displayValue + 1 <= value)
                {
                    displayValue = Mathf.Floor(value);
                    OnTick.Invoke(DisplayValue);
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
            value = 0;
        }

        public void Lap()
        {
            if (laps.Count == 0)
            {
                laps.Add(value);
                currentLappedTime += value;
            }
            else
            {
                float finishedLap = value - currentLappedTime;
                laps.Add(finishedLap);
                currentLappedTime += finishedLap;
            }
        }

        public void ResetLapList()
        {
            laps = new List<float>();
            currentLappedTime = 0;
        }
    }
}