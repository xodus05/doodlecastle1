using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCastle : MonoBehaviour
{

    //public Dialogue Dialogue_1;
    //public Dialogue Dialogue_2;

    //private Dialogue theDM;
    private OrderManager theOrder;
    private PlayerMove thePlayer;   // DirY == 1f

    // Start is called before the first frame update
    void Start()
    {
        // theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
