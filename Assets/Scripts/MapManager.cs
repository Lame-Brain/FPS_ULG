using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public TextAsset groundCSV, floor1CSV, floor2CSV, floor3CSV, blockingCSV, toonCSV, scriptCSV;
    public GameObject boulderPF, brokePillar1PF, brokePillar2PF, cavePF, cubePF, hillPF, MountainPF, pillarPF;

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
        //foreach (GameObject go in levelGOs) Destroy(go);

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
   }
     
}
