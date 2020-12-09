using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public TextAsset GroundCSV, Deco1CSV, Deco2CSV, CeilingCSV, BlockingCSV, InteractCSV;
    public GameObject[,,] mapTileInstance;
    public int[,,] mapTileValue;

    private bool madeObject;

    // Start is called before the first frame update
    void Start()
    {
        //initialize the arrays
        string[] line = GroundCSV.text.Split('\n'), cell = line[0].Split(',');
        int x = cell.Length, y = line.Length, z = 0;
        mapTileInstance = new GameObject[x, y, 6];
        mapTileValue = new int[x, y, 6];

        //Destroy Previous level
        GameObject[] OLDLEVEL = GameObject.FindGameObjectsWithTag("BuildingBlock");
        foreach (GameObject go in OLDLEVEL) Destroy(go);

        //Parse the CSV files
        for (z = 0; z < 6; z++)
        {
            if (z == 0) line = GroundCSV.text.Split('\n');
            if (z == 1) line = Deco1CSV.text.Split('\n');
            if (z == 2) line = Deco2CSV.text.Split('\n');
            if (z == 3) line = CeilingCSV.text.Split('\n');
            if (z == 4) line = BlockingCSV.text.Split('\n');
            if (z == 5) line = InteractCSV.text.Split('\n');
            for (y = 0; y < line.Length; y++) //Ground Layer
            {
                cell = line[y].Split(',');
                for (x = 0; x < cell.Length; x++)
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
            for(y = 0; y < mapTileValue.GetLength(1)-1; y++)
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
        madeObject = false;

        if (mapTileValue[x, y, z] < 14)
        {
            madeObject = true;
            go = Building.BLOCKS.FindPrefab("Floor");
            mapTileInstance[x, y, z] = Instantiate(go, new Vector3(x, -0.51f, -y), Quaternion.Euler(Vector3.right * -90f));
            mapTileInstance[x, y, z].name = "FloorTile _" + x + "_" + y + "_" + z;
            if (mapTileValue[x, y, z] == 0) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Black");
            if (mapTileValue[x, y, z] == 1) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Grass");
            if (mapTileValue[x, y, z] == 2) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("LStone_Side");
            if (mapTileValue[x, y, z] == 3) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("DStone_Side");
            if (mapTileValue[x, y, z] == 4) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("shallowWater");
            if (mapTileValue[x, y, z] == 5) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("DeepWater");
            if (mapTileValue[x, y, z] == 6) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("SwampWater");
            if (mapTileValue[x, y, z] == 7) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Sand");
            if (mapTileValue[x, y, z] == 8) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Checkerboard");
            if (mapTileValue[x, y, z] == 9) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Brick");
            if (mapTileValue[x, y, z] == 10) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("GreenBrick");
            if (mapTileValue[x, y, z] == 11) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("BlueBrick");
            if (mapTileValue[x, y, z] == 12) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("White");
            if (mapTileValue[x, y, z] == 13) mapTileInstance[x, y, z].GetComponent<MeshRenderer>().material = Building.BLOCKS.FindMat("Ice");
        }
        if (mapTileValue[x, y, z] > 13 && mapTileValue[x, y, z] < 23)
        {
            madeObject = true;
            go = Building.BLOCKS.FindPrefab("Low_Wall");
            mapTileInstance[x, y, z] = Instantiate(go, new Vector3(x, -0.5f, -y), Quaternion.identity);
            mapTileInstance[x, y, z].name = "Half_WallTile _" + x + "_" + y + "_" + z;
            Material[] matsArray = mapTileInstance[x, y, z].GetComponent<MeshRenderer>().materials;
            if (mapTileValue[x, y, z] == 14) { matsArray[0] = Building.BLOCKS.FindMat("Wood_Side"); matsArray[1] = Building.BLOCKS.FindMat("Wood_Top"); }
            if (mapTileValue[x, y, z] == 15) matsArray[0] = Building.BLOCKS.FindMat("Brick");
            if (mapTileValue[x, y, z] == 16) matsArray[0] = Building.BLOCKS.FindMat("GreenBrick");
            if (mapTileValue[x, y, z] == 17) matsArray[0] = Building.BLOCKS.FindMat("BlueBrick");
            if (mapTileValue[x, y, z] == 18) { matsArray[0] = Building.BLOCKS.FindMat("LStone_Side"); matsArray[1] = Building.BLOCKS.FindMat("LStone_Top"); }
            if (mapTileValue[x, y, z] == 19) { matsArray[0] = Building.BLOCKS.FindMat("Dstone_Side"); matsArray[1] = Building.BLOCKS.FindMat("Dstone_Top"); }
            if (mapTileValue[x, y, z] == 20) matsArray[0] = Building.BLOCKS.FindMat("Ice");
            if (mapTileValue[x, y, z] == 21) matsArray[0] = Building.BLOCKS.FindMat("Stone");
            if (mapTileValue[x, y, z] == 22) matsArray[0] = Building.BLOCKS.FindMat("SnowBrick");
            mapTileInstance[x, y, z].GetComponent<MeshRenderer>().materials = matsArray; 
        }
        if (madeObject) mapTileInstance[x, y, z].tag = "BuildingBlock";
    }
}
