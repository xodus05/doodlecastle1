using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("house"); //house �� scene �̵�
    }

    public void OnClickExit()
    {
        Application.Quit(); // �� ������
        System.Diagnostics.Process.GetCurrentProcess().Kill(); // omg �̰� ������ ����Ƽ �ƿ� ������..��������������������
    }
}
