using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic _Instance { get; private set; }


    public AudioClip[] BackgroundClips;
    public AudioSource MusicAudioSource;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _Instance = this;

            DontDestroyOnLoad(gameObject);

            int clipIndex = Random.Range(0, BackgroundClips.Length);
            MusicAudioSource.clip = BackgroundClips[clipIndex];
            MusicAudioSource.Play();

            StartCoroutine(NextMusic(MusicAudioSource.clip.length));
        }
    }

    private IEnumerator NextMusic(float time)
    {
        yield return new WaitForSeconds(time);
        if (Random.Range(0, 100) < 30)
            Invoke("DetectNextMusic", Random.Range(15, 75));
        else
        {
            MusicAudioSource.Stop();
            MusicAudioSource.Play();
            StartCoroutine(NextMusic(MusicAudioSource.clip.length));
        }
    }

    public void DetectNextMusic()
    {
        int clipIndex = Random.Range(0, BackgroundClips.Length);
        MusicAudioSource.clip = BackgroundClips[clipIndex];
        MusicAudioSource.Play();
        StartCoroutine(NextMusic(MusicAudioSource.clip.length));
    }
}
