using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour {

    public List<GameObject> objectList;
    public GameObject objectToRecycle;
    public int totalObjectAtStart;

    static private Dictionary<int, ObjectPool> pools = new Dictionary<int, ObjectPool>();

    private void Init()
    {
        objectList = new List<GameObject>(totalObjectAtStart);
        for(int i = 0; i<totalObjectAtStart; i++)
        {
            GameObject newObject = Instantiate(objectToRecycle);
            newObject.transform.parent = transform;
            newObject.SetActive(false);
            objectList.Add(newObject);
        }
        pools.Add(objectToRecycle.GetInstanceID(), this);
    }

    private GameObject PoolObject (Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
    {
        var freeObject = (from item in objectList
                          where item.activeSelf == false
                          select item).FirstOrDefault();

        if(freeObject == null)
        {
            freeObject = (GameObject)Instantiate(objectToRecycle, position, rotation);
            freeObject.transform.parent = transform;
            objectList.Add(freeObject);
        }
        else
        {
            freeObject.transform.position = position;
            freeObject.transform.rotation = rotation;
            freeObject.SetActive(true);
        }
        return freeObject;
    }

    static public GameObject GetInstance(GameObject original, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), int poolSize = 200)
    {
        int id = original.GetInstanceID();
        if (pools.ContainsKey(id))
        {
            return pools[id].PoolObject(position, rotation);
        }
        else
        {
            GameObject go = new GameObject("ObjectPool: " + original.name);
            ObjectPool newPool = go.AddComponent<ObjectPool>();
            newPool.objectToRecycle = original;
            newPool.totalObjectAtStart = poolSize;
            newPool.Init();
            return newPool.PoolObject(position, rotation);
        }

        //return (GameObject) Instantiate(original, position, rotation);
        //ObjectPool pool = FindObjectOfType<ObjectPool>();
        //return pool.PoolObject(position, rotation);
    }

    static public void Release (GameObject obj)
    {
        obj.SetActive(false);
        //Destroy(obj);
        //obj.SetActive(false);
    }

}
