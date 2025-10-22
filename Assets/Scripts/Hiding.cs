using UnityEngine;

public class Hiding : MonoBehaviour
{
    [SerializeField] float safeCounter = 5;
    float timer = 0;

    private bool safety = false;
    LightingManager currentDay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("HidingSpot") || other.gameObject.CompareTag("LootRange"))
        {
            Debug.Log("Safe");
            timer = 0;
            safety = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HidingSpot") || other.gameObject.CompareTag("LootRange"))
        {
            Debug.Log("Not Safe");
            safety = false;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentDay = LightingManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDay.GetCurrentDay() == CurrentDay.Day)
        {
            safety = false;
        } else if(currentDay.GetCurrentDay() == CurrentDay.Night)
        {
            safety = true;
            timer = 0;
        }

        if (!safety)
        {
            
            if (timer >= safeCounter)
            {
                ScoreManager.Instance.RemoveFromHeldLoot(.00001f);
                return;
                //Debug.Log("Im losing loot!");
            }

            timer += Time.deltaTime;
        }

  
    }
}
