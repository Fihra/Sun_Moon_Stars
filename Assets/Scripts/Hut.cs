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
        lootAmount = ResetLootAmount();
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

    public void EmptyOutLoot()
    {
        lootAmount = 0;
    }

    public int ResetLootAmount()
    {
        return Random.Range(minLootAmount, maxLootAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
