using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {


    public TerrainCollider map;

    public NavMeshAgent agent;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private Vector3? ScreenMapLook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!map.Raycast(ray, out hit, Mathf.Infinity))
            return null;

        return hit.point;
    }
    
	void Update () {

        Debug.Log(transform.localRotation.y);
        anim.SetFloat("Move", agent.velocity.magnitude);
        //anim.SetFloat("Side", transform.localRotation.y);
        //anim.SetFloat("YRotation", transform.localRotation.y);
        //GetComponent<Animator>().SetFloat("Move", transform.localRotation.y);


        if (Input.GetMouseButton(1))
        {
            transform.LookAt(ScreenMapLook().Value);
            anim.SetBool("RightClick", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            transform.rotation = transform.parent.rotation;
            anim.SetBool("RightClick", false);
        }
	}
}
