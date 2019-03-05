using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationDesync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimCountdown());
    }

    IEnumerator AnimCountdown()
    {
        Animation myAnim = this.GetComponent<Animation>();
        myAnim.Stop();
        float waitTime = Random.Range(0.0f, 1.5f);
        yield return new WaitForSeconds(waitTime);
        myAnim.Play();
    }
}
