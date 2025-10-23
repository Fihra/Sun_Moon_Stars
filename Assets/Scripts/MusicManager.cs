using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] AudioSource musicSource;

    [Header("Day Music")]
    [SerializeField] public AudioClip dayMusicClip;

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
        musicSource.clip = dayMusicClip;
        musicSource.Play();
        musicSource.loop = true;    

        lowPassFilter = GetComponent<AudioLowPassFilter>();
        cutOffFrequency = 5000f;
        //SafetyZoneFilter();
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

}
