using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic _Instance { get; private set; }


    public AudioClip[] BackgroundClips;
    public AudioSource MusicAudioSource;

    [SerializeField] private bool nextMusic;

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

            nextMusic = false;
        }
    }

    public void LateUpdate()
    {
        if (MusicAudioSource.isPlaying == false && !nextMusic)
        {
            nextMusic = true;
            if (Random.Range(0, 100) < 50)
                Invoke("DetectNextMusic", Random.Range(15, 90));
            else
            {
                nextMusic = false;
                MusicAudioSource.Play();
            }
        }
    }

    public void DetectNextMusic()
    {
        int clipIndex = Random.Range(0, BackgroundClips.Length);
        MusicAudioSource.clip = BackgroundClips[clipIndex];
        MusicAudioSource.Play();
        nextMusic = false;
    }
}
