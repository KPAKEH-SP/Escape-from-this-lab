using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainSaves : MonoBehaviour
{
    [SerializeField] private AudioSource _music;

    private void Start()
    {
        _music.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
