using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTool : MonoBehaviour
{
    public static PlayerTool instance;

    [SerializeField] private Image toolImg;
    [SerializeField] private Sprite[] toolsSprite;
    private int currentTool = 0; //0-Hands | 1-Watering Pot | 2-Hoe

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(GetComponent<PlayerTool>());
        }
    }

    private void Start()
    {
        toolImg.sprite = toolsSprite[0];
    }

    public void ChangeTool(int index) {
        toolImg.sprite = toolsSprite[index];
        currentTool = index;
    }

    public int GetCurrentTool() {
        return currentTool;
    }
}
