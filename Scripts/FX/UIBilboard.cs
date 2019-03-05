 using UnityEngine;

public class UIBilboard : MonoBehaviour
{
    public GameObject camera;
    public bool reverse;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!reverse)
            transform.LookAt(camera.transform);

        if(reverse)
            transform.LookAt(2f* transform.position - camera.transform.position);
    }
}
