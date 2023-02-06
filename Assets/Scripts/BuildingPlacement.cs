using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{

 
    private bool currentlyPlacing;
    private bool currentlyBulldozering;


    private BuilldingPreset curBuildingPreset;


    GameObject lastInstantiatedObject;
    private float indicatorUpdateRate = 0.05f; //seconds
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;
    public List<Building> buildings = new List<Building>();


    public GameObject panelMoneyWarning;
    public GameObject bulldozeIndicator;
    public GameObject house, farm, factory, road, gulag, barracks, bankFood;

    

 
    private void CambiarIndicador(GameObject indicadorObjetivo)
    {
        barracks.SetActive(false);
        house.SetActive(false);
        farm.SetActive(false);
        factory.SetActive(false);
        road.SetActive(false);
        gulag.SetActive(false);
        bulldozeIndicator.SetActive(false);

        indicadorObjetivo.SetActive(true);
    }
    //called when we press a building UI button
    public void BeginNewBuildingPlacementHouse(BuilldingPreset preset)
    {
        //TODO: make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        CambiarIndicador(house);
       
       

    }
    public void BeginNewBuildingPlacementFarm(BuilldingPreset preset)
    {
        //TODO: make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        CambiarIndicador(farm);


    }
    public void BeginNewBuildingPlacementFactory(BuilldingPreset preset)
    {
        //TODO: make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        CambiarIndicador(factory);


    }
    public void BeginNewBuildingPlacementRoad(BuilldingPreset preset)
    {
        //TODO: make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        CambiarIndicador(road);


    }
    public void BeginNewBuildingPlacementGulag(BuilldingPreset preset)
    {
        //TODO: make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        CambiarIndicador(gulag);


    }
    public void BeginNewBuildingPlacementBarracks(BuilldingPreset preset)
    {
        //TODO: make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        CambiarIndicador(barracks);


    }

    public void BeginNewBuildingPlacementBankFood(BuilldingPreset preset)
    {
        //TODO: make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        CambiarIndicador(bankFood);


    }
    //called when we place down a building or press Escape
    private void CancelBuildingPlacement()
    {
        
        currentlyPlacing = false;
       
       
    }

    //turn bulldoze on off
    public void ToggleBulldoze()
    {
        currentlyBulldozering = !currentlyBulldozering;
        CambiarIndicador(bulldozeIndicator);
    }


  
    private void Update()
    {
        
        //cancel building placement
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();

        //called every 0.05 seconds 
        if (Time.time - lastUpdateTime > indicatorUpdateRate)
        {
            lastUpdateTime = Time.time;

            //get the currently selected tile position
            curIndicatorPos = Selector.instance.GetCurTilePosition();

            //move the placement indicator or bulldoze indicator to the selected tile
            if (currentlyPlacing)
                house.transform.position = curIndicatorPos;
            if (currentlyPlacing)
                bankFood.transform.position = curIndicatorPos;
            if (currentlyPlacing)
                farm.transform.position = curIndicatorPos;
            if (currentlyPlacing)
                factory.transform.position = curIndicatorPos;
            if (currentlyPlacing)
                road.transform.position = curIndicatorPos;
            if (currentlyPlacing)
                gulag.transform.position = curIndicatorPos;
            if (currentlyPlacing)
                barracks.transform.position = curIndicatorPos;
            else if(currentlyBulldozering)
                bulldozeIndicator.transform.position = curIndicatorPos;
        }

        //called when we press left mouse button
        if (  Input.GetMouseButtonDown(0) && currentlyPlacing && City.instance.money > curBuildingPreset.cost)
        {
            PlaceBuilding();
         
            
        }
        else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)
            Bulldoze();
    }

    //deletes the currently selected building
    private void Bulldoze()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x=>x.transform.position == curIndicatorPos);

        if (buildingToDestroy != null)
        {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }

    //places down the currently selected building
    private void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
        


        City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());

       

            if (!curBuildingPreset.prefab.tag.Equals("Road"))
            CancelBuildingPlacement();
    }

     
   

    




    public IEnumerator MoneyWarning()
    {

       
            panelMoneyWarning.SetActive(true);


            yield return new WaitForSeconds(2);

            panelMoneyWarning.SetActive(false);
            


      
        

    }
}

