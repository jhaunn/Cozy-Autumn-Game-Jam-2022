using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI seedText;
    [SerializeField] private TextMeshProUGUI harvestText;

    private int seedScore = 5;
    private int harvestScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        seedText.text = seedScore.ToString();
        harvestText.text = harvestScore.ToString();
    }

    public void ChangeSeedScore(int score)
    {
        seedScore += score;
    }

    public int GetSeedScore()
    {
        return seedScore;
    }

    public void ChangeHarvestScore(int score)
    {
        harvestScore += score;
    }

    public int GetHarvestScore()
    {
        return harvestScore;
    }
}
