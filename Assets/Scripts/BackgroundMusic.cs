using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic _Instance { get; private set; }


    public AudioClip[] BackgroundClips;
    public AudioSource MusicAudioSource;

    private int currentMusicIndex;
    [SerializeField] private int nextMusic;
    
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
            currentMusicIndex = clipIndex;
            PlayerPrefs.SetInt("CurrentMusicIndex", currentMusicIndex);
            MusicAudioSource.clip = BackgroundClips[clipIndex];
            MusicAudioSource.Play();
        }
    }

    public void UpdateBackgroundMusic()
    {
        if (PlayerPrefs.HasKey("UpdateBGM"))
        {
            nextMusic = PlayerPrefs.GetInt("UpdateBGM");
            nextMusic++;
            PlayerPrefs.SetInt("UpdateBGM", nextMusic);

            if(nextMusic >= 10)
            {
                nextMusic = 0;

                if (PlayerPrefs.HasKey("CurrentMusicIndex"))
                {
                    currentMusicIndex = PlayerPrefs.GetInt("CurrentMusicIndex");
                }

                bool isClipReapeated = true;
                
                while(isClipReapeated)
                {
                    int clipIndex = Random.Range(0, BackgroundClips.Length);
                    if(clipIndex != currentMusicIndex)
                    {
                        isClipReapeated = false;
                        currentMusicIndex = clipIndex;
                        MusicAudioSource.clip = BackgroundClips[clipIndex];
                        MusicAudioSource.Play();
                    }
                }
            }
        }
        else
        {
            nextMusic++;
            PlayerPrefs.SetInt("UpdateBGM", nextMusic);
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.DeleteKey("CurrentMusicIndex");
        PlayerPrefs.DeleteKey("UpdateBGM");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("CurrentMusicIndex");
        PlayerPrefs.DeleteKey("UpdateBGM");
    }
}
