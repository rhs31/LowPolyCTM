using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
	public LayerMask m_LayerMaskFloor;
	private UnityEngine.AI.NavMeshAgent navMeshAgent;
	private Animesh animesh;
    private bool m_MoveDisabled;

	void Awake()
	{
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		animesh = transform.GetChild(0).GetComponent<Animesh>();
	}

	void HandleTouch()
	{           
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, m_LayerMaskFloor))
		{              
			navMeshAgent.destination = hit.point;
			navMeshAgent.isStopped = false;
		}
	}

	void Update()
	{
        if (m_MoveDisabled) return;
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
		{
			if (!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
				animesh.m_CurrentAnimesh = "Idle";
		}
		else
		{
			animesh.m_CurrentAnimesh = "Walk";
		}
		if (Input.GetMouseButton(0))
		{
			HandleTouch();
		}
	}

    void OnDeath()
    {
        m_MoveDisabled = true;
        navMeshAgent.isStopped = true;
        animesh.m_CurrentAnimesh = "Idle";
    }

}
