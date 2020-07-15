using System;
using UnityEngine.Events;

namespace ACTools.General.Interfaces
{
    public interface ITrackTime
    {
        bool CountInStart { get; set; }
        bool Counting { get; set; }

        float Value { get; }
        float DisplayValue { get; }

        void StartCounting();
        void ContinueCounting();
        void StopCounting();
        void ResetValue();
    }
}

[Serializable]
public class FloatEvent : UnityEvent<float> { };