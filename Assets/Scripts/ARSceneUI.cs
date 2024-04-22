using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSceneUI : MonoBehaviour
{
    public void MessageBtn()
    {

    }

    public void BackBtn()
    {
        Manager.Instance.SceneManager.LoadScene("MainScene");
    }
}
