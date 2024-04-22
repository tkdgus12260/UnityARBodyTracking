using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountMessageItem : MonoBehaviour
{
    public TextMeshProUGUI MessageText;
    public Button RankingButton;

    public void UpdateMessage(string message, bool right, bool left)
    {
        MessageText.text = message;

        if (!right && !left)
        {
            RankingButton.gameObject.SetActive(true);
            RankingButton.onClick.AddListener(RankingRegistration);
            Debug.Log("랭킹 등록 가능.");
        }
        else
        {
            Debug.Log("랭킹 등록 불가능.");
        }
    }

    public void RankingRegistration()
    {
        Debug.Log("랭킹 등록.");
    }
}
