using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    
    public Transform[] goals;
    private int currentGoal = 0;
   
    NavMeshAgent agent;

   
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goals[currentGoal].position;
    
     
    }
    private void Update()
    {
       
        if (agent.remainingDistance < 0.5f)
        {
            currentGoal = (currentGoal + 1) % goals.Length;
            agent.destination = goals[currentGoal].position;
        }
    }
}
