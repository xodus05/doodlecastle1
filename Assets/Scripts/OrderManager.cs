using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerMove thePlayer;    // 이벤트 도중 키입력 처리 방지
    // private List<MovingObject> characters;
    private MovingObject character;
    

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    public void PreLoadCharacter() {
        character = ToList();
    }

    public MovingObject ToList() {
        // List<MovingObject> ToList = new List<MovingObject>();
        MovingObject temp = FindObjectOfType<MovingObject>();

        /*for(int i = 0; i < temp.Length; i++) {
            tempList.Add(temp[i]);
        }*/

        return temp; 
    }

    public void Move(string _name, string _dir) {   // 이동할 객체 이름과 이동할 방향
        /*for(int i = 0; i < characters.Count; i++) {
            // if(_name == characters[i].)
        }*/
        if(_name == character.characterName)
        {
            character.Move(_dir);
        }
    }

    public void SetTransparent(string _name)
    {
        if(_name == character.characterName)
        {
            character.gameObject.SetActive(false);  // 안보이게
        }
    }

    public void SetUnTransparent(string _name)
    {
        if (_name == character.characterName)
        {
            character.gameObject.SetActive(true);   // 보이개
        }
    }

    /*public void SetThrough(string _name)
    {
        if(_name == character.characterName)
        {
            character.boxCollider.enabled = false;  // 막힌 구역을 통과하도록
        }
    }

    public void SetUnThrough(string _name)
    {
        if (_name == character.characterName)
        {
            character.boxCollider.enabled = true;  // 막힌 구역을 통과하지 못하도록
        }
    }*/

    public void Turn(string _name, string _dir) // 객체 이름과 턴할 방향
    {
        if (_name == character.characterName)
        {
            character.animator.SetFloat("DirX", 0f);
            character.animator.SetFloat("DirY", 0f);
            switch (_dir)
            {
                case "UP" :
                    character.animator.SetFloat("DirY", 1f);
                    break;
                case "DOWN":
                    character.animator.SetFloat("DirY", -1f);
                    break;
                case "LEFT":
                    character.animator.SetFloat("DirX", -1f);
                    break;
                case "RIGHT":
                    character.animator.SetFloat("DirX", 1f);
                    break;
            }
        }
    }
}
