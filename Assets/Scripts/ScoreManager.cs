using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] TextMeshProUGUI homeText;
    [SerializeField] TextMeshProUGUI heldText;
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI nightText;

    public static int homeScore = 0;
    public static int heldLoot = 0;

    public enum ChangeText
    {
        changeHome,
        changeHeld,
        changeDay,
        changeNight
    }

    public int GetHomeScore()
    {
        return homeScore;
    }
    public int GetHeldLoot()
    {
        return heldLoot;
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        homeText.text = "Home: 0";
        heldText.text = "Held: 0";
        dayText.text = "Day: 0";
        nightText.text = "Night: 1";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeTextUI(ChangeText changeText, string newText = "")
    {
        switch (changeText)
        {
            case ChangeText.changeHome:
                homeText.text = "Home: " + homeScore.ToString();
                break;
            case ChangeText.changeHeld:
                heldText.text = "Held: " + heldLoot.ToString();
                break;
            case ChangeText.changeDay:
                dayText.text = newText;
                break;
            case ChangeText.changeNight:
                nightText.text = newText;
                break;
            default:
                break;
        }
    }

    public void AddToHomeLoot(int incomingScore)
    {
        homeScore += incomingScore;
        ChangeTextUI(ChangeText.changeHome);
        ChangeTextUI(ChangeText.changeHeld);
        ResetHeldLoot();
    }

    public void ResetHeldLoot()
    {
        heldLoot = 0;
        ChangeTextUI(ChangeText.changeHeld);
    }

    public void AddToHeldLoot(int incomingScore)
    {
        heldLoot += incomingScore;
        ChangeTextUI(ChangeText.changeHeld);
    }

    public void RemoveFromHeldLoot(float incomingNum)
    {

        float tempLoot = heldLoot - incomingNum;
        Debug.Log(tempLoot);
        heldLoot = Mathf.FloorToInt(tempLoot);

        if (heldLoot < 0)
        {
            ResetHeldLoot();
        }
        ChangeTextUI(ChangeText.changeHeld);
    }

}
