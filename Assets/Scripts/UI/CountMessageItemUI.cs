using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountMessageItemUI : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollViewContent;
    [SerializeField]
    private GameObject messageItemPrefab;
    public GameObject nickNamePanel;

    public GameObject warningPanel;
    public TextMeshProUGUI text;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        InitializeMessageItem(20, 0f, 0f, false, false);
    //    }
    //}

    // 새로운 messageitem 생성 및 업데이트 함수
    public void InitializeMessageItem(int count, float rightDistortion, float leftDistortion, bool right, bool left)
    {
        GameObject newMessageItem = Instantiate(messageItemPrefab, scrollViewContent.transform);

        CountMessageItem countMessageItem = newMessageItem.GetComponent<CountMessageItem>();
        countMessageItem.UpdateMessage(count, rightDistortion, leftDistortion, right, left);
    }

    // CountMessageItemUI 숨기기
    public void SetXPosition(float value)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 newPosition = rectTransform.localPosition;
        newPosition.x = value;
        rectTransform.localPosition = newPosition;
    }

    public void OnWarningPanel(string message)
    {
        warningPanel.SetActive(true);
        text.text = message;
    }
}
