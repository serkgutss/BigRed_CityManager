using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="Building Preset", menuName = "New Building Preset")]
public class BuilldingPreset : ScriptableObject
{



    public int cost;
    public int costPerTurn;
    public GameObject prefab;
    public int foodPerTurn;

    public int population;
    public int jobs;
    public int food;
    public int militaryPower;
    public int fear;
    public int governmentStatus;

    

   

}
