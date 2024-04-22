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
using UnityEngine.Video;

public class S3Manager : MonoBehaviour
{
    private string folderPath;
    private string _bucketName = "kshs3test";
    private string _folderName = "UploadTest";

    private void Awake()
    {
        folderPath = Application.persistentDataPath;
    }

    private IAmazonS3 s3Client;
    private TransferUtility transferUtil;

    private void Start()
    {
        s3Client = new AmazonS3Client(new CognitoAWSCredentials("ap-northeast-2:04d7b7ad-527e-417c-b876-43ad9dc193df", RegionEndpoint.APNortheast2), RegionEndpoint.APNortheast2);
        transferUtil = new TransferUtility(s3Client);
    }

    public void PlayVideo()
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

    public void DeleteButton()
    {
        DeleteObjectNonVersionedBucketAsync(s3Client, _bucketName, "sampleUserID/");
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
            return;
        }
        else
        {
            Debug.Log($"Could not upload {objectName} to {bucketName}.");
            return;
        }
    }

    // 파일 삭제
    public async void DeleteObjectNonVersionedBucketAsync(IAmazonS3 client, string bucketName, string keyName)
    {
        try
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
            };

            Debug.Log($"Deleting object: {keyName}");
            await client.DeleteObjectAsync(deleteObjectRequest);
            Debug.Log($"Object: {keyName} deleted from {bucketName}.");
        }
        catch (AmazonS3Exception ex)
        {
            Debug.Log($"Error encountered on server. Message:'{ex.Message}' when deleting an object.");
        }
    }
}