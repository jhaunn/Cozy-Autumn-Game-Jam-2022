using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction instance;

    public List<IInteractable> currentInteractions;
    [SerializeField] private TextMeshPro interactionText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(GetComponent<PlayerInteraction>());
        }
    }

    private void Start()
    {
        currentInteractions = new List<IInteractable>();
    }

    private void Update()
    {
        if (currentInteractions.Count > 0)
        {
            interactionText.text = "Press E";
        }
        else
        {
            interactionText.text = "";
        }
    }

    public void AddCurrentInteraction(IInteractable interaction) {
        if (!currentInteractions.Contains(interaction))
        {
            currentInteractions.Add(interaction);
        }
    }

    public void RemoveCurrentInteraction(IInteractable interaction)
    {
        if (currentInteractions.Contains(interaction))
        {
            currentInteractions.Remove(interaction);
        }
    }
}
