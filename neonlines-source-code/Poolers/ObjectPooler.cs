using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ScriptableObject implementation of a basic object pooling system. This 
/// object pool can grow if the user needs it to.
/// </summary>
[CreateAssetMenu]
public class ObjectPooler : ScriptableObject
{
    [Tooltip("Prefab object to be pooled on initialization.")]
    [SerializeField] private GameObject objectToPool;

    [Tooltip("Initial size of the object pool.")]
    [SerializeField] private int initialPoolSize;

    [Tooltip("If the pool fails to return an object, should it create " +
        "one and increase the pool's size accordingly?")]
    [SerializeField] private bool canGrow;

    /// <summary>
    /// Transform to store objects upon instantiation.
    /// </summary>
    private Transform objectPoolParent;

    /// <summary>
    /// Object pool list.
    /// </summary>
    private List<GameObject> objectPool;

    /// <summary>
    /// Returns the first inactive gameObject in the object pool. If the pool
    /// is allowed to grow, and this method fails to return an inactive
    /// gameObject, then a new object will be created, added to the pool, and 
    /// returned.
    /// </summary>
    /// <returns>{GameObject} First inactive object in pool, or newly added 
    /// gameObject if the pool is allowed to grow.</returns>
    public GameObject GetObject()
    {
        foreach (GameObject gameObject in objectPool)
        {
            if (!gameObject.activeInHierarchy)
            {
                return gameObject;
            }
        }

        if (canGrow)
        {
            InstantiatePoolObject(out GameObject gameObject);
            return gameObject;
        }

        return null;
    }

    /// <summary>
    /// Returns a random, inactive gameObject from the object pool. This 
    /// function does not currently allow for pool growing.
    /// </summary>
    /// <returns>{GameObject} Random inactive object form the object 
    /// pool. </returns>
    public GameObject GetRandomObject()
    {
        GameObject[] activeObjects = objectPool.Where(o => o.activeInHierarchy).ToArray();

        if(activeObjects.Length == 0)
        {
            return null;
        }

        if(activeObjects.Length == 1)
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

        for (int i = 0; i < initialPoolSize; i++)
        {
            InstantiatePoolObject();
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
    /// Deactivates all objects in the object pool. Useful for resets.
    /// </summary>
    public void DeactivateAll()
    {
        foreach(GameObject gameObject in objectPool)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Instatiates the game object and parents it to the objectPoolParent, if
    /// available.
    /// </summary>
    /// <param name="gameObject"></param>
    private void InstantiatePoolObject()
    {
        GameObject gameObject;

        if (objectPoolParent != null)
        {
            gameObject = Instantiate(objectToPool, objectPoolParent);
            gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            gameObject = Instantiate(objectToPool, Vector3.zero,
                Quaternion.identity);
        }

        gameObject.SetActive(false);
        objectPool.Add(gameObject);
    }

    /// <summary>
    /// Instatiates the game object and parents it to the objectPoolParent, if
    /// available. Outputs a GameObject reference.
    /// </summary>
    /// <param name="gameObject"></param>
    private void InstantiatePoolObject(out GameObject gameObject)
    {
        if (objectPoolParent != null)
        {
            gameObject = Instantiate(objectToPool, objectPoolParent);
            gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            gameObject = Instantiate(objectToPool, Vector3.zero,
                Quaternion.identity);
        }

        gameObject.SetActive(false);
        objectPool.Add(gameObject);
    }
}
