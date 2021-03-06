﻿using UnderTheMoonlight.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UnderTheMoonlight.UI
{
    public class VolumeSliderBehavior : MonoBehaviour
    {
        [SerializeField] private VolumeManager volManager = null;

        private Slider slider = null;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        /// <summary> Sets the value the slider should be at. </summary>
        public void GetVolume()
        {
            float level = 0f;

            volManager.GetLevel(gameObject.name, ref level);
            slider.value = Mathf.Pow(10f, level / 20f);
        }

        /// <summary>
        /// Sets the volume of group with the same name as this slider.
        /// Slider to level converstion found on the site below:
        /// https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/ 
        /// </summary>
        /// <param name="sliderValue"> Value of slider. </param>
        public void SetVolume(float sliderValue)
        {
            volManager.SetLevel(gameObject.name, Mathf.Log10(sliderValue) * 20f);
        }
    }
}