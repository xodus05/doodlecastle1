using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusicPlay : MonoBehaviour
{
    private BGMManager bgmManager;
    public int stopMusicTrack;
    public int _playMusicTrack;

    // Start is called before the first frame update
    void Start()
    {
        bgmManager = FindObjectOfType<BGMManager>();
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bgmManager != null)
        {
            bgmManager.Stop(stopMusicTrack);
            this.gameObject.SetActive(false);
        }
        
    }
}
