using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Vector3 newLoc;
    public Vector3 curLoc;

    public GameObject PlayerCam;
    public GameObject MinimapCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            curLoc = PlayerCam.transform.position;
            RaycastHit hit;
            Ray ray = MinimapCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newLoc = hit.point;
                Vector3 tmpLoc = new Vector3(newLoc.x, curLoc.y, newLoc.z);
                PlayerCam.transform.position = tmpLoc;
            }
        }
    }
}
