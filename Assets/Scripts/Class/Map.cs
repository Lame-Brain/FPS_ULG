using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour
{
    public TextAsset groundCSV, floor1CSV, floor2CSV, floor3CSV, floor4CSV, floor5CSV, blockingCSV, toonCSV, scriptCSV;
    public int mapWidth, mapLength;
    public int[,] groundMap, f1Map, f2Map, f3Map, f4Map, f5Map, blockMap, toonIDMap, scriptMap;
    public GameObject[,] toonMap;

    public Map(Map map)
    {
        this.mapWidth = map.mapWidth; this.mapLength = map.mapLength;
        this.groundMap = map.groundMap; this.f1Map = map.f1Map; this.f2Map = map.f2Map; this.f3Map = map.f3Map; this.f4Map = map.f4Map; this.f5Map = map.f5Map; this.blockMap = map.blockMap; this.scriptMap = map.scriptMap;
        this.toonMap = map.toonMap;
    }
}
