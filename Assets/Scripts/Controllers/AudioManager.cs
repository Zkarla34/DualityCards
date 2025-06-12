using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("General")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Ambiente")]
    [SerializeField] private AudioClip ambientSound;

    [Header("Buttons")]
    [SerializeField] private AudioClip buttonClickSound;

    [Header("Level Sound")]
    [SerializeField] private LevelAudioData[] levelSound;
    [SerializeField] private AudioClip wrongMatchSound;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayAmbientMusic();
    }

    public void PlayAmbientMusic()
    {
        if(ambientSound != null)
        {
            musicSource.clip = ambientSound;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSound);
    }

    public void PlaySFX(AudioClip clip)
    {
        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayMatchSound(int levelIndex)
    {
        if(levelIndex >=0 && levelIndex < levelSound.Length)
        {
            PlaySFX(levelSound[levelIndex].matchSound);
        }
    }
    public void PlayWrongMatch()
    {
        PlaySFX(wrongMatchSound);
    }
}
