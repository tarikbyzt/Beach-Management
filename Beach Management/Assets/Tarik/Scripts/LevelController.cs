using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController current;
    public TextMeshProUGUI moneyText;
    public int currentMoney;
    public GameObject moneyImg;
    public Animator moneyAnimator;

    void Start()
    {
        currentMoney = PlayerPrefs.GetInt("currentMoney");
        current = this;

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = currentMoney.ToString();
        ScoreSave();
    }
    public void ScoreSave()
    {
        PlayerPrefs.SetInt("currentMoney",currentMoney);
    }
    public void AddMoney(int randMoney)
    {
        currentMoney += randMoney;
        moneyAnimator.SetTrigger("Money");
    }
}
