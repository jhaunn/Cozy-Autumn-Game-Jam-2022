using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;

    [SerializeField] private GameObject plantEffect;
    [SerializeField] private GameObject waterEffect;
    [SerializeField] private GameObject harvestEffect;

    [SerializeField] private Animator toolPanelAnim;
    [SerializeField] private Animator seedPanelanim;
    [SerializeField] private Animator harvestPanelAnim;
    
    public GameObject PlantEffect {
        get { return plantEffect; }
    }

    public GameObject WaterEffect {
        get { return waterEffect; }
    }

    public GameObject HarvestEffect {
        get { return harvestEffect; }
    }

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

    public void PlayAnimation(int i)
    {
        if (i==0)
        {
            toolPanelAnim.Play("Shake", 0, 0.0f);
        }
        else if (i==1)
        {
            seedPanelanim.Play("Shake", 0, 0.0f);
        }
        else if (i==2)
        {
            harvestPanelAnim.Play("Shake", 0, 0.0f);
        }
    }
}
