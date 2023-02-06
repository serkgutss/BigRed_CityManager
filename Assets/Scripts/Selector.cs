using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Selector : MonoBehaviour
{

 
    private GameObject placementInstance;
    private Camera cam;

    public static Selector instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("house") && Input.GetKeyDown(KeyCode.R))
            {
                hit.transform.Rotate(0, 90, 0);

            }
            if (hit.collider.gameObject.CompareTag("farm") && Input.GetKeyDown(KeyCode.R))
            {
                hit.transform.Rotate(0, 0, 90);

            }
            if (hit.collider.gameObject.CompareTag("gulag") && Input.GetKeyDown(KeyCode.R))
            {
                hit.transform.Rotate(0, 0, 90);

            }
            if (hit.collider.gameObject.CompareTag("factory") && Input.GetKeyDown(KeyCode.R))
            {
                hit.transform.Rotate(0, 90, 0);

            }
            if (hit.collider.gameObject.CompareTag("barracks") && Input.GetKeyDown(KeyCode.R))
            {
                hit.transform.Rotate(0, 0, 90);

            }
            if (hit.collider.gameObject.CompareTag("bankFood") && Input.GetKeyDown(KeyCode.R))
            {
                hit.transform.Rotate(0, 90, 0);

            }
        }
       
    }
    //returns the tile that the mouse is hovering over
    public Vector3 GetCurTilePosition()
    {
        //return if we've hovering over UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return new Vector3(0, -99, 9);
        }

        //create the plane, ray and out distance
        Plane plane = new Plane(Vector3.up, Vector3.zero);  
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
       
        float rayOut = 0.0f;

       
     

        if (plane.Raycast(ray, out rayOut))
        {
          
            //get the position at which we intersected the plane
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);

            //round that up to the nearest full number (nearest meter)
            newPos = new Vector3(Mathf.CeilToInt(newPos.x),0f, Mathf.CeilToInt(newPos.z));
         

            return newPos;
        }

        return new Vector3(0, -99, 9);
    }
}
