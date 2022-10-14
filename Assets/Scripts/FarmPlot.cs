using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRend;

    [SerializeField] private LayerMask playerLayerMask;

    [SerializeField] private Sprite[] farmSprites;
    [SerializeField] private GameObject[] indicators;

    private bool isPlanted = false;
    private bool needsWater = false;
    private bool canHarvest = false;

    [SerializeField] private int[] harvestScoreRange;
    [SerializeField] private int[] seedScoreRange;
    [SerializeField] private float[] growthRateRange;

    private float growthRate;
    private float totalGrowth = 0f;

    private void Awake()
    {
        if (GetComponent<SpriteRenderer>())
        {
            spriteRend = GetComponent<SpriteRenderer>();
        }
        else
        {
            spriteRend = gameObject.AddComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        spriteRend.sortingLayerName = "Ground";
        spriteRend.sortingOrder = 3;
    }

    private void Update()
    {
        if (CheckInteractable())
        {
            PlayerInteraction.instance.AddCurrentInteraction(this);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else
        {
            PlayerInteraction.instance.RemoveCurrentInteraction(this);
        }

        if (isPlanted)
        {
            GrowPlant();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    public bool CheckInteractable()
    {
        if (!isPlanted)
        {
            indicators[0].SetActive(true);
            indicators[1].SetActive(false);
            indicators[2].SetActive(false);

            if (Physics2D.CircleCast(transform.position, 0.25f, Vector2.zero, playerLayerMask) && 
                PlayerTool.instance.GetCurrentTool() == 2 && ScoreManager.instance.GetSeedScore() > 0)
            {
                return true;
            }

            return false;
        }
        else if (needsWater)
        {
            indicators[0].SetActive(false);
            indicators[1].SetActive(true);
            indicators[2].SetActive(false);

            if (Physics2D.OverlapCircle(transform.position, 0.25f, playerLayerMask) && PlayerTool.instance.GetCurrentTool() == 1)
            {
                return true;
            }

            return false;
        }
        else if (canHarvest)
        {
            indicators[0].SetActive(false);
            indicators[1].SetActive(false);
            indicators[2].SetActive(true);

            if (Physics2D.CircleCast(transform.position, 0.25f, Vector2.zero, playerLayerMask) && PlayerTool.instance.GetCurrentTool() == 0)
            {
                return true;
            }

            return false;
        }
        else 
        {
            return false;
        }
    }

    public void Interact()
    {
        if (!isPlanted && ScoreManager.instance.GetSeedScore() > 0)
        {
            ResetIndicators();

            growthRate = Random.Range(growthRateRange[0], growthRateRange[1]);
            spriteRend.sprite = farmSprites[0];
            isPlanted = true;

            ScoreManager.instance.ChangeSeedScore(-1);
            EffectsManager.instance.PlayAnimation(0);
            EffectsManager.instance.PlayAnimation(1);
            Instantiate(EffectsManager.instance.PlantEffect, transform.position, transform.rotation);
        }
        else if (needsWater)
        {
            ResetIndicators();

            needsWater = false;
            totalGrowth += 2f;

            EffectsManager.instance.PlayAnimation(0);
            Instantiate(EffectsManager.instance.WaterEffect, transform.position, transform.rotation);
        }
        else if (canHarvest)
        {
            ResetIndicators();

            isPlanted = false;
            canHarvest = false;
            spriteRend.sprite = null;
            totalGrowth = 0f;

            ScoreManager.instance.ChangeHarvestScore(Random.Range(harvestScoreRange[0], harvestScoreRange[1]));
            ScoreManager.instance.ChangeSeedScore(Random.Range(seedScoreRange[0],seedScoreRange[1]));
            EffectsManager.instance.PlayAnimation(1);
            EffectsManager.instance.PlayAnimation(0);
            EffectsManager.instance.PlayAnimation(2);
            Instantiate(EffectsManager.instance.HarvestEffect, transform.position, transform.rotation);
        }
    }

    public void ResetIndicators()
    {
        indicators[0].SetActive(false);
        indicators[1].SetActive(false);
        indicators[2].SetActive(false);
    }

    private void GrowPlant()
    {
        if (totalGrowth < 100f && !needsWater)
        {
            totalGrowth += Time.deltaTime * growthRate;
        }

        if (totalGrowth > 33f && totalGrowth < 34f)
        {
            spriteRend.sprite = farmSprites[1];
            needsWater = true;
        }

        if (totalGrowth > 66f && totalGrowth < 67f)
        {
            spriteRend.sprite = farmSprites[2];
            needsWater = true;
        }

        if (totalGrowth >= 100f)
        {
            spriteRend.sprite = farmSprites[3];
            canHarvest = true;
        }
    }
}
