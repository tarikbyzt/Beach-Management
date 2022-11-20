using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public ObjectTypes myObjectType;
    public static GameManager current;
    public List<GameObject> selectedObjects;

    public GameObject selectedChar;
    public GameObject selectedBed;
    public CharManager charManager;
    public int clickAnimCount;
    public int clickAnimSayi;
    EnumManager myEnumManager;
    public Color skinColor;
    public Color sunTan;
    public Color greenTone;
    public GameObject tutorialAnimObj;
    public GameObject tutorialAnimMavi;
    public GameObject tutorialAnimYesil;
    

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        clickAnimCount = PlayerPrefs.GetInt("clickAnimCount");
        clickAnimSayi = PlayerPrefs.GetInt("clickAnimSayi");

        //navMesh = GetComponent<NavMeshAgent>();
        //navMesh.destination = new Vector3(12, 2, -25);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedObject();
        }
    }
    private void SelectedObject()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray,out hitInfo))
        {
            myEnumManager = hitInfo.transform.gameObject.GetComponent<EnumManager>();

            if (myEnumManager != null)
            {
                if (myEnumManager.myObjectType==ObjectTypes.Character)
                {
                    
                    selectedChar = hitInfo.transform.gameObject;
                    
                    
                }
                else if (myEnumManager.myObjectType==ObjectTypes.Bed)
                {
                    selectedBed = hitInfo.transform.gameObject;
                    charManager = selectedChar.GetComponent<CharManager>();
                    if (selectedBed.GetComponent<SunbedManager>().isEmpty==false)
                    {
                        selectedBed.GetComponent<SunbedManager>().SunbedRedAnimation();
                    }
                    if (selectedBed.tag == selectedChar.tag || selectedBed.transform.GetChild(0).tag==selectedChar.tag)
                    {
                        SezlongColorAnim(Color.green, selectedBed);
                        //selectedBed.GetComponent<SunbedManager>().SunbedGreenAnimation();
                        charManager.CharMovement(selectedBed);
                        selectedBed.GetComponent<SunbedManager>().isEmpty = false;
                        
                        selectedChar = null;
                        selectedBed = null;
                    }
                    if (selectedChar.CompareTag("None"))
                    {
                        selectedBed.transform.GetChild(1).tag = selectedChar.tag; //seçilen yataðýn.1.childýný charýn tagine eþitliþyoruz.
                        SezlongColorAnim(Color.green, selectedBed);
                        
                        charManager.CharMovement(selectedBed);
                        selectedBed.GetComponent<SunbedManager>().isEmpty = false;
                        selectedChar = null;
                        selectedBed = null;
                    }
                    if (selectedBed.tag != selectedChar.tag || selectedBed.transform.GetChild(0).tag != selectedChar.tag|| !selectedChar.CompareTag("None"))
                    {
                        selectedChar.GetComponent<CharManager>().charReding = true;
                        SezlongColorAnim(Color.red, selectedBed);
                        selectedChar.GetComponent<CharManager>().mat.DOColor(Color.red, "_OutlineColor", 0.001f).OnComplete(() =>
                        {
                            selectedChar.GetComponent<CharManager>().mat.DOFloat(7.5F, "_OutlineWidth", 0.001f).OnComplete(() =>
                            {
                                selectedChar.GetComponent<CharManager>().mat.DOFloat(0, "_OutlineWidth", 0.001f).SetDelay(0.3f).OnComplete(() =>
                                {
                                    selectedChar.GetComponent<CharManager>().charReding = false;
                                });
                                
                            });
                        });

                        selectedBed.GetComponent<SunbedManager>().SunbedRedAnimation();
                        
                    }
                }
            }
        }
    }
    
    public void SezlongColorAnim(Color clr,GameObject sezlong)
    {
        Material mat = sezlong.transform.GetChild(0).GetComponent<Renderer>().materials[3];
        mat.DOColor(clr, "_OutlineColor", 0.001f).OnComplete(() =>
        {
            mat.DOFloat(5, "_OutlineWidth", 0.001f).OnComplete(() =>
            {
                mat.DOFloat(0, "_OutlineWidth", 0.001f).SetDelay(0.3f);

            });
        });
    }

}
