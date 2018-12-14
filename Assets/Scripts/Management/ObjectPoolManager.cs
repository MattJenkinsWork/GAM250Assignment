using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    public GameObject objectToPool;
    public int amountToPool = 1000;

    public int poolID;


    List<GameObject> inactiveObjects;
    List<GameObject> activeObjects;

   

	// Use this for initialization
	void Start ()
    {
        //Setup the pool lists
        inactiveObjects = new List<GameObject>(new GameObject[amountToPool]);
        activeObjects = new List<GameObject>(new GameObject[amountToPool]);

        //Set the pool ID to something else if there are other pools on the same object
        if (!GetComponent<ObjectPoolManager>())
        {
            poolID = 0;
        }
        else
        {
            poolID = 0;

            ObjectPoolManager[] poolManagers = GetComponents<ObjectPoolManager>();

            for (int i = 0; i < poolManagers.Length; i++)
            {
                if (poolID == poolManagers[i].poolID)
                    poolID++;
            }
        } 

        //Create all of the inactive pool objects
        for (int i = 0; i < inactiveObjects.Count; i++)
        {
            inactiveObjects[i] = Instantiate(objectToPool,transform);
            inactiveObjects[i].SetActive(false);
        }

	}
	
    //Provides an object reference and moves the object to the location parameter
    public GameObject RequestObject(Vector3 location)
    {
        bool success = false;
        GameObject requestedObject;

        //Try to take an object from the pool
        try
        {
            requestedObject = inactiveObjects[0];
            success = true;
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Not enough objects in object pool!");
            throw;
        }
       
        //If the object is ok, we setup the object and swap it from the inactive list to the active list
        if (success)
        {
            inactiveObjects.Remove(requestedObject);
            activeObjects.Add(requestedObject);
            requestedObject.transform.SetParent(null);
            requestedObject.SetActive(true);
            requestedObject.transform.position = location;
            return requestedObject;
        }
        else
        {
            return null;
        }
    }

    //Makes the given object inactive and return in to the pool
    public void ReturnObject(GameObject returnedObject)
    {
        bool success = false;

        //If the object is in activeObjects flag success
        for (int i = 0; i < activeObjects.Count; i++)
        {
            if (returnedObject == activeObjects[i])
            {
                success = true;
                break;
            }
                
        }

        //If returning it is valid, we make it inactive and place it back in the pool as well as swapping the lists
        if (success)
        {
            returnedObject.SetActive(false);
            returnedObject.transform.position = transform.position;
            returnedObject.transform.SetParent(transform);
            activeObjects.Remove(returnedObject);
            inactiveObjects.Add(returnedObject);
        }

    }

    //Can be called to check if the given object is the same as the pool prefab
    //Used to check if the right pool is chosen
	public bool CheckForCorrectPool(GameObject objectToTest)
    {
        if (objectToTest != objectToPool)
            return false;
        else
            return true;
    }


}
