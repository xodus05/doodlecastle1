using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test4 : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;
    public string phone;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private Inventory inventory;
    private controlEvent control;
    private AudioManager theAudio;
    private CameraManager theCamera;
    private PlayerMove thePlayer;

    BoxCollider2D boxCollider;

    private bool flag;
    private bool flag2;


    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
        control = FindObjectOfType<controlEvent>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theAudio = FindObjectOfType<AudioManager>();
        theCamera = FindObjectOfType<CameraManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !flag && this.gameObject.ToString() == thePlayer.scanObject.ToString())
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        theOrder.NotMove();
        Panel3.SetActive(true);
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        Panel.SetActive(false);
        theAudio.Play(phone);

        yield return new WaitForSeconds(3f);
        theAudio.Stop(phone);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Move("player", "LEFT");
        theOrder.Move("player", "LEFT");
        theOrder.Move("player", "DOWN");
        theOrder.Move("player", "DOWN");
        theOrder.Move("player", "DOWN");
        theOrder.Move("player", "DOWN");
        theOrder.Move("player", "DOWN");
        theOrder.Move("player", "DOWN");
        theCamera.target = Panel2;
    }

}
