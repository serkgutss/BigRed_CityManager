using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantBePlaced : MonoBehaviour
{
    public LayerMask ignoreLayer;
    private int radius = 1;
    //private LayerMask myLayerMask;
    private void Start()
    {
        //myLayerMask = LayerMask.GetMask("Building");

    }
    void Update()
    {

        int layerMask = ~ignoreLayer;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
       

        
       
        if (Input.GetKeyDown(KeyCode.Space) && hitColliders.Length >0)
        {

          foreach (Collider collider in hitColliders)
          {
            if (collider != GetComponent<Collider>())
            {
                Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.gameObject);

                City.instance.OnRemoveBuilding(buildingToDestroy);

                    Destroy(gameObject);   

            }
          }
        }
    }
}
