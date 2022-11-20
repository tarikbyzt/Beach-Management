using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitSlot : MonoBehaviour
{
    public static WaitSlot current;
    [SerializeField] bool isEmpty = true;
    

    Coroutine spawner;
    public Transform spawnPoint;
    public bool isUnlocked;
    public bool firstTutorial;


    // Start is called before the first frame update
    void Start()
    {
        
        current = this;
        isUnlocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount>2)
        {
            Destroy(transform.GetChild(2).gameObject);
            Spawner.current.waiters.Remove(transform.GetChild(2).gameObject);
            isEmpty =true;
        }
        if (isEmpty && isUnlocked)
        {
            if (spawner == null)
            {
                spawner = StartCoroutine(SpawnCharacter());
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        isEmpty = true;
    }

    IEnumerator SpawnCharacter()
    {
        int spawnWait = Random.Range(2, 6);
        if (GameManager.current.clickAnimCount < 1)
        {
            yield return new WaitForSeconds(0.01f);
        }
        if (GameManager.current.clickAnimCount >= 1)
        {
            yield return new WaitForSeconds(spawnWait);
        }
        GameObject newCharacter = Instantiate(Spawner.current.characters[Random.Range(0, Spawner.current.characters.Length)],spawnPoint);
        Spawner.current.waiters.Add(newCharacter);
        
        GameObject typeObj = newCharacter.transform.GetChild(2).transform.GetChild(Spawner.current.GetRandomSpawn()).gameObject;
        typeObj.SetActive(true);
        Debug.Log("ilk oluþturulan tür=" + typeObj);
        if (GameManager.current.clickAnimCount < 1)
        {
            Destroy(typeObj);
            newCharacter.transform.GetChild(2).transform.GetChild(6).gameObject.SetActive(true);
            
        }
        if (Spawner.current.waiters.Count>0)
        {
            foreach (GameObject item in Spawner.current.waiters)
            {
                if (item.tag == typeObj.GetComponent<TagChanger>().objTag)
                {
                    Destroy(typeObj);
                    newCharacter.transform.GetChild(2).transform.GetChild(Spawner.current.GetRandomSpawn()).gameObject.SetActive(true);
                }
            }
        }
        


        newCharacter.transform.parent =Spawner.current.characterParent.transform;
        newCharacter.GetComponent<NavMeshAgent>().destination = transform.position;
        newCharacter.GetComponent<Animator>().SetTrigger("Start");
        if (newCharacter.transform.GetChild(0).tag == "Man")
        {
            Debug.Log("Erkek Yürüyor");
            newCharacter.GetComponent<Animator>().SetFloat("Blend", 4);
            Debug.Log(newCharacter.GetComponent<Animator>().GetFloat("Blend"));
            
        }
        else
        {
            newCharacter.GetComponent<Animator>().SetFloat("Blend", 1);
        }
        
        spawner = null;
        isEmpty = false;
        
    }
}
