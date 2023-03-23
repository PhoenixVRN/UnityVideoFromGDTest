using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class VideoDownloader
{
    public IEnumerator GetVideo(string url, string path)
    {
        Debug.Log("Downloading video from " + url);
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        DownloadHandler downloadHandler = unityWebRequest.downloadHandler;
        yield return unityWebRequest.SendWebRequest();
        while (!downloadHandler.isDone)
        {
            yield return null;
        }
        
        if (unityWebRequest.downloadHandler.error != string.Empty)
        {
            Debug.LogError(unityWebRequest.downloadHandler.error);
        }
        else
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllBytes(path, downloadHandler.data);
            Debug.Log("Video downloaded to " + path);
        }
    }
}