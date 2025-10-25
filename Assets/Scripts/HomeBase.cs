using UnityEngine;

public class HomeBase : MonoBehaviour
{
    [SerializeField] private bool inDropOffZone = false;

    [SerializeField] private float timer = 0;
    [SerializeField] private float dropOffTime = 5.0f;
    private void OnTriggerEnter(Collider other)
    {
        
        // Perform actions when an object enters the range
        if (other.gameObject.CompareTag("Player"))
        {
            inDropOffZone = true;
            Debug.Log($"GameObject {other.gameObject.name} entered the range!");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (inDropOffZone && ScoreManager.Instance.GetHeldLoot() > 0)
        {
            Debug.Log("Adding Loot");
            timer += Time.deltaTime;
            if(timer >= dropOffTime)
            {
                Debug.Log("Loot done");
                timer = 0;
                inDropOffZone = false;
                ScoreManager.Instance.AddToHomeLoot(ScoreManager.Instance.GetHeldLoot());
                SFXManager.Instance.PlaySFX("dropOffLoot");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inDropOffZone = false;
        }
    }
}
