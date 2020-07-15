using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderBehavior : MonoBehaviour
{
    [SerializeField] private VolumeManager volManager = null;

    private Slider slider = null;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    /// <summary> Sets the value the slider should be at. </summary>
    public void SetSlider()
    {
        float level = 0f;
        volManager.GetVolume(gameObject.name, ref level);
        slider.value = level;
    }

    /// <summary>
    /// Sets the volume of group with the same name as this slider.
    /// Old Slider to level converstion found on the site below:
    /// https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/ 
    /// </summary>
    /// <param name="sliderValue"> Value of slider. </param>
    public void SetVolume(float sliderValue)
    {
        volManager.SetVolume(gameObject.name, sliderValue);
    }
}