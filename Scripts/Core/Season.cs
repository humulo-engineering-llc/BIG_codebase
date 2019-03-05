using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
             //
public class Season : MonoBehaviour
{
    public CanvasGroup Loading;
    public static string PreviousLevel;

    void Start()
    {
        PreviousLevel = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (Application.isLoadingLevel)
            Loading.alpha = 1f;
    }

    //--Change season with string Var
    public void ChangeSeason(string ssn)
    {
        SceneManager.LoadScene(ssn, LoadSceneMode.Single);
    }
}
