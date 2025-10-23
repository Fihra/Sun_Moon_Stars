using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    //[SerializeField] AudioSource musicSource;

    [SerializeField] AudioSource kulintangLayer;
    [SerializeField] AudioSource laudsLayer;
    [SerializeField] AudioSource percussionLayer;

    public float fadeDuration = 1.0f;

    //[Header("Day Music")]
    //[SerializeField] public AudioClip dayMusicClip;


    [SerializeField] public AudioClip kulintangClip;
    [SerializeField] public AudioClip rondallaClip;
    [SerializeField] public AudioClip percussionClip;


    private AudioLowPassFilter lowPassFilter;

    private float cutOffFrequency;
    public float resonanceQ = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //musicSource.clip = dayMusicClip;
        //musicSource.Play();
        //musicSource.loop = true;
        //musicSource.volume = 0;

        kulintangLayer.clip = kulintangClip;
        laudsLayer.clip = rondallaClip;
        percussionLayer.clip = percussionClip;
        
        kulintangLayer.Play();
        kulintangLayer.volume = 0f;
        laudsLayer.volume = 1f;
        laudsLayer.Play();
        percussionLayer.Play();

        kulintangLayer.loop = true;
        laudsLayer.loop = true;
        percussionLayer.loop = true;


        lowPassFilter = GetComponent<AudioLowPassFilter>();
        cutOffFrequency = 5000f;
        ApplyLowPassFilterSettings();
    }

    public void ApplyLowPassFilterSettings()
    {
        if (lowPassFilter != null)
        {
            lowPassFilter.cutoffFrequency = cutOffFrequency;
            lowPassFilter.lowpassResonanceQ = resonanceQ;
        }
    }

    public void SafetyZoneFilter()
    {
        cutOffFrequency = 1500f;
        ApplyLowPassFilterSettings();
    }

    public void NotSafetyZoneFilter()
    {
        cutOffFrequency = 5000f;
        ApplyLowPassFilterSettings();
    }

    public void FadeIn(CurrentDay currentDay)
    {
        switch(currentDay)
        {
            case CurrentDay.Day:
                StartCoroutine(FadeAudio(kulintangLayer, 0f, 1f, fadeDuration));
                break;
            case CurrentDay.Night:
                StartCoroutine(FadeAudio(laudsLayer, 0f, 1f, fadeDuration));
                break;
            default:
                break;

        }
        
    }

    public void FadeOut(CurrentDay currentDay)
    {
        switch(currentDay)
        {
            case CurrentDay.Day:
                StartCoroutine(FadeAudio(kulintangLayer, 1f, 0f, fadeDuration));
                break;
            case CurrentDay.Night:
                StartCoroutine(FadeAudio(laudsLayer, 1f, 0f, fadeDuration));
                break;
            default:
                break;

        }
    }

    private IEnumerator FadeAudio(AudioSource source, float startVolume, float endVolume, float duration)
    {
        float timer = 0f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, endVolume, timer / duration);
            yield return null;
        }
        source.volume = endVolume;
  
    }

}
