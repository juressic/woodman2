using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {
    
    public int health = 10;
    public GameObject snowSmesh;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            Instantiate(snowSmesh, transform.position + transform.up * 1.4f, transform.rotation);
        }
    }
}
