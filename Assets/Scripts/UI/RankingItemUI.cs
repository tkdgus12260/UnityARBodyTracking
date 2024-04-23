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

    private Dictionary<string, GameObject> rankingItemMap = new Dictionary<string, GameObject>();

    // 랭킹 최신화 함수
    public async void OnRankingPanel()
    {
        gameObject.SetActive(true);

        string jsonContent = await Manager.Instance.s3Manager.DownloadRankingJson();

        List<RankingData> rankingList = JsonConvert.DeserializeObject<List<RankingData>>(jsonContent);

        rankingList.Sort((a, b) => b.count.CompareTo(a.count));

        int rank = 0;

        foreach (RankingData data in rankingList)
        {
            if (!rankingItemMap.ContainsKey(data.nickName))
            {
                rank++;
                InitializeMessageItem(data.nickName, data.count, data.imageURL, rank);
            }
        }
    }

    // 랭킹 콘텐츠 프리팹 생성 함수
    private void InitializeMessageItem(string nickName, int count, string url, int rank)
    {
        GameObject newRankingItem = Instantiate(rankingItemPrefab, scrollViewContent.transform);

        RankingItem rankingItem = newRankingItem.GetComponent<RankingItem>();

        string message = $"★ {rank} ★  {nickName}님께서 {count}회 하셨습니다.";

        rankingItem.UpdateRanking(message, url);
        rankingItemMap.Add(nickName, newRankingItem);
    }
}
