using UnityEngine;

public class NoFog : MonoBehaviour
{
    bool isThereFog;

    // Start is called before the first frame update
    void Start()
    {
        isThereFog = RenderSettings.fog;
    }

    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = isThereFog;
    }
}
