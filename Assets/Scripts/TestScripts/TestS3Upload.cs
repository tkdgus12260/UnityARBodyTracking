using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.S3;
using Amazon.CognitoIdentity;
using System;
using System.Threading.Tasks;
using Amazon.S3.Model;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;
using Amazon.S3.Transfer;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;

public class TestS3Upload : MonoBehaviour
{
    private string _bucketName = "kshs3test";
    private string _folderName = "UploadTest";

    [HideInInspector]
    public string ImageURL = string.Empty;

    public RawImage rawImage;
    public Text text;

    private IAmazonS3 s3Client;
    private TransferUtility transferUtil;

    private void Start()
    {
        s3Client = new AmazonS3Client(new CognitoAWSCredentials("ap-northeast-2:04d7b7ad-527e-417c-b876-43ad9dc193df", RegionEndpoint.APNortheast2), RegionEndpoint.APNortheast2);
        transferUtil = new TransferUtility(s3Client);
    }
    private void OnEnable()
    {
        // Application.logMessageReceived 이벤트에 이벤트 핸들러를 추가하여 콘솔 메시지를 캡처
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        // 이벤트 핸들러를 제거하여 누수를 방지합니다.
        Application.logMessageReceived -= HandleLog;
    }

    // 이벤트 핸들러: 콘솔에 출력되는 메시지를 캡처하고 UI Text에 표시
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // 콘솔에 출력되는 모든 메시지를 캡처하여 UI Text에 추가합니다.
        text.text += logString + "\n";
    }

    public void TestUpload()
    {
        //사진과 동영상들 중 선택 가능
        NativeGallery.GetMixedMediaFromGallery((media) => {

            FileInfo selectedMedia = new FileInfo(media);

            if (!string.IsNullOrEmpty(media))
            {
                if (media.Substring(media.Length - 3, 3) == "mp4" || media.Substring(media.Length - 3, 3) == "mov")
                {
                    Debug.Log("영상 파일은 선택할 수 없습니다.");
                }
                else
                {
                    UploadFileAsync(s3Client, _bucketName + "/" + _folderName, selectedMedia.Name, selectedMedia.FullName);
                }
            }
        }, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video);

    }

    // 경로 내 파일 s3에 업로드.
    public async void UploadFileAsync(
        IAmazonS3 client,
        string bucketName,
        string objectName,
        string filePath
        )
    {
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = objectName,
            FilePath = filePath,
            CannedACL = S3CannedACL.PublicRead,

        };
        Debug.Log("KAI_KSH awit");
        var response = await client.PutObjectAsync(request);
        Debug.Log("KAI_KSH awit Complet");

        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            Debug.Log($"Successfully uploaded {objectName} to {bucketName}.");
            StartCoroutine(GetTexture($"https://kshs3test.s3.ap-northeast-2.amazonaws.com/{_folderName}/" + objectName, rawImage));
            return;
        }
        else
        {
            Debug.Log($"Could not upload {objectName} to {bucketName}.");
            return;
        }
    }

    IEnumerator GetTexture(string imageURL, RawImage imageElement)
    {
        Debug.Log("KSH : " + imageURL);
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
