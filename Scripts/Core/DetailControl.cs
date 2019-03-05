using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetailControl : MonoBehaviour
{
    public CanvasGroup loadingText;
    public CanvasGroup infoBox;

    Text infoTitle;
    Text infoText;

    GameObject ActiveObject;
    bool infoUp;

    void Start()
    {
        loadingText = GameObject.Find("Loading").GetComponent<CanvasGroup>();
        loadingText.alpha = 0f;
        infoBox = GameObject.Find("InfoBox").GetComponent<CanvasGroup>();
        infoBox.alpha = 0f;
        infoTitle = GameObject.Find("Title").GetComponent<Text>();
        infoText = GameObject.Find("InfoText").GetComponent<Text>();

        ActiveObject = null;
        infoUp = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Detail")
                {
                    ActiveObject = hitInfo.transform.gameObject;
                    infoUp = true;
                    infoTitle.text = ActiveObject.GetComponent<Animal_detail>().detailName;
                    infoText.text = ActiveObject.GetComponent<Animal_detail>().detailText;


                    string trigger = ActiveObject.GetComponent<Animal_detail>().triggerName;
                    Animator myAnim = ActiveObject.GetComponent<Animal_detail>().animal.GetComponent<Animator>();
                    foreach (AnimatorControllerParameter param in myAnim.parameters)
                    {
                        myAnim.SetBool(param.name, false);
                    }
                    myAnim.SetBool(trigger, true);
                }
            }
        }

        if (infoUp && infoBox.alpha < 1f)
        {
            infoBox.alpha += Time.deltaTime;
        }
        if(!infoUp && infoBox.alpha > 0f)
        {
            infoBox.alpha -= Time.deltaTime;
        }
    }

    public void LoadOverworld()
    {
        loadingText.alpha = 1f;
        SceneManager.LoadScene(Season.PreviousLevel, LoadSceneMode.Single);
    }
    public void CloseInfoBox()
    {
        string trigger = ActiveObject.GetComponent<Animal_detail>().triggerName;
        Animator myAnim = ActiveObject.GetComponent<Animal_detail>().animal.GetComponent<Animator>();
        foreach(AnimatorControllerParameter param in myAnim.parameters)
        {
            myAnim.SetBool(param.name, false);
        }

        ActiveObject = null;
        infoUp = false;
        
    }
}
