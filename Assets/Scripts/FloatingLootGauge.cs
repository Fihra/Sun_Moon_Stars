using UnityEngine;
using UnityEngine.UI;

public class FloatingLootGauge : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateLootGauge(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.forward);
        transform.position = target.position + offset;
    }
}
