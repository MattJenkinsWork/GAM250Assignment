using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    public GameObject objectToPool;

    public List<GameObject> inactiveObjects;
    public List<GameObject> activeObjects;

    public int amountToPool = 1000;

    public int poolID;

	// Use this for initialization
	void Start ()
    {
        inactiveObjects = new List<GameObject>(new GameObject[amountToPool]);
        activeObjects = new List<GameObject>(new GameObject[amountToPool]);

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

        for (int i = 0; i < inactiveObjects.Count; i++)
        {
            inactiveObjects[i] = Instantiate(objectToPool,transform);
            inactiveObjects[i].SetActive(false);
        }

	}
	
    public GameObject RequestObject(Vector3 location)
    {
        bool success = false;
        GameObject requestedObject;

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

    public void ReturnObject(GameObject returnedObject)
    {
        bool success = false;


        for (int i = 0; i < activeObjects.Count; i++)
        {
            if (returnedObject == activeObjects[i])
            {
                success = true;
                break;
            }
                
        }

        if (success)
        {
            returnedObject.SetActive(false);
            returnedObject.transform.position = transform.position;
            returnedObject.transform.SetParent(transform);
            activeObjects.Remove(returnedObject);
            inactiveObjects.Add(returnedObject);
        }

    }

	public bool CheckForCorrectPool(GameObject objectToTest)
    {
        if (objectToTest != objectToPool)
            return false;
        else
            return true;
    }


}
