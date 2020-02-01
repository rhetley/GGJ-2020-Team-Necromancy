using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    public GameObject partPrefab;

    public GameObject parentObject;

    public float partDistance = .5f;

    public bool reset, spawn, spawnPart, spawnCoroutine, stopCoroutines;

    public GameObject lastPart;

    public GameObject weightGO;

    public int currentCount;

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
            SpawnPart();

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
    }

    public void Spawn()
    {
        //int count = (int)(length / partDistance);
        int count = 10;
        for(int i = 0; i < count; i++)
        {
            SpawnPart();
        }
    }

    public void SpawnPart()
    {
        GameObject tmp;
        if (currentCount == 0)
        {
            tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, parentObject.transform);

        }
        else
        {
            tmp = Instantiate(partPrefab, new Vector3(lastPart.transform.position.x + (Mathf.Sin(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.y + (Mathf.Cos(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.z), Quaternion.identity, parentObject.transform);

            tmp.transform.eulerAngles = new Vector3(0, 0, lastPart.transform.eulerAngles.z);
        }

        if(currentCount == 0)
        {
            tmp.GetComponent<HingeJoint2D>().connectedBody = parentObject.GetComponent<Rigidbody2D>();

        }
        else
        {
            tmp.GetComponent<HingeJoint2D>().connectedBody = lastPart.GetComponent<Rigidbody2D>();
        }


        lastPart = tmp;
        weightGO.transform.position = new Vector3(lastPart.transform.position.x + (Mathf.Sin(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.y + (Mathf.Cos(lastPart.transform.eulerAngles.z - 90) * partDistance), lastPart.transform.position.z);

        tmp.name = parentObject.transform.childCount.ToString();


        
        currentCount++;

        weightGO.GetComponent<DistanceJoint2D>().distance = currentCount + .5f;
        weightGO.GetComponent<HingeJoint2D>().connectedBody = lastPart.GetComponent<Rigidbody2D>();
    }


    public void Reset()
    {
        foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("Rope"))
        {
            Destroy(tmp);
        }
        weightGO.transform.position = parentObject.transform.position;
        weightGO.GetComponent<DistanceJoint2D>().distance = .5f;
        currentCount = 0;
    }
    IEnumerator SpawnRopeOnTimer(float interval)
    {
        for(int i = 0; i < 100; i++)
        {
            SpawnPart();
            yield return new WaitForSeconds(interval);
        }
    }
}
