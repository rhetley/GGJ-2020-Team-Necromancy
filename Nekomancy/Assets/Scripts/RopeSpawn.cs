using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    public GameObject partPrefab;

    public GameObject parentObject;

    //public float partDistance = .5f;

    public bool reset, spawn, spawnPart, spawnCoroutine, stopCoroutines, retract;

    public GameObject lastPart;

    public GameObject weightGO;

    public int currentCount;

    public float allowedDistancePerPart = 3f;

    public float PIVOT_TO_CRYSTAL;
    public float ALLOWEDDISTANCE_COUNT;
    // Update is called once per frame
    void Update()
    {
        if(reset)
        {
            Reset();
            reset = false;
        }

        if(spawn)
        {
            Spawn();

            spawn = false;
        }

        if(spawnPart)
        {
            SpawnPartStartToWeight();

            spawnPart = false;
        }

        if(spawnCoroutine)
        {
            StopAllCoroutines();

            spawnCoroutine = false;

            StartCoroutine(SpawnRopeOnTimer(.25f));
        }

        if (stopCoroutines)
        {
            StopAllCoroutines();

            stopCoroutines = false;
        }

        if(retract)
        {
            Retract();

            retract = false;
        }


        ALLOWEDDISTANCE_COUNT = (currentCount * allowedDistancePerPart);
        PIVOT_TO_CRYSTAL = (parentObject.transform.position - weightGO.transform.position).magnitude;
        while (ALLOWEDDISTANCE_COUNT < PIVOT_TO_CRYSTAL)
        {
            Debug.Log("MORE");
            SpawnPartWeightToStart();
            ALLOWEDDISTANCE_COUNT = (currentCount * allowedDistancePerPart);
            PIVOT_TO_CRYSTAL = (parentObject.transform.position - weightGO.transform.position).magnitude;
        }
        
        
    }

    public void Spawn()
    {
        //int count = (int)(length / partDistance);
        int count = 10;
        for(int i = 0; i < count; i++)
        {
            SpawnPartStartToWeight();
        }
    }

    public void SpawnPartStartToWeight()
    {
        GameObject tmp;
        tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, parentObject.transform);
        if (currentCount == 0)
        {
            weightGO.GetComponentInChildren<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();
        }
        else
        {
            //tmp = Instantiate(partPrefab, new Vector3(lastPart.transform.position.x + (Mathf.Sin(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.y + (Mathf.Cos(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.z), Quaternion.identity, parentObject.transform);

            //tmp.transform.eulerAngles = new Vector3(0, 0, lastPart.transform.eulerAngles.z);
        }
        tmp.GetComponent<HingeJoint2D>().anchor = new Vector2(0, 0);
        tmp.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        tmp.GetComponent<HingeJoint2D>().connectedBody = parentObject.GetComponent<Rigidbody2D>();
        //lastPart.GetComponent<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();
        
        if (currentCount == 0)
        {
            weightGO.GetComponentInChildren<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();
            //tmp.GetComponent<HingeJoint2D>().connectedBody = parentObject.GetComponent<Rigidbody2D>();

        }
        
        else
        {
            lastPart.GetComponent<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();

            lastPart.GetComponent<HingeJoint2D>().anchor = new Vector2(0, .5f);
            lastPart.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
        

        lastPart = tmp;
        //weightGO.transform.position = new Vector3(lastPart.transform.position.x + (Mathf.Sin(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.y + (Mathf.Cos(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.z);

        tmp.name = parentObject.transform.childCount.ToString();


        
        currentCount++;

        //weightGO.GetComponent<HingeJoint2D>().connectedBody = lastPart.GetComponent<Rigidbody2D>();
    }
    public void SpawnPartWeightToStart()
    {
        GameObject tmp;
        if (currentCount == 0)
        {
            tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, parentObject.transform);

            tmp.GetComponent<HingeJoint2D>().connectedBody = parentObject.GetComponent<Rigidbody2D>();
            tmp.GetComponent<HingeJoint2D>().anchor = new Vector2(0, 0);
            tmp.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
        else
        {
            tmp = Instantiate(partPrefab, new Vector3(lastPart.transform.position.x, lastPart.transform.position.y, lastPart.transform.position.z), Quaternion.identity, parentObject.transform);

            tmp.GetComponent<HingeJoint2D>().connectedBody = lastPart.GetComponent<Rigidbody2D>();
            tmp.transform.eulerAngles = new Vector3(0, 0, lastPart.transform.eulerAngles.z);
        }
        weightGO.GetComponentInChildren<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();

        //lastPart.GetComponent<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();

        /*
        if (currentCount == 0)
        {
            weightGO.GetComponentInChildren<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();
            //tmp.GetComponent<HingeJoint2D>().connectedBody = parentObject.GetComponent<Rigidbody2D>();

        }

        else
        {
            lastPart.GetComponent<HingeJoint2D>().connectedBody = tmp.GetComponent<Rigidbody2D>();
        }
        */


        lastPart = tmp;
        //weightGO.transform.position = new Vector3(lastPart.transform.position.x + (Mathf.Sin(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.y + (Mathf.Cos(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.z);

        tmp.name = parentObject.transform.childCount.ToString();



        currentCount++;

        //weightGO.GetComponent<HingeJoint2D>().connectedBody = lastPart.GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("Rope"))
        {
            Destroy(tmp);
        }
        weightGO.transform.position = parentObject.transform.position;
        currentCount = 0;
    }
    IEnumerator SpawnRopeOnTimer(float interval)
    {
        for(int i = 0; i < 100; i++)
        {
            SpawnPartStartToWeight();
            yield return new WaitForSeconds(interval);
        }
    }

    public void Retract()
    {
        if (currentCount > 0)
        {
            GameObject tmp = GameObject.Find(currentCount.ToString());// parentObject.transform.childCount.ToString();
            Destroy(tmp);
            currentCount--;
            if (currentCount == 0)
            {
                weightGO.GetComponent<HingeJoint2D>().connectedBody = parentObject.GetComponent<Rigidbody2D>();
            }
            else
            {
                lastPart = GameObject.Find(currentCount.ToString());
                lastPart.GetComponent<HingeJoint2D>().connectedBody = parentObject.GetComponent<Rigidbody2D>();
            }
        }
        
    }
}
