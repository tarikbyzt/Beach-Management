using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeciesCanvas : MonoBehaviour
{
    RectTransform discRect;
    public GameObject character;
    public GameObject discBGwait;
    public GameObject whiteStrokewait;


    void Start()
    {
        
        character = transform.parent.parent.parent.gameObject;
        discRect =transform.GetChild(3).GetComponent<RectTransform>();
        discBGwait = transform.GetChild(2).gameObject.transform.GetChild(3).gameObject;
        whiteStrokewait = discRect.gameObject.transform.GetChild(0).gameObject;
        discRect.gameObject.GetComponent<Image>().DOFillAmount(1, 12).SetDelay(5f).OnComplete(() =>
        {
            character.GetComponent<CharManager>().Comeback();
            
        });
        discRect.gameObject.GetComponent<Image>().DOColor(Color.red, 12f).SetDelay(5f);
    }

    // Update is called once per frame
    void Update()
    {
        whiteStrokewait.gameObject.GetComponent<Image>().fillAmount = discRect.gameObject.GetComponent<Image>().fillAmount;
        
    }
}
