using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObject : MonoBehaviour
{
    BundleManager manager;
    public string assetName;
    public bool loadFromResources;

    // Start is called before the first frame update
    void Start()
    {
        GameObject asset;

        if (!loadFromResources)
        {
            manager = GameObject.Find("Bundle Manager").GetComponent<BundleManager>();
            asset = manager.myAsset.LoadAsset(assetName) as GameObject;
            Instantiate(asset, transform.position, transform.rotation);
        }
        if (loadFromResources)
        {
            asset = Resources.Load(assetName) as GameObject;
            Instantiate(asset, transform.position, transform.rotation);
        }

        
    }
}
