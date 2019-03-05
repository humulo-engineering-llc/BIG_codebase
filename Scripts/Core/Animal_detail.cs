using UnityEngine;
using TMPro;

public class Animal_detail : MonoBehaviour
{
    public string detailName;
    public string detailText;

    public string triggerName;

    public GameObject animal;
    public TextMeshPro label;

    public Color visible;
    public Color invisible;

    public float fadeRate;
    float alpha;

    void Start()
    {
        alpha = 0f;

        //--Create label
        GameObject lbl = Resources.Load("Label") as GameObject;
        GameObject newLabel = Instantiate(lbl);
        newLabel.transform.SetParent(this.transform);
        newLabel.transform.position = this.transform.position;
        label = newLabel.GetComponent<TextMeshPro>();
        label.text = detailName;
    }

    void Update()
    {
        //--Pass info to infobox
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            if(hitInfo.transform.gameObject == this.gameObject)
            {
                if(alpha < 1f)
                {
                    alpha += Time.deltaTime * fadeRate;
                }
                Color myCol = new Color(255, 255, 255, alpha);
                label.color = myCol;
            } else
            {
                if(alpha > 0f)
                {
                    alpha -= Time.deltaTime * fadeRate;
                }
                Color myCol = new Color(255, 255, 255, alpha);
                label.color = myCol;
            }
        } else
        {
            if (alpha > 0f)
            {
                alpha -= Time.deltaTime * fadeRate;
            }
            Color myCol = new Color(255, 255, 255, alpha);
            label.color = myCol;
        }
    }
}
