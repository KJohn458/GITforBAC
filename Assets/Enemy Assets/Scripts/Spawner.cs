using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner instance;

    public GameObject[] Prefabs;
    List<GameObject>[] pools;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        pools = new List<GameObject>[Prefabs.Length];
        
        for (int i = 0; i < Prefabs.Length; i++)
        {
            pools[i]= new List<GameObject>();  
        }
    }
  
    public GameObject Spawn (string name)
    {
        for (int i = 0;i<Prefabs.Length;i++)
        {
            GameObject prefab = Prefabs[i];
            if (prefab.name == name)
            {                
                List <GameObject> pool = pools[i];
                GameObject g = pool.Find(IsInactive);
                if (g == null)
                {
                    g = Instantiate(prefab);
                    pool.Add(g);
                }
                g.SetActive(true);
                return g;
            }
        }
        return null;
    }

    bool IsInactive(GameObject g)
    {
        return !g.activeSelf;
    }

    public void DisableAll()
    {
        for (int j = 0; j < Prefabs.Length; j++)
        {
            for (int i = 0; i < pools[i].Count; i++)
            {
                pools[i][j].SetActive(false);
            }
        }
    }
}
