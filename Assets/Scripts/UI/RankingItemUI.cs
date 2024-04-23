using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class RankingItemUI : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollViewContent;
    [SerializeField]
    private GameObject rankingItemPrefab;

    public async void OnRankingPanel()
    {
        gameObject.SetActive(true);

        string jsonContent = await Manager.Instance.s3Manager.RankingJson();

        List<RankingData> rankingList = JsonConvert.DeserializeObject<List<RankingData>>(jsonContent);

        rankingList.Sort((a, b) => b.count.CompareTo(a.count));

        foreach (RankingData data in rankingList)
        {
            InitializeMessageItem(data.nickName, data.count, data.imageURL);
        }
    }

    private void InitializeMessageItem(string nickName, int count, string url)
    {
        GameObject newRankingItem = Instantiate(rankingItemPrefab, scrollViewContent.transform);

        RankingItem rankingItem = newRankingItem.GetComponent<RankingItem>();

        string message = $"{nickName}님께서 {count}회 하셨습니다.";

        rankingItem.UpdateRanking(message, url);
    }
}
