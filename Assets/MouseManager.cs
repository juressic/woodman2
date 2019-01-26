using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{

    public LayerMask clickable;

    public EventVector3 OnClickEnviroment;

    public EventVector3 OnClickTree;

    public Texture2D terrain;
    public Texture2D offTerrain;
    public Texture2D doorway;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity,clickable.value))
        {
            bool door = false;
            if (hit.collider.gameObject.tag == "Terrain")
            {
                Cursor.SetCursor(terrain, new Vector2(16, 16), CursorMode.Auto);
            }
            else if(hit.collider.gameObject.tag == "doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(door == true)
                {
                    Transform doorway1 = hit.collider.gameObject.transform;
                    OnClickEnviroment.Invoke(doorway1.position + doorway1.forward * 10);
                    //Debug.Log("doorway");
                }
                OnClickEnviroment.Invoke(hit.point);
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