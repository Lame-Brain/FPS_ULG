using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonPool : MonoBehaviour
{
    public static ToonPool INSTANCE;

    public GameObject[] toon;

    void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(INSTANCE);
        }
        else Destroy(this);
    }

    public GameObject GetToon(string s)
    {
        GameObject target = null;
        foreach (GameObject go in toon) if (go.name == s) target = go;
        return target;
    }

    public bool ToonExists(string s)
    {
        bool result = false;
        foreach (GameObject go in toon) if (go.name == s) result = true;
        return result;
    }
}
