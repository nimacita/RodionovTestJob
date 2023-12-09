using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickSound : MonoBehaviour
{

    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip sound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.7f;
    }

    private bool OnSound
    {
        get
        {
            if (PlayerPrefs.GetString("Sound").Equals("on") || !PlayerPrefs.HasKey("Sound")) { return true; }
            else { return false; }
        }
    }

    public void BtnClick()
    {
        if (OnSound) {
            audioSource.PlayOneShot(sound);
        }
    }

    
}
