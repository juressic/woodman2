using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {


    public TerrainCollider map;

    public NavMeshAgent agent;

    private Animator anim;

    public bool cutTree;

    private GameObject currentTree;

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

        //CutTree();

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


    public IEnumerator CutTree(GameObject tree)
    {
        //Vector3 pos = ScreenMapLook().Value;
        Vector3 pos = tree.transform.position;
        agent.destination = pos;

        currentTree = tree;
        Debug.Log("Prepare to cut");
        while ((transform.position - tree.transform.position).magnitude > 2f)
        {
            yield return null;
        }
        //Play Animation
        //
        anim.Play("Axe Swing");
        Debug.Log("Tree Cutting");
        
        //Set in place


        for(int i = 0; i < tree.GetComponent<Tree>().health; i++)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("dest" + i);
            tree.GetComponent<Tree>().health -= 1;
        }
        anim.SetTrigger("TreeDown");
        tree.GetComponent<Animation>().Play();
    }
}
