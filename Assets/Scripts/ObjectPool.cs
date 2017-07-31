using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour {

    private List<GameObject> _objectList;
    private GameObject _objectToRecycle;
    private int _totalObjectAtStart;

    static private Dictionary<int, ObjectPool> pools = new Dictionary<int, ObjectPool>();

    private void Init()
    {
        _objectList = new List<GameObject>(_totalObjectAtStart);
        for(int i = 0; i<_totalObjectAtStart; i++)
        {
            GameObject newObject = Instantiate(_objectToRecycle);
            newObject.transform.parent = transform;
            newObject.SetActive(false);
            _objectList.Add(newObject);
        }
        pools.Add(_objectToRecycle.GetInstanceID(), this);
    }

    private GameObject PoolObject (Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
    {
        var freeObject = (from item in _objectList
                          where item.activeSelf == false
                          select item).FirstOrDefault();

        if(freeObject == null)
        {
            freeObject = (GameObject)Instantiate(_objectToRecycle, position, rotation);
            freeObject.transform.parent = transform;
            _objectList.Add(freeObject);
        }
        else
        {
            freeObject.transform.position = position;
            freeObject.transform.rotation = rotation;
            freeObject.SetActive(true);
        }
        return freeObject;
    }

    static public bool IsPoolReady (GameObject original)
    {
        return pools.ContainsKey(original.GetInstanceID());
    }

    static public void InitPool (GameObject original, int poolSize = 200)
    {
        if (!pools.ContainsKey(original.GetInstanceID()))
        {
            GameObject go = new GameObject("ObjectPool: " + original.name);
            ObjectPool newPool = go.AddComponent<ObjectPool>();
            newPool._objectToRecycle = original;
            newPool._totalObjectAtStart = poolSize;
            newPool.Init();
        }
    }

    static public GameObject GetInstance(int instanceID, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), int poolSize = 200)
    {
        return pools[instanceID].PoolObject(position, rotation);
    }

        static public GameObject GetInstance(GameObject original, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), int poolSize = 200)
    {
        int id = original.GetInstanceID();
        InitPool(original, poolSize);
        return pools[id].PoolObject(position, rotation);
    }

    static public void Release (GameObject obj)
    {
        if(obj.GetComponentInParent<ObjectPool>() == null)
        {
            foreach (ObjectPool p in pools.Values)
            {
                if (p._objectList.Contains(obj))
                {
                    obj.transform.parent = p.transform;
                    break;
                }
            }
        }
        obj.SetActive(false);
    }

}
