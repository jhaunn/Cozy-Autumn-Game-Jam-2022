using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTable : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 interactPosition;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private int toolIndex;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + interactPosition, 0.1f);
    }

    public bool CheckInteractable()
    {
        return Physics2D.OverlapCircle(transform.position + interactPosition, 0.25f, playerLayerMask) &&
            PlayerTool.instance.GetCurrentTool() != toolIndex;
    }

    public void Interact()
    {
        EffectsManager.instance.PlayAnimation(0);
        PlayerTool.instance.ChangeTool(toolIndex);
    }
}
