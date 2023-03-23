using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class VideoDownloaderAsync
{
    public async Task GetVideoAsync(string url, string path)
    {
        using var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);
        byte[] bytes = await response.Content.ReadAsByteArrayAsync();
        if (response.Content.Headers.ContentLength != null)
        { 
            var contentLength = (int) response.Content.Headers.ContentLength;
            Debug.Log(response.Content.Headers.ContentDisposition.FileName);
            var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            await fileStream.WriteAsync(bytes, 0, contentLength);
            await fileStream.DisposeAsync();
        }
    }
}