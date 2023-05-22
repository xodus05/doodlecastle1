using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class end1 : MonoBehaviour
{
    public Text text;
    public List<string> listSentences;

    private int count;
    public bool talking = false;
    private bool keyActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        text.text = "";
        listSentences = new List<string>();
    }

    IEnumerator NormalChat()
    {
        for (int i = 0; i < listSentences.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                text.text = "";     // text 초기화
                count++;
                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(NormalChat());
                }
            }
        }
    }
}
