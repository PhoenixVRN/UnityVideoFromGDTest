using System.Collections;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoTest : MonoBehaviour
{
    [SerializeField] private GameObject _progressObject;
    private VideoPlayer _player;

    private const string URL = "https://drive.google.com/uc?export=download&id=1G-JfMHreE2sj2xanlqTcBrWLXFW72l1-";
    private string _path;
    private bool _isDownloading;
    
    private void Start()
    {
        _progressObject.SetActive(false);
        _path = Application.persistentDataPath + "/video.mp4";
        _player = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isDownloading)
        {
            // StartCoroutine(Interact());
            InteractAsync();
        }
    }

    private IEnumerator Interact()
    {
        _isDownloading = true;
        _progressObject.SetActive(true);
        var videoDownloader = new VideoDownloader();
        yield return videoDownloader.GetVideo(URL, _path);
        _progressObject.SetActive(false);
        _player.url = _path;
        _player.Play();
        _isDownloading = false;
    }

    private async void InteractAsync()
    {
        _isDownloading = true;
        _progressObject.SetActive(true);
        
        var videoDownloaderAsync = new VideoDownloaderAsync();
        await videoDownloaderAsync.GetVideoAsync(URL, _path);
        
        _progressObject.SetActive(false);
        _player.url = _path;
        _player.Play();
        _isDownloading = false;
    }
}
