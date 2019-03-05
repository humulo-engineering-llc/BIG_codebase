using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadDetailModel : MonoBehaviour
{
    public string assetName;
    public string season;
    BundleManager manager;
    CanvasGroup LoadingCV;

    // Start is called before the first frame update
    void Start()
    {
        //--Get bundle manager
        //--Assign animal to load DO NOT CHANGE THIS
        manager = GameObject.Find("Bundle Manager").GetComponent<BundleManager>();
        Switcheroo(WorldControl.animalSelected);

        //--Loading text
        LoadingCV = GameObject.Find("Loading").GetComponent<CanvasGroup>();
        LoadingCV.alpha = 1f;

        //--Load animal
        GameObject myAss = manager.myAsset.LoadAsset(assetName) as GameObject;
        Instantiate(myAss, transform.position, transform.rotation);
        LoadingCV.alpha = 0f;
    }

    //--Geez guys, so this is a nested switch case that determines which asset to load from the Bundle Manager
    //--Takes the name of WorldControl.animal as first arg, this.instance.season as second arg
    //--By the love of all that is holy, FIND A NEW WAY TO DO THIS
    /*
    -- TEMPLATE --
     case (""):
                switch (season)
                {
                    case ("Spring"):
                        assetName = "REPLACE_Spring_Detail";
                        break;
                    case ("Summer"):
                        assetName = "REPLACE_Summer_Detail";
                        break;
                    case ("Fall"):
                        assetName = "REPLACE_Fall_Detail";
                        break;
                    case ("Winter"):
                        assetName = "REPLACE_Winter_Detail";
                        break;
                }
                break;
         */
    void Switcheroo(string animal)
    {
        switch (animal)
        {
            case ("Gray Wolf"):
                switch (season)
                {
                    case ("Spring"):
                        assetName = "Wolf_Spring_Detail";
                        break;
                    case ("Summer"):
                        assetName = "Wolf_Summer_Detail";
                        break;
                    case ("Fall"):
                        assetName = "Wolf_Fall_Detail";
                        break;
                    case ("Winter"):
                        assetName = "Wolf_Winter_Detail";
                        break;
                }
                break;
            case ("Beluga Whale"):
                switch (season)
                {
                    case ("Spring"):
                        assetName = "Beluga_Spring_Detail";
                        break;
                    case ("Summer"):
                        assetName = "Beluga_Summer_Detail";
                        break;
                    case ("Fall"):
                        assetName = "Beluga_Fall_Detail";
                        break;
                    case ("Winter"):
                        assetName = "Beluga_Winter_Detail";
                        break;
                }
                break;
            case ("Bowhead Whale"):
                switch (season)
                {
                    case ("Spring"):
                        assetName = "Bowhead_Spring_Detail";
                        break;
                    case ("Summer"):
                        assetName = "Bowhead_Summer_Detail";
                        break;
                    case ("Fall"):
                        assetName = "Bowhead_Fall_Detail";
                        break;
                    case ("Winter"):
                        assetName = "Bowhead_Winter_Detail";
                        break;
                }
                break;
        }
    }

}
