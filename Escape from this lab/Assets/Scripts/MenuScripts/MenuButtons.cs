using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private List<sliderElement> _settingsSliders;

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenInterface(GameObject controlledObject)
    {
        controlledObject.SetActive(true);
    }

    public void CloseInterface(GameObject controlledObject)
    {
        controlledObject.SetActive(false);
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }

    public void SaveSlider(int sliderId)
    {
        PlayerPrefs.SetFloat(_settingsSliders[sliderId].variableName, _settingsSliders[sliderId].slider.value);
        _settingsSliders[sliderId].auidoObject.volume = _settingsSliders[sliderId].slider.value;
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Play();
    }
}

[System.Serializable]

class sliderElement
{
    public string variableName;
    public Slider slider;
    public AudioSource auidoObject;
}