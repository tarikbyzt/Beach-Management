using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner current;
    #region Percentages
    [Header("Percentages")]
    [SerializeField] float red;
    [SerializeField] float green;
    [SerializeField] float blue;
    [SerializeField] float yellow;
    [SerializeField] float table;
    [SerializeField] float sunShade;
    #endregion
    public GameObject[] characters ;
    public List<GameObject> randomSpec;
    public Transform spawnPoint;
    public GameObject characterParent;
    public List<float> percentages=new List<float>();
    public List<GameObject> waiters ;
    int rand;
    

    private void Start()
    {
        current = this;
        if (PlayerPrefs.HasKey("yellowPercentage"))
        {
            percentages[3] = PlayerPrefs.GetFloat("yellowPercentage");
        }
        if (PlayerPrefs.HasKey("semsiyePercentage"))
        {
            percentages[5] = PlayerPrefs.GetFloat("semsiyePercentage");
        }
        if (PlayerPrefs.HasKey("redPercentage"))
        {
           percentages[0] = PlayerPrefs.GetFloat("redPercentage");
        }

        //InvokeRepeating("Spawn", 0f, 5f);


    }
    void Update()
    {
        rand = Random.Range(0, 4);
        
        
    }
    public int GetRandomSpawn()
    {
        float random = Random.Range(0f, 1f);
        float numForAdding = 0;
        float total = 0;
        for (int i = 0; i < percentages.Count; i++)
        {
            total += percentages[i];
        }
        for (int i = 0; i < percentages.Count; i++)
        {
            if (percentages[i]/total+numForAdding>=random)
            {
                return i;
            }
            else
            {
                numForAdding += percentages[i] / total;
            }
        }
        return 0;
    }
   
}
