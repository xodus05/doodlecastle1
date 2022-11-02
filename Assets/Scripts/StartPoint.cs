using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; //���� �̵��Ҷ� �÷��̾��� ���� ���
    public int startPointNumber;
    private PlayerMove thePlayer;
    private CameraManager theCamera;
   

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerMove>();

        if(startPoint == thePlayer.currentMapName && startPointNumber == thePlayer.startPointNumber)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = this.transform.position;
        }
    }
}
