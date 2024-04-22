using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMessageItemUI : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollViewContent;
    [SerializeField]
    private GameObject messageItemPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            InitializeMessageItem();
        }
    }

    // 새로운 messageitem 생성 및 업데이트 함수 실행
    public void InitializeMessageItem()
    {
        GameObject newMessageItem = Instantiate(messageItemPrefab, scrollViewContent.transform);

        CountMessageItem countMessageItem = newMessageItem.GetComponent<CountMessageItem>();
        countMessageItem.UpdateMessage("턱걸이 10회 하셨습니다.", false, false);
    }
}
