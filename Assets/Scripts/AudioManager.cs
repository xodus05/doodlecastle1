using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{

    public string name; // ������ �̸�
    public AudioClip clip; // ���� ����
    private AudioSource source; // ���� �÷��̾�

    public float Volumn;
    public bool loop;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play() {
        source.Play();
    }
}



public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    #region Singleton
    private void Awake() {
         if(instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
         }
         else {
            Destroy(this.gameObject);
         }
    }
    #endregion Singleton
    [SerializeField]
    public Sound[] sounds;

    void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + " = " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void Play(string _name) {
        for(int i = 0; i<sounds.Length; i++) {
            if(_name == sounds[i].name) {
                sounds[i].Play();
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
