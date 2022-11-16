using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    float time = 0;

    void Update()
    {
/*        if(time < 2f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time/2);
        }
        else
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;*/
    }

/*    public void resetAnim()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        this.gameObject.SetActive(true);
        time = 0;
    }




    public Image image; //������ ȭ��
    public GameObject button; //Ŭ���� ��ư

    public void Fadebutton()
    {
        Debug.Log("��ưŬ��");
        button.SetActive(false); //��ư�� Ŭ���ϸ� ��ư�� ��Ȱ��ȭ
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 0; //ó�� ���İ�
        while (fadeCount < 1.0f) //���� �ִ밪 1.0���� �ݺ�
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); //0.01�� ���� ����
            image.color = new Color(0, 0, 0, fadeCount); //�ش� ���������� ���İ� ����
        }
    }*/
}
