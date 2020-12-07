using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public TextAsset GroundCSV, BlockingDecoCSV, NonBlockDecoCSV, InteractiveCSV;
    public GameObject[,,] mapTileInstance;
    public int[,,] mapTileValue;


    // Start is called before the first frame update
    void Start()
    {
        //initialize the arrays
        string[] line = GroundCSV.text.Split('\n'), cell = line[0].Split(',');
        int x = cell.Length, y = line.Length, z = 0;
        mapTileInstance = new GameObject[x, y, 4];
        mapTileValue = new int[x, y, 4];

        //Destroy Previous level
        GameObject[] OLDLEVEL = GameObject.FindGameObjectsWithTag("BuildingBlock");
        foreach (GameObject go in OLDLEVEL) Destroy(go);

        //Parse the CSV files
        for (z = 0; z < 4; z++)
        {
            if (z == 0) line = GroundCSV.text.Split('\n');
            if (z == 1) line = BlockingDecoCSV.text.Split('\n');
            if (z == 2) line = NonBlockDecoCSV.text.Split('\n');
            if (z == 3) line = InteractiveCSV.text.Split('\n');
            for (y = 0; y < line.Length; y++) //Ground Layer
            {
                cell = line[y].Split(',');
                for (x = 0; x < cell.Length-1; x++)
                {
                    int bleh;
                    bool success = int.TryParse(cell[x], out bleh);
                    if (!success) { Debug.Log("COULD NOT CONVERT '" + cell[x] + "' TO AN INTEGER SO NOW IT IS 0. SUCK IT."); bleh = 0; }
                    mapTileValue[x, y, z] = bleh;
                }
            }
        }
        //Build Level
        z = 0;//for (z = 0; z < mapTileValue.GetLength(2); z++)
        {            
            for(y = 0; y < mapTileValue.GetLength(1); y++)
            {
                for (x = 0; x < mapTileValue.GetLength(0); x++)
                {
                    GetTileLogic(x, y, z);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTileLogic(int x, int y, int z)
    {
        GameObject go = null;

        if (mapTileValue[x, y, z] < 5 || mapTileValue[x, y, z] == 22 || mapTileValue[x, y, z] == 23 || mapTileValue[x, y, z] == 24 || mapTileValue[x, y, z] == 49 || mapTileValue[x, y, z] == 50 || mapTileValue[x, y, z] == 51)
        {
            go = Building.BLOCKS.FindPrefab("Floor");
            mapTileInstance[x, y, z] = Instantiate(go, new Vector3(x, 0, y), Quaternion.Euler(Vector3.right * -90f));
            if (mapTileValue[x, y, z] == 0) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Black");
            if (mapTileValue[x, y, z] == 1) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Grass");
            if (mapTileValue[x, y, z] == 2) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Brown");
            if (mapTileValue[x, y, z] == 3) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("LStone_Side");
            if (mapTileValue[x, y, z] == 4) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("DStone_Side");
            if (mapTileValue[x, y, z] == 22) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("shallowWater");
            if (mapTileValue[x, y, z] == 23) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("DeepWater");
            if (mapTileValue[x, y, z] == 24) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("SwampWater");
            if (mapTileValue[x, y, z] == 49) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Sand");
            if (mapTileValue[x, y, z] == 50) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Checkerboard");
            if (mapTileValue[x, y, z] == 51) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Brick");
        }
        if(mapTileValue[x,y,z] == 17)
        {
            go = Building.BLOCKS.FindPrefab("Floor");
            mapTileInstance[x, y, z] = Instantiate(go, new Vector3(x, 0, y), Quaternion.Euler(Vector3.right * -90f));
            mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Brick");
        }
    }
}
