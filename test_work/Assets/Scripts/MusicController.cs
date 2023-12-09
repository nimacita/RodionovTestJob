using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        CanPlayMusic();
    }

    
    void FixedUpdate()
    {
        CanPlayMusic();
    }

    private void CanPlayMusic()
    {
        if (audioSource != null) 
        {
            audioSource.mute = !OnMusic;
        }
    }

    private bool OnMusic
    {
        get
        {
            if (PlayerPrefs.GetString("Music").Equals("on") || !PlayerPrefs.HasKey("Music")) { return true; }
            else { return false; }
        }
    }
}
