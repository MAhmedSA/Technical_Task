using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{

    [Header("Spawn Positions")]
    [Tooltip("Array of random positions")]
    [SerializeField]
    Transform[] spwanPositions;

    [Header("Pooling Variables")]
    [SerializeField] private int initPoolSize;
    [SerializeField] private PooledObject[] objectToPool;
    // Store the pooled objects in a collection
    private Stack<PooledObject> stack;

    public int InitPoolSize { get => initPoolSize; set => initPoolSize = value; }

    public Stack<PooledObject> Stack { get => stack;}

    private void Start()
    {
        SetupPool();
    }
    // Creates the pool (invoke when the lag is not noticeable)
    private void SetupPool()
    {
        stack = new Stack<PooledObject>();
        PooledObject instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool[Random.RandomRange(0, spwanPositions.Length)]);
            instance.Pool = this;
            instance.transform.position = spwanPositions[Random.RandomRange(0, spwanPositions.Length)].position;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }
    // returns the first active GameObject from the pool
    public PooledObject GetPooledObject()
    {
        // if the pool is not large enough, instantiate a new PooledObjects
        if (stack.Count == 0)
        {
            PooledObject newInstance = Instantiate(objectToPool[Random.RandomRange(0, 3)]);
            newInstance.Pool = this;
            newInstance.transform.position = spwanPositions[Random.RandomRange(0, spwanPositions.Length)].position;
            return newInstance;
        }
        // otherwise, just grab the next one from the list
        PooledObject nextInstance = stack.Pop();

        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }
    public void ReturnToPool(PooledObject pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
        pooledObject.gameObject.transform.position = spwanPositions[Random.RandomRange(0, spwanPositions.Length)].position;
    } 

}
