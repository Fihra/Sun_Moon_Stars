using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [SerializeField] AudioSource sfxAudioSource;

    [Header("Looting SFX")]
    [SerializeField] public AudioClip looting;
    [Header("Drop Off Loot SFX")]
    [SerializeField] public AudioClip dropOffLoot;
    [Header("Day Cue SFX")]
    [SerializeField] public AudioClip dayCue;
    [Header("Night Cue SFX")]
    [SerializeField] public AudioClip nightCue;

    [Range(1f,400f)]
    [SerializeField] float lowPitchRange = 1f;
    [Range(1f, 400f)]
    [SerializeField] float highPitchRange = 300f;

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

    public void PlaySFX(string sfxName, float volume = 0.5f)
    {
        if(sfxAudioSource != null)
        {
            switch (sfxName)
            {
                case "looting":
                    sfxAudioSource.clip = looting;
                    break;
                case "dropOffLoot":
                    sfxAudioSource.clip = dropOffLoot;
                    break;
                case "daySFXCue":
                    sfxAudioSource.clip = dayCue;
                    break;
                case "nightSFXCue":
                    sfxAudioSource.clip = nightCue;
                    break;
                default:
                    sfxAudioSource = null;
                    break;
            }
            sfxAudioSource?.Play();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
