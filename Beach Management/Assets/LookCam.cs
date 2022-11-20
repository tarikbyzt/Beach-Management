using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{
    [SerializeField] Transform lookCamera;
    private Transform localTrans;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        localTrans = GetComponent<Transform>();
        lookCamera= cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (lookCamera)
        {
            localTrans.LookAt(2 * localTrans.position - lookCamera.position);
        }
    }
}
