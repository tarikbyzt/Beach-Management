using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorChange : MonoBehaviour
{
    public int buttonCost;
    public TextMeshProUGUI textTmp;
    public Color currentColor;
    public Color newColor;

    void Start()
    {
        textTmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelController.current.currentMoney>=buttonCost)
        {
            textTmp.color = newColor;
        }
        else
        {
            textTmp.color = currentColor;
        }
    }
}
