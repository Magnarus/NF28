using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour {


    static List<GameObject> list = new List<GameObject>(); // Liste de gameObject instantiés.

    void Awake()
    {

    }

    public static void PoolManagerData(int size, GameObject prefab)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab) as GameObject;
            list.Add(obj);
        }
    }

    public static GameObject GetObject()
    {
        if (list.Count > 0)
        {
            GameObject obj = list[0];
            list.RemoveAt(0);
            return obj;
        }
        return null;
    }

    public static void DestroyObjectPool(GameObject obj)
    {
        list.Add(obj);
        obj.SetActive(false);
    }


    public static void ClearPool()
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            GameObject obj = list[i];
            list.RemoveAt(i);
            Destroy(obj);
        }
        list = null;
    }

}
