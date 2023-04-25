using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map1event : MonoBehaviour
{
    [SerializeField]
    public Dialogue Dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private PlayerMove thePlayer;
    private BGMManager BGM;

    private static bool flag;
    private static bool flag2;
    private static bool isOK;

    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theChoice = FindObjectOfType<ChoiceManager>();
        BGM = FindObjectOfType<BGMManager>();
        if(isOK) Panel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            BGM.Stop(1);
            BGM.FadeInMusic();
            BGM.Play(2);
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter(); // 리스트 채우기

        theOrder.NotMove();

        yield return new WaitForSeconds(0.5f);
        thePlayer.walkCount = 30;
        theOrder.Move("player", "UP");
        theOrder.Move("player", "UP");
        yield return new WaitUntil(()=>thePlayer.queue.Count == 0);
        Panel.SetActive(true);
        isOK = true;
        theDM.ShowDialogue(Dialogue_1);
        yield return new WaitUntil(()=>!theDM.talking);
        theOrder.Move();
    }
}
