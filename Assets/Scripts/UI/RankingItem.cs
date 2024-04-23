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

    public void UpdateRanking(string message, string url)
    {
        RankingText.text = message;
        StartCoroutine(GetTexture(url, userImage));
    }

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
