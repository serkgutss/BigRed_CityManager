using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class City : MonoBehaviour
{
    public GameObject panelGameOver;


    public int brokeStrike;
    public int curGovernmentStatus;
    public int curMilitaryPower;
    public int curFear;
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJob;

    public TextMeshProUGUI statsText;

    public List<Building> buildings = new List<Building>();

    public static City instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateStatText();
    }
    private void Update()
    {
        
       
        incomePerJob = 1500; 
    }
    //called when we place down a building
    public void OnPlaceBuilding (Building building)
    {
        buildings.Add(building);

        money -= building.preset.cost;

        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;

        UpdateStatText();
    }

    //called when we bulldoze a building
    public void OnRemoveBuilding(Building building)
    {
        buildings.Remove(building);

        money += building.preset.cost;
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        //Destroy(gameObject);

        UpdateStatText();
    }

    public void EndTurn()
    {

        if (money<=0)
        {
            brokeStrike++;
        }
        if (brokeStrike==5)
        {
            GameOver();
        }
        day++;

        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        CalculateFoodperTurn();
        UpdateStatText();
        CalculateFear();
        CalculateMilitaryPower();
        CalculateGovernmentStatus();

        DayNightSystem.instance.transform.rotation = Quaternion.identity;
        DayNightSystem.instance.ResetTimer();
        if (curGovernmentStatus >= 5)
        {
            curFood += 10;
        }

        if (curFear >0)
        {
            curPopulation -= 5;
        }
    }

    private void UpdateStatText()
    {
        statsText.text = String.Format("Day:{0} Roubles:{1} Pop:{2}/{3} Jobs:{4}/{5} Food:{6} MilitaryPower:{7} Fear:{8} GovernmentStatus:{9}", new object[10] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood, curMilitaryPower, curFear, curGovernmentStatus });
    }

    private void CalculateFood()
    {
        curFood = 0;

        foreach (Building building in buildings)
            curFood += building.preset.food;
        
    }
    private void CalculateFoodperTurn()
    {
        

        foreach (Building building in buildings)
            curFood += building.preset.foodPerTurn;

    }
    private void CalculateFear()
    {
        curFear = 0;

        foreach (Building building in buildings)
            curFear += building.preset.fear;

    }
    private void CalculateMilitaryPower()
    {
        curMilitaryPower = 0;

        foreach (Building building in buildings)
            curMilitaryPower += building.preset.militaryPower;

    }
    private void CalculateGovernmentStatus()
    {
        curGovernmentStatus = 0;

        foreach (Building building in buildings)
            curGovernmentStatus += building.preset.governmentStatus;

        

    }

    private void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
        if (curJobs <= 0)
        {
            curJobs = 0;
        }
    }

    private void CalculatePopulation()
    {
        if (curFood >= curPopulation && curPopulation < maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4 ) , maxPopulation);
        }
        else if (curFood < curPopulation)
        {
            curPopulation = curFood;
        }
        if (curPopulation <= 0)
        {

            curPopulation = 0;
        }
    }

    private void CalculateMoney()
    {
        money += curJobs * incomePerJob;

        foreach(Building building in buildings) 
            money -= building.preset.costPerTurn;
    }


    private void GameOver()
    {

        panelGameOver.SetActive(true);
        Time.timeScale = 0;

    }
}
