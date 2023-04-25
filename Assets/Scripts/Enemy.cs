using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    private Inventory inventory;

    public Vector2 startPosition;

    public static Enemy instance;

    public GameObject monster;

    #region Singleton

    void Start() {
        startPosition = this.transform.position;
        inventory = FindObjectOfType<Inventory>();
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if(collision.gameObject.name == "Player" && !inventory.doing("불")) {
            // inventory.activeList.Add("불");
            SceneManager.LoadScene("Died"); //quote 로 scene 이동
          }
    }
}