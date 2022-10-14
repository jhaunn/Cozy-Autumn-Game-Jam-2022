using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmArea : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isBought = false;

    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private int price;
    [SerializeField] private LayerMask playerLayerMask;
    private bool isInteractable;

    private void Start()
    {
        CheckFarmStatus();
    }

    private void Update()
    {
        priceText.text = price.ToString();

        if (isInteractable)
        {
            priceText.gameObject.SetActive(true);
        }
        else
        {
            priceText.gameObject.SetActive(false);
        }

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
    }

    public bool CheckInteractable()
    {
        return Physics2D.OverlapCircle(transform.position, 1f, playerLayerMask) && 
            ScoreManager.instance.GetHarvestScore() >= price && isInteractable;
    }

    public void Interact()
    {
        EffectsManager.instance.PlayAnimation(2);
        ScoreManager.instance.ChangeHarvestScore(-price);
        isBought = true;
        CheckFarmStatus();
    }

    private void CheckFarmStatus()
    {
        if (isBought)
        {
            isInteractable = false;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.GetComponent<FarmPlot>())
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            isInteractable = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.GetComponent<FarmPlot>())
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
