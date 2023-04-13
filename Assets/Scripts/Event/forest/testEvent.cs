using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testEvent : MonoBehaviour
{

    private Inventory inventory;

    public string transferMapName; //이동할 맵의 이름
    public int startPointNumber;

    

    private FadeManager theFade;
    private DialogueManager theDM;
    private ChoiceManager theChoice;
    private OrderManager theOrder;
    private PlayerMove thePlayer;

    private Enemy theEnemy;
    private EnemyAI theEnemyAI;
    private CameraManager theCamera;

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theOrder = FindObjectOfType<OrderManager>();
        inventory = FindObjectOfType<Inventory>();
        theFade = FindObjectOfType<FadeManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theEnemy = FindObjectOfType<Enemy>();
        theEnemyAI = FindObjectOfType<EnemyAI>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        inventory.activeList.Add("불");
        theEnemy.transform.position = new Vector2(-819, 452);
        theEnemyAI.transform.position = new Vector2(-819, 452);
        // thePlayer.transform.position = new Vector2(theEnemy.startPosition.x+200, theEnemy.startPosition.y-200);
        thePlayer.transform.position = new Vector2(40, 390);
        theCamera.transform.position = new Vector2(40, 390);
        // theCamera.transform.position = new Vector2(theEnemy.startPosition.x+200, theEnemy.startPosition.y-200);
        flag = false;
        yield return new WaitForSeconds(1.0f);
        if(thePlayer.getSceneName()!="map5") {
            SceneManager.LoadScene("map5");
        }
        else {
            Debug.Log("in map5");
        }
    }
}