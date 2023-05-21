using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    private Inventory inventory;

    public Vector2 startPosition;

    public static Monster instance;

    public GameObject monster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Died1");
        }
    }
}
