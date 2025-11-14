using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class ScriptCapsule : MonoBehaviour
{
	private Vector3 destination;
	private NavMeshAgent navAgent;



    void Start()
    {
		navAgent = GetComponent<NavMeshAgent>();
	}

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay.origin, camRay.direction, out RaycastHit infosCollision, 5000f))
            {
                navAgent.SetDestination(infosCollision.point);
            }

        }
	}
}
