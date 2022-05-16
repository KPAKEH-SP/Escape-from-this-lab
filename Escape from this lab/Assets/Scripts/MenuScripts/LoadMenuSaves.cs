using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenuSaves : MonoBehaviour
{
    [SerializeField] private List<SliderInfo> _sliders;

    private void Start()
    {
        foreach (var i in _sliders)
        {
            i.slider.value = PlayerPrefs.GetFloat(i.variableName, 1f);
            i.audio.volume = i.slider.value;
        }
    }
}

[System.Serializable]

class SliderInfo
{
    public Slider slider;
    public AudioSource audio;
    public string variableName;
}
