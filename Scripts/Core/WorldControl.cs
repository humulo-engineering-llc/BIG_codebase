using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldControl : MonoBehaviour
{
    GameObject ActiveObject;
    Camera playerCam;
    CanvasGroup infoGroup;

    //--Stores variables for animal scenes
    public static string animalSelected;
    bool animalSelected_Land;

    bool inInfoBox;

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        ActiveObject = null;
        infoGroup = GameObject.Find("infoGroup").GetComponent<CanvasGroup>();
        inInfoBox = false;
        infoGroup.alpha = 0f;
        infoGroup.interactable = false;
        infoGroup.blocksRaycasts = false;
        animalSelected = "none";
    }

    void Update()
    {
        //--Raycast to select animals or objects
        //--If animal, make sure to attach Animal script to animals
        //--and Animal_detail to detail objects
        if (Input.GetKeyDown(KeyCode.Mouse0) && !inInfoBox)
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(playerCam.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if(hitInfo.transform.gameObject.tag == "Detail")
                {
                    ActiveObject = hitInfo.transform.gameObject;
                    infoGroup.interactable = true;
                    infoGroup.blocksRaycasts = true;

                    GameObject animalName = infoGroup.transform.Find("Panel/TitleBar").gameObject;
                    GameObject titleBlock = animalName.transform.Find("AnimalName").gameObject;
                    Text title = titleBlock.GetComponent<Text>();
                    animalSelected = ActiveObject.GetComponent<Overworld_detail>().detailName;
                    title.text = animalSelected;
                    animalSelected_Land = ActiveObject.GetComponent<Overworld_detail>().landAnimal;
                }
                else
                {
                    ActiveObject = null;
                    infoGroup.interactable = false;
                    infoGroup.blocksRaycasts = false;
                }
            }
        }

        //--Infobox fade in/out
        if (ActiveObject != null)
        {
            if(infoGroup.alpha < 1.0f)
            {
                infoGroup.alpha += Time.deltaTime;
            }

        } else
        {
            if(infoGroup.alpha > 0f)
            {
                infoGroup.alpha -= Time.deltaTime;
            }
        }
    }

    //--Load detail scene
    public void LoadMore(bool isLandAnimal)
    {
        if (animalSelected_Land)
        {
            SceneManager.LoadScene("Land Detail", LoadSceneMode.Single);
        } else
        {
            SceneManager.LoadScene("Water Detail", LoadSceneMode.Single);
        }
    }

    //--HELPERS
    //--Determines if mouse is inside of Infobox (prevents closing infobox prematurely)
    public void mouseIn_InfoBox()
    {
        inInfoBox = true;
    }
    public void mouseOut_InfoBox()
    {
        inInfoBox = false;
    }
    //--Clears active object var (use also to close infobox)
    public void ClearActiveSelection()
    {
        ActiveObject = null;
        infoGroup.interactable = false;
        infoGroup.blocksRaycasts = false;
        animalSelected = "none";
    }
}
