using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SunbedManager : MonoBehaviour
{
    public ObjectTypes myObjectType;
    public bool isEmpty=true;
    EnumManager myEnumManager;
    public Material sunbedMat;
    // Start is called before the first frame update
    void Start()
    {
        myEnumManager = GetComponent<EnumManager>();
        sunbedMat =transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Renderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEmpty)
        {
            myObjectType = ObjectTypes.None;
            myEnumManager.myObjectType=ObjectTypes.None;
            Debug.Log("Þezlong Dolu");

        }
        if (isEmpty)
        {
            myObjectType = ObjectTypes.Bed;
            myEnumManager.myObjectType = ObjectTypes.Bed;
        }
    }
    
    public void SunbedGreenAnimation()
    {
        sunbedMat.DOColor(Color.green, 0.001f).OnComplete(() =>
        {
            sunbedMat.DOFade(1, 0.1f).OnComplete(() =>
            {
                sunbedMat.DOFade(0, 0.1f);
                
            });
        });
    }
    public void SunbedRedAnimation()
    {
        sunbedMat.DOColor(Color.red, 0.001f).OnComplete(() =>
        {
            
            sunbedMat.DOFade(1, 0.1f).OnComplete(() =>
            {
                sunbedMat.DOFade(0, 0.1f);
                //sunbedMat.color = Color.green;
            });
        });
        

    }
}
