using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class WeeklyBonusController : MonoBehaviour
{

    private DateTime currentTime;

    [SerializeField]
    private int lastClaimDay;

    [SerializeField]
    private bool canClaim;
    [Space]
    [SerializeField]
    private TextMeshProUGUI timeToNextReward;
    [SerializeField]
    private TextMeshProUGUI currentClaimDay;

    [Space]
    [Header("ProgressBar")]
    [SerializeField]
    private Image fillProgressBar;
    [SerializeField]
    private TextMeshProUGUI progressBarTxt;

    [Space]
    [Header("Weekly Bonus")]
    [SerializeField]
    private int[] weeklyBonus;
    [SerializeField]
    private TextMeshProUGUI currentBonusValue;
 
    [Space]
    [Header("Streak")]
    [SerializeField]
    private int maxStreak;
    [SerializeField]
    private int currentStreak;
    [SerializeField]
    private int currentBonus;

    [Space]
    [Header("View")]
    [SerializeField]
    private GameObject anyWeeklyBonusView;
    [SerializeField]
    private GameObject claimBonusView;
    [SerializeField]
    private GameObject AnimatedView;

    [Space]
    [Header("MainMenuController")]
    [SerializeField]
    private MainMenuUIController mainMenuUIController;

    [Space]
    [Header("Debug")]
    [SerializeField]
    private int addDays = 0;

    [Space]
    [Header("WeklyIcons")]
    [SerializeField]
    private GameObject[] weeklyIcons;
    private TextMeshProUGUI[] weeklyIconsValueTxt;
    private TextMeshProUGUI[] weeklyIconsDayTxt;
    private GameObject[] weeklyIconsAcceptedImage;

    void Start()
    {
        //LastClaim = new DateTime();
        InitializedWeeklyIconsValueTxt();
        AnimatedView.SetActive(true);
        UpdateTime();
        CanClaimRewardUpdate();
        //Animated();
    }

    public DateTime LastClaim
    {
        
        get
        {
            DateTime dateTime = new DateTime();
            if (!PlayerPrefs.HasKey("LastClaim"))
            {
                return dateTime;
            }
            else
            {
                return DateTime.Parse(PlayerPrefs.GetString("LastClaim"));
            }
        }
        set
        {
            PlayerPrefs.SetString("LastClaim", value.ToString());
        }
    }

    private int Streak
    {
        get
        {
            if (!PlayerPrefs.HasKey("Streak"))
            {
                PlayerPrefs.SetInt("Streak", 1);
            }
            else
            {
                if (PlayerPrefs.GetInt("Streak") < 1 || PlayerPrefs.GetInt("Streak") > maxStreak) 
                {
                    PlayerPrefs.SetInt("Streak", 1);
                }
            }
            return PlayerPrefs.GetInt("Streak");
        }

        set
        {
            if (value >= 1 )
            {
                if (value > maxStreak) {
                    PlayerPrefs.SetInt("Streak", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Streak", value);
                }
            }
        }
    }


    private void FixedUpdate()
    {
        currentStreak = Streak;
        currentBonus = weeklyBonus[Streak - 1];

        UpdateTime();
        UpdateFillBar();
    }

    private void UpdateTime()
    {
        currentTime = DateTime.Now.AddDays(addDays);

        UpdateTimeToNextrewardTxt();
        CanClaimRewardUpdate();
    }

   private void CanClaimRewardUpdate()
    {

        if (currentTime.Subtract(LastClaim).Days >= 1)
        {
            canClaim = true;
        }
        else
        {
            canClaim = false;
        }

        if (currentTime.Subtract(LastClaim).Days > 1)
        {
            Streak = 1;
        }

        ViewControllerUpdate();
    }

    private void ViewControllerUpdate()
    {
        anyWeeklyBonusView.SetActive(false);
        claimBonusView.SetActive(false);

       

        if (canClaim)
        {//вью сбора бонуса
            currentClaimDay.text = $"DAY {currentStreak}";
            claimBonusView.SetActive(true);
            anyWeeklyBonusView.SetActive(false);
            currentBonusValue.text = "X" + weeklyBonus[Streak - 1].ToString();
        }
        else
        {//вью недельных бонусов
            claimBonusView.SetActive(false);
            anyWeeklyBonusView.SetActive(true);
            DefindWeeklyIconsAccepted();
        }
    }

    public void Animated()
    {
        AnimatedView.transform.localScale = new Vector3(0f, 0f, 0f);
        LeanTween.scale(AnimatedView, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutElastic);
    }

    private void UpdateFillBar()
    {
        fillProgressBar.fillAmount = (float) (Streak-1) / maxStreak;

        progressBarTxt.text = $"{Streak - 1}/{maxStreak}";
    }

    private void UpdateTimeToNextrewardTxt()
    {
        TimeSpan sub = new TimeSpan(24,0,0).Subtract(currentTime.Subtract(LastClaim));
        
        string txt = $"{sub.Hours:D2}:{sub.Minutes:D2}:{sub.Seconds:D2}";
        timeToNextReward.text = $"[Come back in {txt} to collect your reward!]";

        TimeSpan zero = new TimeSpan(0, 0, 0);
        CanClaimRewardUpdate();
    }

    private void InitializedWeeklyIconsValueTxt()
    {
        weeklyIconsValueTxt = new TextMeshProUGUI[weeklyIcons.Length];
        weeklyIconsAcceptedImage = new GameObject[weeklyIcons.Length];
        weeklyIconsDayTxt = new TextMeshProUGUI[weeklyIcons.Length];
        if (weeklyIcons.Length > 0)
        {
            for (int i = 0; i < weeklyIcons.Length; i++) 
            {
                weeklyIconsValueTxt[i] = weeklyIcons[i].transform.Find("ValueTxt").gameObject.GetComponent<TextMeshProUGUI>();
                weeklyIconsAcceptedImage[i] = weeklyIcons[i].transform.Find("Accepted").gameObject;
                weeklyIconsDayTxt[i] = weeklyIcons[i].transform.Find("DayTxt").gameObject.GetComponent<TextMeshProUGUI>();
            }
        }

        for (int i = 0; i < weeklyIconsValueTxt.Length; i++) 
        {
            weeklyIconsValueTxt[i].text = weeklyBonus[i].ToString();
        }

        for (int i = 0; i < weeklyIconsDayTxt.Length; i++)
        {
            weeklyIconsDayTxt[i].text = $"DAY{i+1}";
        }

        DefindWeeklyIconsAccepted();
    }

    private void DefindWeeklyIconsAccepted()
    {
        for (int i = 0; i < weeklyIconsAcceptedImage.Length; i++)
        {
            if (i + 1 < Streak)
            {
                weeklyIconsAcceptedImage[i].SetActive(true);
            }
            else 
            {
                weeklyIconsAcceptedImage[i].SetActive(false);
            }
        }
    }

    public void ClaimReward()
    {
        if (canClaim)
        {
            mainMenuUIController.Tickets += weeklyBonus[Streak - 1];
            Streak += 1;
            LastClaim = currentTime;
            CanClaimRewardUpdate();
        }
    }

    
}
