using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour { 

    public string animalName;
    public GameObject herdControl;
    public float wanderDistance;

    Animator myAC;
    NavMeshAgent nav;
    float wanderTimer;
    float trigger;

    public bool constantWander;
    public bool landAnimal;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator StartUpProcedure()
    {
        yield return new WaitForSeconds(1f);

        nav = GetComponent<NavMeshAgent>();
        if (landAnimal)
        {
            myAC = GetComponent<Animator>();
        }
        if (!landAnimal)
        {
            myAC = GetComponentInChildren<Animator>();
        }

        if (!constantWander)
            trigger = Random.Range(0f, 30f);
        if (constantWander)
            trigger = 2f;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        wanderTimer += Time.deltaTime;
        if (wanderTimer >= trigger)
        {
            Wander();
        }

        if (landAnimal)
        {
            if (nav.velocity.magnitude >= 0.1f)
            {
                myAC.SetBool("isWalking", true);
            }
            else
            {
                myAC.SetBool("isWalking", false);
            }
        }
        if (!landAnimal)
        {
            foreach (AnimatorControllerParameter param in myAC.parameters)
            {
                myAC.SetBool(param.name, false);
            }
        }
    }

    public void Wander()
    {
        Vector3 newPos = RandomNavSphere(herdControl.transform.position, wanderDistance, -1);
        nav.SetDestination(newPos);
        wanderTimer = 0f;
        if(!constantWander)
            trigger = Random.Range(0f, 30f);
        if (constantWander)
            trigger = Random.Range(0f, 2f);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
