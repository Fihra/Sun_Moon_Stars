using UnityEngine;

public class Stealing : MonoBehaviour
{
    [SerializeField] private bool isInLootingZone = false;
    //private bool isThereLoot;
    private Hut hut;

    [SerializeField] private float timer = 0;
    [SerializeField] private float lootTime = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hut = GetComponentInParent<Hut>();
        //isThereLoot = hut.GetIsThereLoot();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= lootTime)
        {
            //Debug.Log($"Loot is done {timer}");
            hut.SetIsThereLoot(false);
            //isThereLoot = true;
            
            isInLootingZone = false;
            timer = 0;
            int loot = hut.GetLootAmount();
            Debug.Log("my loot: " + loot);
            if (loot < 0) return;
            else
            {
                ScoreManager.Instance.AddToHeldLoot(loot);
               
                hut.EmptyOutLoot();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"GameObject {other.gameObject.name} entered the range!");
        // Perform actions when an object enters the range
        if (other.gameObject.CompareTag("Player"))
        {
            isInLootingZone = true;
            timer = 0;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(hut);
        if (isInLootingZone && hut.GetIsThereLoot())
        {
            Debug.Log("hit me");
            Debug.Log(timer);
            timer += Time.deltaTime;
        } else
        {
            isInLootingZone = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"GameObject {other.gameObject.name} exited the range!");
        // Perform actions when an object leaves the range
        if (other.gameObject.CompareTag("Player"))
        {
            isInLootingZone = false;
            timer = 0;
        }
    }
}
