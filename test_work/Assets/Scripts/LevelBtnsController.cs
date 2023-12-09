using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelBtnsController : MonoBehaviour
{

    [SerializeField]
    private int BtnValue = 0;

    [SerializeField]
    private GameObject BtnTxt, BtnLockIcon;

    [SerializeField]
    private LevelScreenController levelScreenController;

    private Button lvlBtn;

    void Start()
    {
        lvlBtn = gameObject.GetComponent<Button>();

        BtnLockIcon.SetActive(true);
        BtnTxt.SetActive(false);
    }
    
    public void DefineBtnValue(int value, Transform tr)
    {
        levelScreenController = tr.GetComponent<LevelScreenController>();
        BtnValue = value;
        BtnTxt.GetComponent<TextMeshProUGUI>().text = $"{BtnValue}";
        DefineBtnLevelImageStart();
    }

    public void DefineBtnLevelImageStart()
    {
        if (BtnValue <= levelScreenController.CurrLevel + 1)
        {
            BtnLockIcon.SetActive(false);
            lvlBtn.interactable = true;
            BtnTxt.SetActive(true);
        }
        else
        {
            lvlBtn.interactable = false;
            BtnLockIcon.SetActive(true);
            BtnTxt.SetActive(false);
        }
    }

    public void DefineBtnLevelImageUpdate()
    {

        if (BtnValue <= levelScreenController.CurrLevel + 1)  
        {
            //BtnLockIcon.SetActive(false);
            AnimateBtnLockIcon();
            lvlBtn.interactable = true;
            BtnTxt.SetActive(true);
        }
        else
        {
            lvlBtn.interactable = false;
            BtnLockIcon.SetActive(true);
            BtnTxt.SetActive(false);
        }
    }

    private void AnimateBtnLockIcon()
    {
        LeanTween.alpha(BtnLockIcon.GetComponent<Image>().rectTransform, 0.0f, 0.6f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(BtnLockIcon, new Vector3(1.6f, 1.6f, 1.6f), 0.6f).setEase(LeanTweenType.easeOutCubic).setOnComplete(BtnLockIconACtiveFalse);
    }

    private void BtnLockIconACtiveFalse()
    {
        BtnLockIcon.SetActive(false);
        BtnLockIcon.transform.localScale = new Vector3(1f, 1f, 1f);
    }
   

    public void BtnClick()
    {
        if (BtnValue > 0)
        {
            levelScreenController.LevelBtnClick(BtnValue);
        }
    }

    
}
