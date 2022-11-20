using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SunbedDiscCanvas : MonoBehaviour
{
   public RectTransform discRect;
    public GameObject character;
    public GameObject sezlong;
    public GameObject discBG;
    public GameObject whiteStroke;
    
    
    void Start()
    {
        discBG = transform.GetChild(0).gameObject;
        character = transform.parent.parent.gameObject;
        sezlong = character.transform.parent.gameObject;
        whiteStroke = discRect.gameObject.transform.GetChild(0).gameObject;
        discRect.gameObject.GetComponent<Image>().DOFillAmount(1, 20);
        
        discRect.gameObject.GetComponent<Image>().DOColor(Color.red, 17f).SetDelay(10);
    }

    // Update is called once per frame
    void Update()
    {
        whiteStroke.gameObject.GetComponent<Image>().fillAmount= discRect.gameObject.GetComponent<Image>().fillAmount;
        
    }
    
}
