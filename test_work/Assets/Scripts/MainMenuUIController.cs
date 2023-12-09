using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MainMenuUIController : MonoBehaviour
{

    [SerializeField]
    private GameObject currLayer;

    [Space]
    [Header("Tickets")]
    [SerializeField]
    private TextMeshProUGUI ticketsCountTxt;

    [Space]
    [Header ("Settings Menu")]
    public GameObject settLayer;
    [SerializeField]
    private GameObject settingsPanel;
    public Button settBackToMenu, settMusic, settSound;
    [SerializeField]
    private Sprite musicOn, musicOff, soundOn, soundOff;
    private Image musicImage, soundImage;


    [Space]
    [Header("Weekly Menu")]
    [SerializeField]
    private GameObject weeklyLayer;
    [SerializeField]
    private GameObject weeklyNotice;
    [SerializeField]
    private WeeklyBonusController weeklyBonusController;

    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip soundClip;
    private AudioSource audioSource;

    [SerializeField]
    private bool music, sound;

    void Start()
    {
        //PlayerPrefs.DeleteAll();

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.7f;

        musicImage = settMusic.GetComponent<Image>();
        soundImage = settSound.GetComponent<Image>();

        MusicSoundSettings();
        UpdateTicketsCountTxt();

        currLayer = null;
        settLayer.SetActive(false);
        weeklyLayer.SetActive(false);


        IsWeeklyNotice();
    }

    private void MusicSoundSettings()
    {
        music = OnMusic;//поменять на включение музыки
        if (OnMusic)
        {
            musicImage.sprite = musicOn;
        }
        else
        {
            musicImage.sprite = musicOff;
        }

        sound = OnSound;//поменять на включение звука
        if (OnSound)
        {
            soundImage.sprite = soundOn;
        }
        else
        {
            soundImage.sprite = soundOff;
        }
    }
   
    private bool OnMusic
    {
        get 
        {
            if (PlayerPrefs.GetString("Music").Equals("on") || !PlayerPrefs.HasKey("Music")) { return true; }
            else { return false; }
        }
        set 
        {
            if (value)
            {
                PlayerPrefs.SetString("Music", "on");
            }
            else
            {
                PlayerPrefs.SetString("Music", "off");
            }
            MusicSoundSettings();
        }
    }

    private bool OnSound
    {
        get
        {
            if (PlayerPrefs.GetString("Sound").Equals("on") || !PlayerPrefs.HasKey("Sound")) { return true; }
            else { return false; }
        }
        set
        {
            if (value)
            {
                PlayerPrefs.SetString("Sound", "on");
            }
            else
            {
                PlayerPrefs.SetString("Sound", "off");
            }
            MusicSoundSettings();
        }
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

    private void IsWeeklyNotice()
    {
        if (weeklyNotice != null)
        {
            if (DateTime.Now.Subtract(weeklyBonusController.LastClaim).Days >= 1)
            {
                //добавить анимацию
                weeklyNotice.SetActive(true);
                LeanTween.scale(weeklyNotice, new Vector3(1.3f, 1.3f, 1.3f), 1f).setEase(LeanTweenType.punch).setLoopClamp();
            }
            else
            {
                weeklyNotice.SetActive(false);
            }
        }
    }

    

    public void BackToMenu()//возвращает в меню с нынешнего окна
    {
        if (currLayer && currLayer.activeSelf)
        {
            currLayer.SetActive(false);
            currLayer = null;
        }
        IsWeeklyNotice();
    }

    public void SettingsMenuOn()//включает меню настроек
    {
        settLayer.SetActive(true);
        settingsPanel.transform.localScale = new Vector3(0f, 0f, 0f);
        LeanTween.scale(settingsPanel, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutElastic);
        currLayer = settLayer;
        SounClick();
    }

    public void WeeklyMenuOn()//включаем меню ежедневной награды
    {
        weeklyLayer.SetActive(true);
        currLayer = weeklyLayer;
        weeklyBonusController.Animated();
        SounClick();
    }

    public void MusicSettings()//вкл выкл музыку
    {
        if (OnMusic)
        { OnMusic = false; }
        else { OnMusic = true; }
        music = OnMusic;
        SounClick();
    }

    public void SoundSettings()//вкл выкл звуки
    {
        if (OnSound)
        { OnSound = false; }
        else { OnSound = true; }
        sound = OnSound;
        SounClick();
    }

    public void SounClick()
    {
        if (OnSound)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }

   public void PlayBtnClick()
    {
        SounClick();
        Invoke("LoadPlayScen",0.2f);
        //SceneManager.LoadScene("LevelScen");
    }

    private void LoadPlayScen()
    {
        SceneManager.LoadScene("LevelScen");
    }

    public void ShopBtnClick()
    {
        SounClick();
        Invoke("LoadShopScen", 0.2f);
        //SceneManager.LoadScene("ShopMenu");
    }

    private void LoadShopScen()
    {
        SceneManager.LoadScene("ShopMenu");
    }

}
