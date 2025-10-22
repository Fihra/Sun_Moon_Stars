using UnityEngine;

public class MoonBehavior : MonoBehaviour
{
    private LightingManager lightingManager;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // Keep the object in the scene but invisible at the start
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        gameObject.SetActive(true);
    }

    private void Start()
    {
        lightingManager = FindFirstObjectByType<LightingManager>();

        if (lightingManager != null)
        {
            lightingManager.OnDayTimeChanged += HandleDayTimeChanged;
        }
        else
        {
            Debug.LogError("MoonBehavior ERROR: No LightingManager found in the scene!");
        }
    }

    private void HandleDayTimeChanged(bool isDay)
    {
        // Moon is visible at night -> opposite of day
        bool isNight = !isDay;

        meshRenderer.enabled = isNight;
        gameObject.SetActive(isNight);
    }

    private void OnDestroy()
    {
        // Prevent memory leaks
        if (lightingManager != null)
        {
            lightingManager.OnDayTimeChanged -= HandleDayTimeChanged;
        }
    }
}
