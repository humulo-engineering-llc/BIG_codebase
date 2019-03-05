using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class BundleManager : MonoBehaviour
{
    public string assetBundleName;
    public string URL;
    public AssetBundle myAsset;

    public bool local;

    void Start()
    {
        //--Read local vs server file
        if (local)
        {
            StartCoroutine(GetLocalBundle());
        } else
        {
            StartCoroutine(DownloadBundle());
        }

        //--Keeps the bundle alive
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator GetLocalBundle()
    {
        myAsset = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, assetBundleName));
        yield return myAsset != null;
        if (myAsset != null)
            SceneManager.LoadScene("Overworld_Summer", LoadSceneMode.Single);
        
    }

    IEnumerator DownloadBundle()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(URL);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            myAsset = DownloadHandlerAssetBundle.GetContent(www);
            if (myAsset != null)
                SceneManager.LoadScene("Overworld_Summer", LoadSceneMode.Single);
        }
    }
}
