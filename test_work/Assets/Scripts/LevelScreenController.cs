using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenController : MonoBehaviour
{

    [Header ("Levels")]
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    private int currentLevl;


    [Space]
    [Header("LevelRoad")]
    [SerializeField]
    private GameObject levelRoad;

    [Space]
    [Header("LevelBtns")]
    [SerializeField]
    private GameObject[] levelButtons;

    [Header("Btn Click Sound")]
    [Space]
    [SerializeField]
    private AudioClip soundClip;
    private AudioSource audioSource;



    void Start()
    {
        //PlayerPrefs.SetInt("currLevel", 0);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.7f;

        currentLevl = CurrLevel;

        InitializedLevelBtns();
    }

    public int CurrLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("currLevel"))
            {
                PlayerPrefs.SetInt("currLevel", 0);
            }
            return PlayerPrefs.GetInt("currLevel");
        }

        set
        {
            if (value >= 0 && value < maxLevel + 1) 
            {
                PlayerPrefs.SetInt("currLevel", value);
                currentLevl = PlayerPrefs.GetInt("currLevel");
            }
        }
    }


    private void InitializedLevelBtns()
    {
        if (levelRoad!= null) 
        {
            maxLevel = levelRoad.transform.childCount;
            levelButtons = new GameObject[maxLevel];
            for (int i = 0; i < maxLevel; i++) 
            {
                if (levelButtons[i] == null) {
                    levelButtons[i] = levelRoad.transform.GetChild(i).gameObject;
                    levelButtons[i].GetComponent<LevelBtnsController>().DefineBtnValue(i + 1, this.transform);
                }
            }

        }
    }

    private void UpdateBtnsImage()
    {
        for (int i = 0; i < maxLevel; i++)
        {
            levelButtons[i].GetComponent<LevelBtnsController>().DefineBtnLevelImageUpdate();
        }
    }


    public void LevelBtnClick(int btnClickValue)
    {
        SounClick();
        if (btnClickValue - 1 == CurrLevel) 
        {
            CurrLevel += 1;
            UpdateBtnsImage();
        }
    }

    public void HomeBtnClick()
    {
        SounClick();
        Invoke("LoadHomeScene", 0.12f);
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
