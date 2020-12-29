using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public TextAsset groundCSV, floor1CSV, floor2CSV, floor3CSV, blockingCSV, toonCSV, scriptCSV;

    private int mapWidth = 32, mapLength = 32;
    private int[,] groundMap, f1Map, f2Map, f3Map, blockMap, toonMap, scriptMap;

    // Start is called before the first frame update
    void Start()
    {
        groundMap = new int[mapWidth, mapLength];
        f1Map = new int[mapWidth, mapLength];
        f2Map = new int[mapWidth, mapLength];
        f3Map = new int[mapWidth, mapLength];
        blockMap = new int[mapWidth, mapLength];
        toonMap = new int[mapWidth, mapLength];
        scriptMap = new int[mapWidth, mapLength];

        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        //start by deleting existing level geometry
        GameObject[] levelGOs = GameObject.FindGameObjectsWithTag("level");
        foreach (GameObject go in levelGOs) Destroy(go);

        //load CSVs
        string[] row1 = groundCSV.text.Split(new char[] { '\n' });
        string[] row2 = floor1CSV.text.Split(new char[] { '\n' });
        string[] row3 = floor2CSV.text.Split(new char[] { '\n' });
        string[] row4 = floor3CSV.text.Split(new char[] { '\n' });
        string[] row5 = blockingCSV.text.Split(new char[] { '\n' });
        string[] row6 = toonCSV.text.Split(new char[] { '\n' });
        string[] row7 = scriptCSV.text.Split(new char[] { '\n' });
        for (int y = 0; y < mapLength; y++)
        {
            string[] column1 = row1[y].Split(new char[] { ',' });
            string[] column2 = row2[y].Split(new char[] { ',' });
            string[] column3 = row3[y].Split(new char[] { ',' });
            string[] column4 = row4[y].Split(new char[] { ',' });
            string[] column5 = row5[y].Split(new char[] { ',' });
            string[] column6 = row6[y].Split(new char[] { ',' });
            string[] column7 = row7[y].Split(new char[] { ',' });
            for (int x = 0; x < mapWidth; x++)
            {
                int.TryParse(column1[x], out groundMap[x, y]);
                int.TryParse(column2[x], out f1Map[x, y]);
                int.TryParse(column3[x], out f2Map[x, y]);
                int.TryParse(column4[x], out f3Map[x, y]);
                int.TryParse(column5[x], out blockMap[x, y]);
                int.TryParse(column6[x], out toonMap[x, y]);
                int.TryParse(column7[x], out scriptMap[x, y]);
            }
        }

        //instantiate level geometry
        GameObject ground;
        for(int y = 0; y < mapLength; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                ground = null;
                if ((groundMap[x,y] > 50 && groundMap[x,y] < 54) || groundMap[x,y] == 93) //Ground is liquid
                {
                    ground = Instantiate(MapGeometry.MAP.GetModel("LiquidCube"), new Vector3(x, -1, -y), MapGeometry.MAP.GetModel("LiquidCube").transform.rotation);
                    if (groundMap[x, y] == 51) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Deep_Water"); //Deep Water
                    if (groundMap[x, y] == 52) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Medium_Water"); //Medium Water
                    if (groundMap[x, y] == 53) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Shallow_Water"); //Shallow Water
                    if (groundMap[x, y] == 93) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Lava"); //Lava
                }
                if(groundMap[x,y] > 86 && groundMap[x,y] < 91) //Ground is a field
                {
                    ground = Instantiate(MapGeometry.MAP.GetModel("EnergyCube"), new Vector3(x, -1, -y), MapGeometry.MAP.GetModel("EnergyCube").transform.rotation);
                    if (groundMap[x, y] == 87) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Poison_Field"); //Poison Field
                    if (groundMap[x, y] == 88) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Energy_Field"); //Arcane Field
                    if (groundMap[x, y] == 89) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Fire_Field"); //Fire Field
                    if (groundMap[x, y] == 90) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Sleep_Field"); //Sleep Field
                }
                if (groundMap[x, y] == 59) //Ground is a Mountain
                {
                    ground = Instantiate(MapGeometry.MAP.GetModel("Mountain"), new Vector3(x, -1, -y), MapGeometry.MAP.GetModel("Mountain").transform.rotation);
                }
                if (groundMap[x, y] > 59 && groundMap[x, y] < 64) //Ground is a Mountain Cave
                {
                    ground = Instantiate(MapGeometry.MAP.GetModel("Cave"), new Vector3(x, -1, -y), MapGeometry.MAP.GetModel("Cave").transform.rotation);
                    if (groundMap[x, y] == 60) ground.transform.rotation = Quaternion.Euler(-90f, 90f, 0);
                    if (groundMap[x, y] == 61) ground.transform.rotation = Quaternion.Euler(-90f, 180f, 0);
                    if (groundMap[x, y] == 62) ground.transform.rotation = Quaternion.Euler(-90f, 270f, 0);
                    if (groundMap[x, y] == 63) ground.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                }

                if(groundMap[x,y] == 58) //Hill
                {
                    ground = Instantiate(MapGeometry.MAP.GetModel("Hill"), new Vector3(x, -0.915f, -y), MapGeometry.MAP.GetModel("Hill").transform.rotation);
                    ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Hill");
                }

                if (ground == null && groundMap[x,y] > 0) //Solid Ground
                {
                    ground = Instantiate(MapGeometry.MAP.GetModel("Cube"), new Vector3(x, -1, -y), MapGeometry.MAP.GetModel("Cube").transform.rotation);
                    if (groundMap[x, y] == 54) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Swamp"); //Swamp
                    if (groundMap[x, y] == 70) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Tile_Floor"); //Tile
                    if (groundMap[x, y] == 80) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Stone_Wall"); //Stone Wall
                    if (groundMap[x, y] == 81) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Locked_Door"); //Locked Door
                    if (groundMap[x, y] == 82) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Door"); //Door
                    if (groundMap[x, y] == 84) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Brick_Floor"); //Brick Floor
                    if (groundMap[x, y] == 85) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Planks"); //Planks
                    if (groundMap[x, y] == 90) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("White"); //White Block
                    if (groundMap[x, y] == 92) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Secret_Passage"); //Secret Passage
                    if (groundMap[x, y] == 94) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Brick_Wall"); //Brick Wall
                    if (groundMap[x, y] == 95) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Black"); //Black
                    if (groundMap[x, y] == 98) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Dirt"); //Dirt
                    if (groundMap[x, y] == 99) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Grass"); //Grass
                    if (groundMap[x, y] == 100) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Sand"); //Sand
                    if (groundMap[x, y] == 101) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Snow"); //Snow
                    if (groundMap[x, y] == 102) ground.GetComponent<MeshRenderer>().material = MapGeometry.MAP.GetMat("Stone"); //Stone
                }
            }
        }
   }
     
}
