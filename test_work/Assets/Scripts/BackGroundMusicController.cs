using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicController : MonoBehaviour
{

    [Header("Tag")]
    [SerializeField]
    private string musicTag;

    void Awake()
    {
        GameObject obj = GameObject.FindWithTag(musicTag);
        if (obj != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.tag = musicTag;
            DontDestroyOnLoad(gameObject);
        }
    }


}
