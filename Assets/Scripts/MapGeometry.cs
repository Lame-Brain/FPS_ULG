using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeometry : MonoBehaviour
{
    public GameObject[] model;
    public Material[] texture;
    public GameObject[] deco;

    public GameObject GetModel(string s)
    {
        GameObject target = null;
        foreach (GameObject go in model) if (go.name == s) target = go;
        return target;
    }

    public Material GetMat(string s)
    {
        Material target = null;
        foreach (Material mat in texture) if (mat.name == s) target = mat;
        return target;
    }

    public GameObject GetDeco(string s)
    {
        GameObject target = null;
        foreach (GameObject go in deco) if (go.name == s) target = go;
        return target;
    }
}
