using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{
    public static CharManager current;
    public ObjectTypes myObjectType;
    EnumManager myEnumManager;
    NavMeshAgent navMesh;
    public Animator animator;
    public bool isWalking = false;
    public bool forceUp = true;
    public bool isDestinationReach = false;
    private int randInt;
    private Collider myCollider;
    public int randMoney;
    public float blendValue;
    public GameObject childCanvas;
    public GameObject sunbedDiscCanv;
    public bool isLaying = false;
    public Material mat;
    public bool charReding = false;
    public Tween scaleAnim;
    int loopNum;

    void Start()
    {
        Debug.Log("Çarýn tagi= " + tag);


        randMoney = Random.Range(40, 80);

        current = this;
        animator = GetComponent<Animator>();
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        myEnumManager = GetComponent<EnumManager>();
        myCollider = GetComponent<Collider>();
        if (transform.GetChild(0).gameObject.name == "NurbsPath.013")
        {
            mat = transform.GetChild(0).gameObject.GetComponent<Renderer>().materials[1];
        }
        else
        {
            mat = transform.GetChild(0).gameObject.GetComponent<Renderer>().materials[0];
        }

        sunbedDiscCanv = transform.GetChild(3).transform.GetChild(0).gameObject;
        animator.SetTrigger("Start");
        loopNum = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            Destroy(childCanvas);
            mat.SetFloat("_OutlineWidth", 0f);
        }
        if (isLaying)
        {
            if (sunbedDiscCanv.GetComponent<SunbedDiscCanvas>().discRect.gameObject.GetComponent<Image>().fillAmount >= 0.75f)
            {
                GameObject sezlong = transform.parent.gameObject;
                SunbedGreenOutline(true);
                if (GameManager.current.clickAnimSayi < 2)
                {
                    if (sezlong.name == "Right1")
                    {
                        GameManager.current.tutorialAnimMavi.SetActive(true);
                        GameManager.current.clickAnimSayi=1;
                        PlayerPrefs.SetInt("clickAnimSayi", GameManager.current.clickAnimSayi);
                    }
                    if (sezlong.name == "Right2")
                    {
                        GameManager.current.tutorialAnimYesil.SetActive(true);
                        GameManager.current.clickAnimSayi = 2;
                        PlayerPrefs.SetInt("clickAnimSayi", GameManager.current.clickAnimSayi);
                    }
                }

                mat.DOColor(GameManager.current.sunTan, 5f);
                if (sunbedDiscCanv != null)
                {
                    //transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f).SetLoops(-1, LoopType.Yoyo);

                }
                sezlong.transform.GetChild(2).gameObject.SetActive(true);

            }

        }
        if (sunbedDiscCanv != null)
        {
            if (sunbedDiscCanv.GetComponent<SunbedDiscCanvas>().discRect.gameObject.GetComponent<Image>().fillAmount == 1f)
            {
                forceUp = false;
                sunbedDiscCanv.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = 0;
                sunbedDiscCanv.transform.GetChild(2).gameObject.SetActive(false);
                StartCoroutine(Kaldirici(transform.parent.gameObject));

            }
        }

        if (gameObject == GameManager.current.selectedChar && charReding == false)
        {
            gameObject.GetComponent<CharManager>().mat.DOColor(Color.green, "_OutlineColor", 0.001f).OnComplete(() =>
            {
                gameObject.GetComponent<CharManager>().mat.SetFloat("_OutlineWidth", 7.5f);
            });
        }
        if (gameObject != GameManager.current.selectedChar)
        {
            gameObject.GetComponent<CharManager>().mat.SetFloat("_OutlineWidth", 0f);
        }
        //if (isTweening)
        //{
        //    timer += Time.deltaTime;
        //    percent = timer / timeToGo;
        //    transform.position = Vector3.Lerp(start.position, end.position, percent);
        //    if (timer >= timeToGo)
        //    {
        //        isTweening = false;
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waiting"))
        {
            myObjectType = ObjectTypes.Character;
            myEnumManager.myObjectType = ObjectTypes.Character;
            if (GameManager.current.clickAnimCount < 1)
            {
                GameManager.current.tutorialAnimObj.SetActive(true);
                GameManager.current.clickAnimCount++;
                PlayerPrefs.SetInt("clickAnimCount", GameManager.current.clickAnimCount);
            }
            animator.SetFloat("Blend", 0);



        }
        if (other.CompareTag("SunShade") || other.CompareTag("Table"))
        {
            string childName = other.transform.GetChild(0).gameObject.tag;
        }
        if (other.CompareTag(gameObject.tag) || other.transform.GetChild(0).gameObject.CompareTag(gameObject.tag) || other.transform.GetChild(1).CompareTag(gameObject.tag))
        {
            StartCoroutine(SunbedTriggered(other));
        }

    }
    public void CharMovement(GameObject selectedBed)
    {
        Spawner.current.waiters.Remove(gameObject);
        isWalking = true;
        myObjectType = ObjectTypes.None;
        myEnumManager.myObjectType = ObjectTypes.None;
        if (transform.GetChild(0).tag == "Man")
        {
            animator.SetFloat("Blend", 4);
        }
        else
        {
            blendValue = animator.GetFloat("Blend");
            //DOTween.To(DegiskeninDegeriniAl, DegiskeninDegeriniDegistir, 1f, 0.5f);
            animator.SetFloat("Blend", 1);
        }
        Vector3 bedLocation = new Vector3(selectedBed.transform.GetChild(1).position.x, selectedBed.transform.GetChild(1).position.y, selectedBed.transform.GetChild(1).position.z);
        navMesh.destination = bedLocation;



    }
    public IEnumerator SunbedTriggered(Collider cld)
    {

        Debug.Log("Çarpýþýldý");
        myCollider.enabled = false;
        animator.SetTrigger("Start");
        animator.SetFloat("Blend", 2);
        //yield return new WaitForSeconds(1);
        navMesh.enabled = false;

        gameObject.transform.parent = cld.transform;
        transform.localPosition = new Vector3(0, 0, -4);
        transform.DOLocalMove(new Vector3(0, 0, 0.29f), 2f).SetEase(Ease.InQuad);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        isLaying = true;
        cld.transform.GetChild(1).tag = "Untagged";
        yield return new WaitForSeconds(2);

        mat.DOColor(GameManager.current.skinColor, 10f);
        sunbedDiscCanv.GetComponent<SunbedDiscCanvas>().enabled = true;




    }
    public IEnumerator Kaldirici(GameObject sezlong)
    {
        //scaleAnim=null;

        GameObject character = sezlong.transform.GetChild(4).gameObject;
        character.GetComponent<CharManager>().isLaying = false;
        Vector3 escapePoint = new Vector3(sezlong.transform.GetChild(3).position.x, sezlong.transform.GetChild(3).position.y, sezlong.transform.GetChild(3).position.z);
        sezlong.transform.GetChild(0).GetComponent<Renderer>().materials[3].SetFloat("_OutlineWidth", 0);
        if (character.GetComponent<CharManager>().forceUp)
        {

            LevelController.current.AddMoney(character.GetComponent<CharManager>().randMoney);
            Destroy(character.GetComponent<CharManager>().sunbedDiscCanv);
        }
        if (GameManager.current.clickAnimSayi <=2)
        {
            if (sezlong.name == "Right1")
            {
                GameManager.current.tutorialAnimMavi.SetActive(false);
            }
            if (sezlong.name == "Right2")
            {
                GameManager.current.tutorialAnimYesil.SetActive(false);
            }
        }

        character.GetComponent<Animator>().SetTrigger("Start");
        character.GetComponent<Animator>().SetFloat("Blend", 3);
        sezlong.transform.GetChild(2).gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        sezlong.GetComponent<SunbedManager>().isEmpty = true;
        yield return new WaitForSeconds(2.4f);
        if (character.transform.GetChild(0).CompareTag("Man"))
        {
            character.GetComponent<CharManager>().animator.SetFloat("Blend", 4);
        }
        if (!character.transform.GetChild(0).CompareTag("Man"))
        {
            character.GetComponent<CharManager>().animator.SetFloat("Blend", 1);
        }





        character.transform.localPosition = sezlong.transform.GetChild(1).localPosition;

        character.GetComponent<CharManager>().navMesh.enabled = true;
        character.GetComponent<CharManager>().navMesh.destination = escapePoint;



        Destroy(character, 2);
    }

    public void Comeback()
    {
        GameManager.current.selectedChar = null;
        Spawner.current.waiters.Remove(gameObject);
        Destroy(childCanvas);
        animator.SetTrigger("Start");
        animator.SetFloat("Blend", 1);
        myObjectType = ObjectTypes.None;

        gameObject.tag = "Untagged";
        myEnumManager.myObjectType = ObjectTypes.None;

        navMesh.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20);
        Destroy(gameObject, 4f);

    }


    public void SunbedGreenOutline(bool active)
    {
        GameObject sunbed = transform.parent.gameObject;
        Material mat = sunbed.transform.GetChild(0).GetComponent<Renderer>().materials[3];
        if (active)
        {
            mat.SetColor("_OutlineColor", Color.green);
            mat.SetFloat("_OutlineWidth", 8);
        }

    }



}
