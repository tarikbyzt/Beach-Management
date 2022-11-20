using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SezlongManager : MonoBehaviour
{
    public static SezlongManager current;
    public List<GameObject> unlockButtons;
    public List<GameObject> sunbedList;
    public int openedSunbed;
    public Color buttonColor;

    private void Start()
    {
        current = this;
        openedSunbed= PlayerPrefs.GetInt("openedSunbed");
        
        
        if (sunbedList[0].activeSelf==false)
        {
            unlockButtons[0].SetActive(true);
        }
        for (int i = 1; i <= PlayerPrefs.GetInt("openedSunbed"); i++)
        {
            sunbedList[i-1].SetActive(true);
            sunbedList[i - 1].transform.localScale=new Vector3(1,1,1);
            unlockButtons[i - 1].SetActive(false);
            
            if (i<6)
            {
                unlockButtons[i].SetActive(true);

            }
        }
    }
    #region SunbedUnlockers
    public void SunbedUnlocker0()
    {
        if (LevelController.current.currentMoney>=150)
        {
            Spawner.current.percentages[3] = 20;
            PlayerPrefs.SetFloat("yellowPercentage", Spawner.current.percentages[3]);
            LevelController.current.currentMoney-=150;
            sunbedList[0].SetActive(true);
            ScaleAnimation(sunbedList[0], 1);
            WaitSlotManager.current.secondButton.SetActive(true);
            unlockButtons[1].SetActive(true);
            unlockButtons[0].SetActive(false);
            SunbedSave();
        }
        else
        {
            //SunbedButtonEffect(unlockButtons[0]);
        }
    }
    public void SunbedUnlocker1()
    {
        if (LevelController.current.currentMoney >= 250)
        {
            LevelController.current.currentMoney -= 250;
            sunbedList[1].SetActive(true);
            ScaleAnimation(sunbedList[1], 1);
            unlockButtons[2].SetActive(true);
            unlockButtons[1].SetActive(false);
            SunbedSave();
        }
        else
        {
            //SunbedButtonEffect(unlockButtons[1]);
        }
    }
    public void SunbedUnlocker2()
    {
        if (LevelController.current.currentMoney >= 500)
        {
            LevelController.current.currentMoney -= 500;
            sunbedList[2].SetActive(true);
            ScaleAnimation(sunbedList[2], 1);
            unlockButtons[3].SetActive(true);
            unlockButtons[2].SetActive(false);
            
            SunbedSave();
        }
        else
        {
            //SunbedButtonEffect(unlockButtons[2]);
        }
    }
    public void SunbedUnlocker3()
    {
        if (LevelController.current.currentMoney >= 750)
        {
            LevelController.current.currentMoney -= 750;
            sunbedList[3].SetActive(true);
            ScaleAnimation(sunbedList[3], 1);
            unlockButtons[4].SetActive(true);
            unlockButtons[3].SetActive(false);
            SunbedSave();
        }
        else
        {
            //SunbedButtonEffect(unlockButtons[3]);
        }
    }
    public void SunbedUnlocker4()
    {
        if (LevelController.current.currentMoney >= 1000)
        {
            Spawner.current.percentages[5] = 12;
            Spawner.current.percentages[0] = 18;
            PlayerPrefs.SetFloat("semsiyePercentage", Spawner.current.percentages[5]);
            PlayerPrefs.SetFloat("redPercentage", Spawner.current.percentages[0]);
            LevelController.current.currentMoney -= 1000;
            sunbedList[4].SetActive(true);
            ScaleAnimation(sunbedList[4],1);
            unlockButtons[5].SetActive(true);
            unlockButtons[4].SetActive(false);
            SunbedSave();
        }
        else
        {
            //SunbedButtonEffect(unlockButtons[4]);
        }
    }
    public void SunbedUnlocker5()
    {
        if (LevelController.current.currentMoney >= 1000)
        {
            LevelController.current.currentMoney -= 1000;
            sunbedList[5].SetActive(true);
            ScaleAnimation(sunbedList[5],1);
            unlockButtons[5].SetActive(false);
            SunbedSave();
        }
        else
        {
            //SunbedButtonEffect(unlockButtons[5]);
        }
    }
    #endregion
    #region Kaldiricilar
    public void Right1()
    {

        sunbedList[6].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[6].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[6]));
        
    }
    public void Right2()
    {
        sunbedList[7].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[7].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[7]));
    }
    public void Right3()
    {
        sunbedList[0].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[0].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[0]));
    }
    public void Right4()
    {
        sunbedList[5].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[5].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[5]));
    }
    public void Left1()
    {
        sunbedList[1].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[1].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[1]));
    }
    public void Left2()
    {
        sunbedList[2].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[2].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[2]));
    }
    public void Left3()
    {
        sunbedList[3].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[3].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[3]));
    }
    public void Left4()
    {
        sunbedList[4].transform.GetChild(4).GetComponent<CharManager>().StartCoroutine(sunbedList[4].transform.GetChild(4).GetComponent<CharManager>().Kaldirici(sunbedList[4]));
    }

    #endregion
    public void ScaleAnimation(GameObject sunbed,float i)
    {
        sunbed.transform.DOScale(new Vector3(i, i, i), 0.7f).SetEase(Ease.InOutElastic);
    }
    private void SunbedSave()
    {
        if (openedSunbed <= 6)
        {
            openedSunbed += 1;
            PlayerPrefs.SetInt("openedSunbed", openedSunbed);
        }
    }
    public void SunbedButtonEffect(GameObject obj)
    {

        Image buttonImage=obj.transform.GetChild(0).gameObject.GetComponent<Image>();
        buttonImage.DOColor(buttonColor, 0.1f).OnComplete(() =>
        {
            buttonImage.DOColor(Color.white, 0.1f);
        });
    }
}
