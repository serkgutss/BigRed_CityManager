using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitcher : MonoBehaviour
{

    public Light myLight;

    void Start()
    {
        StartCoroutine(switchLights());
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    public IEnumerator switchLights()
    {
        while (true) { 

            myLight.enabled = false;
        yield return new WaitForSeconds(Random.Range(4f, 30f));
        myLight.enabled = true;
        yield return new WaitForSeconds(Random.Range(4f, 30f));
        myLight.enabled = false;
        }



    }
}
