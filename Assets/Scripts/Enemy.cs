using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    public EnemyAI enemyai;

    public void Start()
    {
        enemyai = GetComponent<EnemyAI>();
    }

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

     private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            //enemyai.isStopped = true;
            //transform.position = enemyai.startPosition;
            //enemyai.isStopped = false;
            SceneManager.LoadScene("Died"); //quote 로 scene 이동
          }
    }
}
