using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour, IInteractable
{
    private bool isBought = false;
    [SerializeField] private int price;
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject gfx;

    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float interactionRadius;

    private void Start()
    {
        priceText.text = price.ToString();
        priceText.gameObject.SetActive(true);
        gfx.SetActive(false);
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
    }

    public bool CheckInteractable()
    {
        return Physics2D.OverlapCircle(transform.position, interactionRadius, playerLayerMask) &&
            ScoreManager.instance.GetHarvestScore() >= price && !isBought;
    }

    public void Interact()
    {
        isBought = true;
        EffectsManager.instance.PlayAnimation(2);
        ScoreManager.instance.ChangeHarvestScore(-price);
        priceText.gameObject.SetActive(false);
        gfx.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
