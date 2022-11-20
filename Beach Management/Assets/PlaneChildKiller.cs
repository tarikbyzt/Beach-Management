using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneChildKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            Spawner.current.waiters.Remove(transform.GetChild(0).gameObject);
            Destroy(transform.GetChild(0).gameObject);
            
            
        }
    }
}
