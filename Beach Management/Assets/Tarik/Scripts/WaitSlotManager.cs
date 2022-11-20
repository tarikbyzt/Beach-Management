using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitSlotManager : MonoBehaviour
{
    public static WaitSlotManager current;
    [SerializeField] List<GameObject> waitingSlots;
    [SerializeField] GameObject thirdCanvas;
    public GameObject secondButton;
    public GameObject thirdButton;


    public int openedWaitSlots;
    

    public int randInt;
    
    void Start()
    {
        PlayerPrefs.GetInt("openedWaitingSlots");
        if (PlayerPrefs.GetInt("openedWaitingSlots") == 1)
        {
            waitingSlots[1].SetActive(true);
            waitingSlots[1].transform.localScale = new Vector3(420.4264f, 420.4264f, 420.4264f);
        }
        if (PlayerPrefs.GetInt("openedWaitingSlots") == 2)
        {
            waitingSlots[1].SetActive(true);
            waitingSlots[1].transform.localScale = new Vector3(420.4264f, 420.4264f, 420.4264f);
            waitingSlots[2].SetActive(true);
            waitingSlots[2].transform.localScale = new Vector3(420.4264f, 420.4264f, 420.4264f);
        }

        if (SezlongManager.current.openedSunbed > 0 && waitingSlots[1].activeSelf==false)
        {
            secondButton.SetActive(true);
        }
        if (SezlongManager.current.openedSunbed > 1 && waitingSlots[2].activeSelf == false)
        {
           thirdButton.SetActive(true);
        }
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SecondWaitSlotUnlock()
    {
        if (LevelController.current.currentMoney>=250)
        {
            LevelController.current.currentMoney -= 250;
            waitingSlots[1].SetActive(true);
            waitingSlots[1].GetComponent<WaitSlot>().isUnlocked = true;
            thirdButton.SetActive(true);
            SezlongManager.current.ScaleAnimation(waitingSlots[1], 420.4264f);
            secondButton.SetActive(false);
            WaitSlotSave();
        }
        else
        {
            //WaitSlotButtonEffect(secondButton);
        }
    }
    public void ThirdWaitSlotUnlock()
    {
        if (LevelController.current.currentMoney>=500)
        {
            LevelController.current.currentMoney -=500;
            waitingSlots[2].SetActive(true);
            waitingSlots[2].GetComponent<WaitSlot>().isUnlocked = true;
            SezlongManager.current.ScaleAnimation(waitingSlots[2], 420.4264f);
            thirdButton.SetActive(false);
            WaitSlotSave();
        }
        else
        {
            //WaitSlotButtonEffect(thirdButton);
        }
    }

    private void WaitSlotSave()
    {
        if (PlayerPrefs.GetInt("openedWaitingSlots")<=2)
        {
            openedWaitSlots += 1;
            PlayerPrefs.SetInt("openedWaitingSlots", openedWaitSlots);
        }
    }
    public void WaitSlotButtonEffect(GameObject obj)
    {

        Image buttonImage = obj.gameObject.GetComponent<Image>();
        buttonImage.DOColor(SezlongManager.current.buttonColor, 0.1f).OnComplete(() =>
        {
            buttonImage.DOColor(Color.white, 0.1f);
        });
    }
}
