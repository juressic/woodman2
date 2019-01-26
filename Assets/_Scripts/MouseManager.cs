using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{

    public LayerMask clickable;

    public EventVector3 OnClickEnviroment;

    public EventVoid OnClickFunction;

    public Texture2D terrain;
    public Texture2D offTerrain;
    public Texture2D tree;

    public Player player;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity,clickable.value))
        {
            bool cutTree = false;
            if (hit.collider.gameObject.tag == "Terrain")
            {
                Cursor.SetCursor(terrain, new Vector2(16, 16), CursorMode.Auto);
            }
            else if(hit.collider.gameObject.tag == "Tree")
            {
                Debug.Log("Tree");
                Cursor.SetCursor(tree, new Vector2(16, 16), CursorMode.Auto);
                cutTree = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(cutTree == true)
                {
                    Debug.Log("tree collision");
                    StartCoroutine(player.CutTree(hit.transform.gameObject));
                }
                else
                {
                    OnClickEnviroment.Invoke(hit.point);
                }
                
            }
        }
        else
        {
            Cursor.SetCursor(offTerrain, new Vector2(16, 16), CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

[System.Serializable]
public class EventVoid : UnityEvent<EventVoid> { }