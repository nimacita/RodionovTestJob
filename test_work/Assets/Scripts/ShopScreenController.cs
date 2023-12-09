using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopScreenController : MonoBehaviour
{

    [Space]
    [Header("Tickets")]
    [SerializeField]
    private TextMeshProUGUI ticketsCountTxt;

    [Header("Btn Click Sound")]
    [Space]
    [SerializeField]
    private AudioClip soundClip;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.7f;

        UpdateTicketsCountTxt();
    }

    public int Tickets
    {
        get
        {
            if (!PlayerPrefs.HasKey("Tickets"))
            {
                PlayerPrefs.SetInt("Tickets", 0);
            }
            return PlayerPrefs.GetInt("Tickets");
        }
        set
        {
            PlayerPrefs.SetInt("Tickets", value);
            UpdateTicketsCountTxt();
        }
    }

    private void UpdateTicketsCountTxt()
    {
        ticketsCountTxt.text = Tickets.ToString();
    }

    public void HomeBtnClick()
    {
        SounClick();
        Invoke("LoadHomeScene", 0.15f);
    }

    private bool OnSound
    {
        get
        {
            if (PlayerPrefs.GetString("Sound").Equals("on") || !PlayerPrefs.HasKey("Sound")) { return true; }
            else { return false; }
        }
    }

    private void LoadHomeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void SounClick()
    {
        if (OnSound)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}
