using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private List<AudioObj> _playlist;
    [SerializeField] private AudioSource _musicPlayer;

    private int _selectedId;
    private Coroutine _musicCoroutine;

    private void Start()
    {
        _selectedId = Random.Range(0, _playlist.Count);
        _musicCoroutine = StartCoroutine(StartMusic(_playlist[_selectedId].musicTime));
    }

    public void NextMusic()
    {
        if (_selectedId == _playlist.Count-1)
        {
            _selectedId = 0;
        }

        else
        {
            _selectedId++;
        }

        if (_musicCoroutine != null)
        {
            StopCoroutine(_musicCoroutine);
        }

        _musicCoroutine = StartCoroutine(StartMusic(_playlist[_selectedId].musicTime));
    }

    public void PreviousMusic()
    {
        if (_selectedId == 0)
        {
            _selectedId = _playlist.Count;
        }

        else
        {
            _selectedId--;
        }

        if (_musicCoroutine != null)
        {
            StopCoroutine(_musicCoroutine);
        }

        _musicCoroutine = StartCoroutine(StartMusic(_playlist[_selectedId].musicTime));
    }

    private IEnumerator StartMusic(int time)
    {
        _musicPlayer.clip = _playlist[_selectedId].music;
        _musicPlayer.Play();
        yield return new WaitForSeconds(time);
        _musicCoroutine = null;
        NextMusic();
    }
}

[System.Serializable]

public class AudioObj
{
    public AudioClip music;
    public int musicTime;
}