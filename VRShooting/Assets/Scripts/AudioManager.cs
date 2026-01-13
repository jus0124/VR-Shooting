using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 제거
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayBGM(string filePath)
    {
        StartCoroutine(LoadAndPlay(filePath));
    }

    private IEnumerator LoadAndPlay(string filePath)
    {
        string fullPath = $"file://{filePath}";

        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.MPEG))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                audioSource.clip = clip;
                audioSource.loop = true; // BGM이므로 반복 재생
                audioSource.Play();
                Debug.Log($"재생 중: {filePath}");
            }
            else
            {
                Debug.LogError($"BGM 로드 실패: {request.error}");
            }
        }
    }
}
