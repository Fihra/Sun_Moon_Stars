using System;
using System.Diagnostics;
using UnityEngine;

public enum CurrentDay { Day, Night };

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public static LightingManager Instance { get; private set; }
    public static CurrentDay currentDay = CurrentDay.Night;

    public static int dayCounter = 0;
    public static int nightCounter = 1;

    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    private float startHour;
    public bool NightTime;

    //Store the state of the last frame
    private bool wasDayTimeLastFrame;

    public event Action<bool> OnDayTimeChanged;
    private bool dayTime;

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

    public bool DayTime
    {
        get => dayTime;
        set
        {
            dayTime = value;
            OnDayTimeChanged?.Invoke(dayTime);
        }
    }

    public CurrentDay GetCurrentDay()
    {
        return currentDay;
    }

    public void SetCurrentDay(CurrentDay newDay)
    {
        currentDay = newDay;
    }

    void Start()
    {
        // 18.0f is the exact start of night, so 19.0f makes it look fully night.
        TimeOfDay = 19.0f;

        //set the initial lighting 
        UpdateLighting(TimeOfDay / 24f);
        ScoreManager.Instance.ChangeTextUI(ScoreManager.ChangeText.changeDay, $"Day: {dayCounter}");
        ScoreManager.Instance.ChangeTextUI(ScoreManager.ChangeText.changeNight, $"Night: {nightCounter}");
    }


    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            //(Replace with a reference to the game time)
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);

        }
        // Always update lighting, whether playing or editing
        UpdateLighting(TimeOfDay / 24f);

        // Logic to define DayTime 
        if (TimeOfDay >= 6 && TimeOfDay < 18)
        {
            DayTime = true;
            SetCurrentDay(CurrentDay.Day);
        }
        else
        {
            DayTime = false;
            SetCurrentDay(CurrentDay.Night);
        }

        // Set NightTime as the opposite of DayTime
        NightTime = !DayTime;

        if (Application.isPlaying)
        {
            // Checks if the current state is different from the last frame's state
            if (DayTime != wasDayTimeLastFrame)
            {
                // Log only once when the state has flipped
                if (DayTime)
                {
                    dayCounter++;
                    ScoreManager.Instance.ChangeTextUI(ScoreManager.ChangeText.changeDay, $"Day: {dayCounter}");
                    UnityEngine.Debug.Log("It is now DAYTIME!");
                    MusicManager.Instance.FadeIn(CurrentDay.Day);
                    MusicManager.Instance.FadeOut(CurrentDay.Night);
                    SFXManager.Instance.PlaySFX("daySFXCue");
                    Hut[] allHuts = FindObjectsByType<Hut>(FindObjectsSortMode.None);
                    foreach(Hut hut in allHuts)
                    {
                        hut.NewLoot();
                    }
                }
                else
                {
                    nightCounter++;
                    ScoreManager.Instance.ChangeTextUI(ScoreManager.ChangeText.changeNight, $"Night: {nightCounter}");
                    UnityEngine.Debug.Log("It is now NIGHTTIME!");
                    SFXManager.Instance.PlaySFX("nightSFXCue");
                    MusicManager.Instance.FadeIn(CurrentDay.Night);
                    MusicManager.Instance.FadeOut(CurrentDay.Day);
                    Hut[] allHuts = FindObjectsByType<Hut>(FindObjectsSortMode.None);
                    foreach (Hut hut in allHuts)
                    {
                        hut.NewLoot();
                    }
                }
            }

            // 3. Update the 'wasDayTimeLastFrame' variable for the NEXT frame's comparison
            wasDayTimeLastFrame = DayTime;
        }

    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight == null) return;
        //If the directional light is set then rotate and set its color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 0, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            //Light[] lights = GameObject.FindObjectsByType(FindObjectsSortMode.None);
            Light[] lights = GameObject.FindObjectsByType<Light>(FindObjectsSortMode.None);
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}