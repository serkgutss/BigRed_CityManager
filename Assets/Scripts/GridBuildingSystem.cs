using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using Unity.VisualScripting;

public class GridBuildingSystem : MonoBehaviour
{
    private AudioSource built;
    
    public bool canPlace = true;


    
    public static GridBuildingSystem current;
    private bool currentlyPlacing;
    
    private BuilldingPreset buildingPreset;
    public GridLayout gridLayout;
    private Grid grid;
    
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private TileBase whiteTile;

    

    private PlaceableObject objectToPlace;

    #region Unity methods

    private void Awake()
    {

        built= GetComponent<AudioSource>();   
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    public void BeginNewBuildingPlacementHouse(BuilldingPreset preset)
    {
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace == true)
        {
            canPlace = false;
            PlaceBuilding();
        }
        currentlyPlacing = true;





    }

    
    public void BeginNewBuildingPlacementFarm(BuilldingPreset preset)
    {
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace ==true)
        {
            canPlace = false;
            PlaceBuilding();
        }
        currentlyPlacing = true;
       
       


    }
    public void BeginNewBuildingPlacementFactory(BuilldingPreset preset)
    {

        var road = 
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace == true)
        {
            canPlace = false;
            PlaceBuilding();
        }
        currentlyPlacing = true;
       





    }
   
    public void BeginNewBuildingPlacementRoad2(BuilldingPreset preset)
    {
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace == true)
        {


            canPlace = false;
            PlaceBuilding();
        }

        currentlyPlacing = true;



        //preset.prefab.transform.SetParent(roadContainer.transform);
        // surface.UpdateNavMesh(surface.navMeshData);




    }
    public void BeginNewBuildingPlacementRoad3(BuilldingPreset preset)
    {
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace == true)
        {


            canPlace = false;
            PlaceBuilding();
        }

        currentlyPlacing = true;



        //preset.prefab.transform.SetParent(roadContainer.transform);
        // surface.UpdateNavMesh(surface.navMeshData);




    }
    public void BeginNewBuildingPlacementGulag(BuilldingPreset preset)
    {
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace == true)
        {
            canPlace = false;
            PlaceBuilding();
        }
        currentlyPlacing = true;
      
        
       


    }
    public void BeginNewBuildingPlacementBarracks(BuilldingPreset preset)
    {
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace == true)
        {
            canPlace = false;
            PlaceBuilding();
        }
        currentlyPlacing = true;
        
        


    }

    public void BeginNewBuildingPlacementBankFood(BuilldingPreset preset)
    {
        buildingPreset = preset;
        if (currentlyPlacing && City.instance.money > buildingPreset.cost && canPlace==true)
        {
            canPlace = false;
            PlaceBuilding();
        }
        
        currentlyPlacing = true;
       
        


    }
    private void Update()
    {

     

        if (!objectToPlace)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            objectToPlace.Rotate();


        }

      else if (Input.GetKeyDown(KeyCode.Space) && canPlace ==false)
      {

        
        if (CanBePlaced(objectToPlace))
        {
                City.instance.OnPlaceBuilding(buildingPreset.prefab.GetComponent<Building>());
                canPlace =true;
            objectToPlace.Place();
                built.Play();
                //Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                //TakeArea(start, objectToPlace.Size);
        }
           
            else
            {
            Destroy(objectToPlace.gameObject);
            }
      }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(objectToPlace.gameObject);
        }
    }
    #endregion

    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
                return Vector3.zero;
        }


    }

    private void PlaceBuilding()
    {
        InitializeWithObject(buildingPreset.prefab);



        

    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {

        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;



    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var vector3Int in area.allPositionsWithin)
        {

            Vector3Int pos = new Vector3Int(vector3Int.x, vector3Int.y, z: 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;

    }
    #endregion
    #region Building Placement



    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
        



        //MonoBehaviour script = obj.GetComponent(CantBePlaced) as MonoBehaviour;
        //if (script != null)
        //{
        //    Destroy(script);
        //}


    }



    private bool CanBePlaced(PlaceableObject placeableObject)
    {
       
        Building buildingToDestroy = City.instance.buildings.Find(b => b.gameObject);
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = new Vector3Int(placeableObject.Size.x + 1, placeableObject.Size.y + 1, placeableObject.Size.z);

        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);


        return true;
    }


    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        mainTilemap.BoxFill(start, whiteTile, startX:start.x, startY:start.y, endX:start.x + size.x, endY:start.y + size.y);


    }
    #endregion
   
  
  
}

