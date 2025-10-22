using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AgueoAnimation : MonoBehaviour
{
    private LightingManager lightingManager;
    private Animation animComponent;
    public string animIn= "AgueoIn";
    public string animOut = "AgueoOut";
    public string animIdle = "AgueoIdle";
    private float fadeTime = 1f;
 


    void Awake()
    { 
      
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animComponent = GetComponent<Animation>();

        lightingManager = FindFirstObjectByType<LightingManager>();


        if (lightingManager == null)
        {
            Debug.LogError("FATAL ERROR Could not find LightingManager script in the scene");
        }


        if (animComponent != null)
        {
           // if (animComponent[animIdle] != null)
              //  animComponent[animIdle].wrapMode = WrapMode.Loop;
        }

    }


// Update is called once per frame
void Update()
    {
        if (lightingManager == null || animComponent == null) return;

      
        // -----------------------------------------------------------
        // ANIMATION LOGIC (ONLY RUNS IF GAMEOBJECT IS ACTIVE)
        // -----------------------------------------------------------
        // Only run IN animation when DayTime STARTS
        if (lightingManager.DayTime)
        {
            // Transition from Night to Day (only runs on the first DayTime frame)

            // 1. Play the one-shot 'In' animation
            animComponent.CrossFade(animIn, fadeTime);

            // 2. Queue the 'Idle' loop to start AFTER 'AgueoIn' finishes
            animComponent.CrossFadeQueued(animIdle, fadeTime, QueueMode.CompleteOthers);

              }
        // Ensure the NightTime transition logic doesn't interfere with the DayTime loop
        else if (lightingManager.NightTime)
        {


        }
    }


}
