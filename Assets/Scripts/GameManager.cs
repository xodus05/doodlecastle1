using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    private PlayerMove thePlayer;
    public GameObject talkPanel;
    Objdata objData;
    public Text talkText;
    public int talkIndex;
    public GameObject scanObject;
    public bool isAction; //상태 변수값
    public Text text;
    public Animator animSprite;
    public Animator animDialogueWindow;
    public OrderManager theOrder;

    public static GameManager instance;

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

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
        theOrder = FindObjectOfType<OrderManager>();
    }
}
