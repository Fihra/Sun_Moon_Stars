using UnityEngine;

public class Hut : MonoBehaviour
{
    [SerializeField] private int lootAmount = 0;
    [SerializeField] private bool isThereLoot = true;
    [SerializeField] int minLootAmount;
    [SerializeField] int maxLootAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lootAmount = Random.Range(minLootAmount, maxLootAmount);
    }

    public int GetLootAmount()
    {
        return lootAmount;
    }

    public bool GetIsThereLoot()
    {
        return isThereLoot;
    }

    public void SetIsThereLoot(bool lootCheck)
    {
        isThereLoot = lootCheck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
