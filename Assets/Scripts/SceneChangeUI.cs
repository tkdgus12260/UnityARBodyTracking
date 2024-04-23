using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeUI : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Manager.Instance.SceneManager.LoadScene(sceneName);
    }
}
