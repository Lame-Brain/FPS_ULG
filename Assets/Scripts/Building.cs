using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public static Building BLOCKS;

    public GameObject[] prefab;
    public Material[] mat;

    private void Awake()
    {
        BLOCKS = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject FindPrefab(string name)
    {
        GameObject r = null;
        foreach (GameObject go in prefab)
        {
            if (go.name == name) r = go;
        }
        return r;
    }

    public Material FindMat(string name)
    {
        if (name == "Grass")
        {
            int randoNumber = Random.Range(1, 11);
            name = name + " " + randoNumber;
        }
        Material r = null;
        foreach (Material m in mat)
        {
            if (m.name == name) r = m; 
        }
        return r;
    }
}
