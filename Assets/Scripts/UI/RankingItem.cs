using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class RankingItem : MonoBehaviour
{
    public TextMeshProUGUI RankingText;
    public RawImage userImage;

    // 랭킹 업데이트
    public void UpdateRanking(string message, string url)
    {
        RankingText.text = message;
        StartCoroutine(GetTexture(url, userImage));
    }

    // 텍스쳐 파일 업로드 코루틴
    IEnumerator GetTexture(string imageURL, RawImage imageElement)
    {
        if (imageURL != "")
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                imageElement.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
        }
    }
}
