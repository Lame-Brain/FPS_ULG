using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GAME;
    public static ToonPool TOON;
    public static MapGeometry PART;
    public static MapManager MAP;

    public GameObject[] mapList;

    void Awake()
    {
        if (GAME == null)
        {
            GAME = this;
            DontDestroyOnLoad(GAME);
        }
        else Destroy(this);

        TOON = GameObject.FindGameObjectWithTag("ToonManager").GetComponent<ToonPool>();
        PART = GameObject.FindGameObjectWithTag("GeometryManager").GetComponent<MapGeometry>();
        MAP = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
    }

    private void Start()
    {
        mapList[0].GetComponent<MapManager>().InitMap();
        mapList[0].GetComponent<MapManager>().BuildLevel();
    }

}
