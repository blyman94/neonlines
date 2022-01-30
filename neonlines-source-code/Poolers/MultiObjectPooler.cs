using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ScriptableObject implementation of a basic object pooling system. This 
/// object pool can grow if the user needs it to.
/// </summary>
[CreateAssetMenu]
public class MultiObjectPooler : ScriptableObject
{
    [Tooltip("Prefab objects to be pooled on initialization.")]
    [SerializeField] private GameObject[] objectsToPool;

    [Tooltip("Number of objects to be pooled per object in objectsToPool.")]
    [SerializeField] private int initialPoolSize;

    /// <summary>
    /// Transform to store objects upon instantiation.
    /// </summary>
    private Transform objectPoolParent;

    /// <summary>
    /// Object pool list.
    /// </summary>
    private List<GameObject> objectPool;

    /// <summary>
    /// Deactivates all objects in the pool. Useful for resets.
    /// </summary>
    public void DeactivateAll()
    {
        foreach(GameObject gameObject in objectPool)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Returns a random, inactive gameObject from the object pool. This 
    /// function does not currently allow for pool growing.
    /// </summary>
    /// <returns>{GameObject} Random inactive object form the object 
    /// pool. </returns>
    public GameObject GetRandomObject()
    {
        GameObject[] activeObjects = objectPool.Where(o => !o.activeInHierarchy).ToArray();

        if (activeObjects.Length == 0)
        {
            return null;
        }

        if (activeObjects.Length == 1)
        {
            return activeObjects[0];
        }

        int randomObjectInt = Random.Range(0, activeObjects.Length);
        return activeObjects[randomObjectInt];
    }

    /// <summary>
    /// Creates an object pool of size initialPoolSize
    /// </summary>
    public void InitializePool()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < objectsToPool.Length; i++)
        {
            for (int j = 0; j < initialPoolSize; j++)
            {
                InstantiatePoolObject(i);
            }
        }
        
    }

    /// <summary>
    /// Overload for InitializeObjectPool that parents each instatiated
    /// gameObject to a passed parent transform.
    /// </summary>
    /// <param name="objectPoolParent">Transform to parent objects to.</param>
    public void InitializePool(Transform objectPoolParent)
    {
        this.objectPoolParent = objectPoolParent;
        InitializePool();
    }

    /// <summary>
    /// Instatiates the game object and parents it to the objectPoolParent, if
    /// available.
    /// </summary>
    /// <param name="gameObject"></param>
    private void InstantiatePoolObject(int objectToPoolIndex)
    {
        GameObject gameObject;

        if (objectPoolParent != null)
        {
            gameObject = Instantiate(objectsToPool[objectToPoolIndex], 
                objectPoolParent);
            gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            gameObject = Instantiate(objectsToPool[objectToPoolIndex], 
                Vector3.zero, Quaternion.identity);
        }

        gameObject.SetActive(false);
        objectPool.Add(gameObject);
    }
}
