using UnityEngine;
using System.Collections;

public class Navmesh : MonoBehaviour {

    private NavMeshAgent agent;
    private Transform[] points = new Transform[4];
    private int index = 0;

    //private Animator anim;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        for(int i = 0; i < 4; i++)
        {
            points[i] = GameObject.Find("Plane").transform.FindChild("point" + i);
        }
        agent.SetDestination(points[0].position);
      //  anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	if(agent.hasPath && agent.remainingDistance < agent.stoppingDistance)
        {
            agent.SetDestination(points[(index + 1) % 4].position);
            index = (index + 1) % 4;
        }
    //    SetAnim();
	}

    //void SetAnim()
    //{
    //    anim.SetBool("Walk", 1 < agent.velocity.magnitude);
    //}


}