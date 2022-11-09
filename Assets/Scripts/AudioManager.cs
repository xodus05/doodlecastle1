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
}



public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] sounds;

    void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("���� ���� �̸� : " + i + " = " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
