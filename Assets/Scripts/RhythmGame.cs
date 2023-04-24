using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGame : MonoBehaviour
{
    public SpriteRenderer up;
    public SpriteRenderer down;
    public SpriteRenderer right;
    public SpriteRenderer left;
    public SpriteRenderer UpArrow;
    public SpriteRenderer downArrow;
    public SpriteRenderer rightArrow;
    public SpriteRenderer leftArrow;
    private Color color;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    private WaitForSeconds waitTime2 = new WaitForSeconds(0.5f);
    private System.Random rand = new System.Random();

    public int correctNumber = 0;  // 정답

    public void createTile() {
        int r = rand.Next(1, 5); 
        correctNumber *= 10;
        correctNumber += r;
        StartCoroutine(createTileCoroutine(r));
    }

    IEnumerator createTileCoroutine(int r) {
        Debug.Log(r);
        switch(r) {
            case 1 :
                Debug.Log("위");
                FadeOut(up);
                FadeOut(UpArrow);
                yield return waitTime2;
                FadeIn(up);
                FadeIn(UpArrow);
                break;
            case 2 :
                Debug.Log("아래");
                FadeOut(down);
                FadeOut(downArrow);
                yield return waitTime2;
                FadeIn(down);
                FadeIn(downArrow);
                break;
            case 3 : 
                Debug.Log("오른");
                FadeOut(right);
                FadeOut(rightArrow);
                yield return waitTime2;
                FadeIn(right);
                FadeIn(rightArrow);
                break;
            case 4 :
                Debug.Log("왼");
                FadeOut(left);
                FadeOut(leftArrow);
                yield return waitTime2;
                FadeIn(left);
                FadeIn(leftArrow);
                break;
        }
    }

    public void FadeOut(SpriteRenderer arrow, float _speed = 0.05f) {
        StartCoroutine(FadeOutCoroutine(arrow, _speed));
    }

    IEnumerator FadeOutCoroutine(SpriteRenderer arrow, float _speed) {
        color = arrow.color;
        while(color.a < 1f) {
            color.a += _speed;
            arrow.color = color;
            yield return waitTime;
        }
    }

    public void FadeIn(SpriteRenderer arrow, float _speed = 0.1f) {
        StartCoroutine(FadeInCoroutine(arrow, _speed));
    }

    IEnumerator FadeInCoroutine(SpriteRenderer arrow, float _speed) {
        color = arrow.color;
        while(color.a > 0f) {
            color.a -= _speed;
            arrow.color = color;
            yield return waitTime;
        }
    }
}
