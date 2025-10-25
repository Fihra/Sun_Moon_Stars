using UnityEngine;

public class SunBehavior : MonoBehaviour
{
    private LightingManager lightingManager;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // Keep GameObject active, but hide visual
        meshRenderer = GetComponent<MeshRenderer>();
        gameObject.SetActive(true);
        meshRenderer.enabled = false;
    }

    void Start()
    {
        lightingManager = FindFirstObjectByType<LightingManager>();
        if (lightingManager != null)
        {
            lightingManager.OnDayTimeChanged += HandleDayTimeChanged;
        }
    }

    private void HandleDayTimeChanged(bool isDay)
    {
        gameObject.SetActive(isDay);
        meshRenderer.enabled = true;

    }

}
    