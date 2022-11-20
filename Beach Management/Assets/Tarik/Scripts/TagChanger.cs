using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagChanger : MonoBehaviour
{
    public string objTag;
    public TextMeshProUGUI costText;
    
    void Start()
    {
        
        costText.text = gameObject.transform.parent.parent.GetComponent<CharManager>().randMoney.ToString();
        Debug.Log(gameObject.transform.parent.parent);
        gameObject.transform.parent.parent.tag = objTag;
        gameObject.transform.parent.parent.GetComponent<CharManager>().childCanvas = transform.GetChild(0).gameObject;
    }

    
}
