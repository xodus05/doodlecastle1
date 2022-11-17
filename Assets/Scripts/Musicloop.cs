using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicloop : MonoBehaviour
{
    static public Musicloop instance;
    AudioSource backmusic;
    GameObject bgmMusic;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        backmusic = GetComponent<AudioSource>();
        this.gameObject.SetActive(true);
    }

    void Start()
    {
        if (backmusic.isPlaying && backmusic != null) return;
        else
        {
            backmusic.Play();
            DontDestroyOnLoad(bgmMusic);
        }
    }
}
