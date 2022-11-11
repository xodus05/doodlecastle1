using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerMove thePlayer;    // 이벤트 도중 키입력 처리 방지
    private List<MovingObject> characters;
    

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    public void PreLoadCharater() {
        characters = ToList();
    }

    public List<MovingObject> ToList() {
        List<MovingObject> ToList = new List<MovingObject>();
        MovingObject temp = FindObjectOfType<MovingObject>();

        for(int i = 0; i < temp.Length; i++) {
            tempList.Add(temp[i]);
        }

        return tempList; 
    }

    public void Move(string _name, string _dir) {
        for(int i = 0; i < characters.Count; i++) {
            // if(_name == characters[i].)
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
