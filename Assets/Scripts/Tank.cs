using UnityEngine;
using UnityEngine.AI;


public class Tank : MonoBehaviour
{
   

    public NavMeshAgent agent;
    public Vector3[] path;
    public int currentPoint = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(path[0]);
    }
    

   
    private void Update()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *5);
        agent.Move(agent.desiredVelocity * Time.deltaTime);

        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
        {
            currentPoint++;
            if (currentPoint >= path.Length)
            {
                currentPoint = 0;
            }
            agent.SetDestination(path[currentPoint]);
        }
    }
}
