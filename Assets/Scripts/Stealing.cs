using UnityEngine;

public class Stealing : MonoBehaviour
{
    [SerializeField] private bool isInLootingZone = false;
    [SerializeField] private bool lootDone = false;

    [SerializeField] private float timer = 0;
    [SerializeField] private float lootTime = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= lootTime)
        {
            //Debug.Log($"Loot is done {timer}");
            lootDone = true;
            isInLootingZone = false;
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"GameObject {other.gameObject.name} entered the range!");
        // Perform actions when an object enters the range
        if (other.gameObject.CompareTag("LootRange"))
        {
            isInLootingZone = true;
            timer = 0;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (isInLootingZone && !lootDone)
        {
            Debug.Log(timer);
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"GameObject {other.gameObject.name} exited the range!");
        // Perform actions when an object leaves the range
        if (other.gameObject.CompareTag("LootRange"))
        {
            isInLootingZone = false;
            timer = 0;
        }
    }
}
