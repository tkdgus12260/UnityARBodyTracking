using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class CountMessageItem : MonoBehaviour
{
    public TextMeshProUGUI MessageText;
    public Button RankingButton;
    private CountMessageItemUI countMessageItemUI = null;
    private TMP_InputField inputField = null;

    private int count;

    private void Awake()
    {
        countMessageItemUI = GetComponentInParent<CountMessageItemUI>();
    }

    public void UpdateMessage(int count, float rightDistortion, float leftDistortion, bool right, bool left)
    {
        string errorMessage;

        errorMessage = $"풀업 {count}회 하셨습니다.";

        if (!right && !left)
        {
            RankingButton.gameObject.SetActive(true);
            this.count = count;
            RankingButton.onClick.AddListener(OnNickName);
        }
        if(right)
        {
            errorMessage += $"\n오른쪽으로 {rightDistortion}도 만큼 기울었습니다.";
        }
        if (left)
        {
            errorMessage += $"\n왼쪽으로 {leftDistortion}도 만큼 기울었습니다.";
        }

        MessageText.text = errorMessage;
    }

    public void OnNickName()
    {
        countMessageItemUI.nickNamePanel.SetActive(true);
        countMessageItemUI.nickNamePanel.GetComponentInChildren<Button>().onClick.AddListener(NickNameInput);
    }

    public void NickNameInput()
    {
        if (inputField == null)
            inputField = countMessageItemUI.nickNamePanel.GetComponentInChildren<TMP_InputField>();

        string nickName = inputField.text;

        if(nickName != string.Empty)
        {
            if (Regex.IsMatch(nickName, @"^[a-zA-Z0-9]+$"))
            {
                RankingRegistration(nickName, this.count);
                RankingButton.gameObject.SetActive(false);
                countMessageItemUI.nickNamePanel.SetActive(false);
            }
            else
            {
                Debug.Log("알파벳과 숫자로만 입력해주세요.");
            }
        }
        else
        {
            Debug.Log("닉네임을 입력해주세요.");
        }
    }

    public void RankingRegistration(string nickName, int count)
    {
        Manager.Instance.s3Manager.RankingPhotoRegistration(nickName);

        RankingData newData = new RankingData();
        newData.nickName = nickName;
        newData.count = count;
        newData.imageURL = Manager.Instance.s3Manager.ImageURL;

        Manager.Instance.s3Manager.UpdateRankingJson(newData);

        Manager.Instance.s3Manager.ImageURL = string.Empty;
    }
}
