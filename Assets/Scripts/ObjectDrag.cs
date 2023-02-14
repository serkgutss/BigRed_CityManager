using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
   
    private Vector3 offset;
  

    private void Start()
    {
      

       
    }
    private void OnMouseDown()
    {
        offset = transform.position - GridBuildingSystem.GetMouseWorldPosition();
    }


   

    private void OnMouseDrag()
    {
        
        Vector3 pos = GridBuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = GridBuildingSystem.current.SnapCoordinateToGrid(pos);
    }


}
