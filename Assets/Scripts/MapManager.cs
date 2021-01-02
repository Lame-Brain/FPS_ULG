using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public void InitMap()
    {
        groundMap = new int[mapWidth, mapLength];
        f1Map = new int[mapWidth, mapLength];
        f2Map = new int[mapWidth, mapLength];
        f3Map = new int[mapWidth, mapLength];
        f4Map = new int[mapWidth, mapLength];
        f5Map = new int[mapWidth, mapLength];
        blockMap = new int[mapWidth, mapLength];
        toonIDMap = new int[mapWidth, mapLength];
        scriptMap = new int[mapWidth, mapLength];
        toonMap = new GameObject[mapWidth, mapLength];

        LoadLevelFromCSVs();
    }

    public void LoadLevelFromCSVs()
    {
        string[] row1 = groundCSV.text.Split(new char[] { '\n' });
        string[] row2 = floor1CSV.text.Split(new char[] { '\n' });
        string[] row3 = floor2CSV.text.Split(new char[] { '\n' });
        string[] row4 = floor3CSV.text.Split(new char[] { '\n' });
        string[] row5 = floor4CSV.text.Split(new char[] { '\n' });
        string[] row6 = floor5CSV.text.Split(new char[] { '\n' });
        string[] row7 = blockingCSV.text.Split(new char[] { '\n' });
        string[] row8 = toonCSV.text.Split(new char[] { '\n' });
        string[] row9 = scriptCSV.text.Split(new char[] { '\n' });
        for (int y = 0; y < mapLength; y++)
        {
            string[] column1 = row1[y].Split(new char[] { ',' });
            string[] column2 = row2[y].Split(new char[] { ',' });
            string[] column3 = row3[y].Split(new char[] { ',' });
            string[] column4 = row4[y].Split(new char[] { ',' });
            string[] column5 = row5[y].Split(new char[] { ',' });
            string[] column6 = row6[y].Split(new char[] { ',' });
            string[] column7 = row7[y].Split(new char[] { ',' });
            string[] column8 = row8[y].Split(new char[] { ',' });
            string[] column9 = row9[y].Split(new char[] { ',' });
            for (int x = 0; x < mapWidth; x++)
            {
                int.TryParse(column1[x], out groundMap[x, y]);
                int.TryParse(column2[x], out f1Map[x, y]);
                int.TryParse(column3[x], out f2Map[x, y]);
                int.TryParse(column4[x], out f3Map[x, y]);
                int.TryParse(column5[x], out f4Map[x, y]);
                int.TryParse(column6[x], out f5Map[x, y]);
                int.TryParse(column7[x], out blockMap[x, y]);
                int.TryParse(column8[x], out toonIDMap[x, y]);
                int.TryParse(column9[x], out scriptMap[x, y]);
            }
        }
    }

    public void BuildLevel()
    {

        //start by deleting existing level geometry
        GameObject[] levelGOs = GameObject.FindGameObjectsWithTag("level");
        foreach (GameObject go in levelGOs) Destroy(go);

        //instantiate level geometry
        for (int y = 0; y < mapLength; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                Tile(groundMap[x, y], x, y, -1);
                Tile(f1Map[x, y], x, y, 0);
                Tile(f2Map[x, y], x, y, 1);
                Tile(f3Map[x, y], x, y, 2);
                Tile(f4Map[x, y], x, y, 1);
                Tile(f5Map[x, y], x, y, 2);
            }
        }

        //add toons
        for (int y = 0; y < mapLength; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                Toon(toonIDMap[x, y], x, y, 0);
            }
        }
    }


    private void Tile(int input, int x, int y, int z)
    {
        GameObject ground = null;
        if ((input > 50 && input < 54) || input == 93) //Ground is liquid
        {
            ground = Instantiate(GameManager.PART.GetModel("LiquidCube"), new Vector3(x, z, -y), GameManager.PART.GetModel("LiquidCube").transform.rotation);
            if (input == 51) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Deep_Water"); //Deep Water
            if (input == 52) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Medium_Water"); //Medium Water
            if (input == 53) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Shallow_Water"); //Shallow Water
            if (input == 93) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Lava"); //Lava
        }
        if (input > 86 && input < 91) //Ground is a field
        {
            ground = Instantiate(GameManager.PART.GetModel("EnergyCube"), new Vector3(x, z, -y), GameManager.PART.GetModel("EnergyCube").transform.rotation);
            if (input == 87) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Poison_Field"); //Poison Field
            if (input == 88) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Energy_Field"); //Arcane Field
            if (input == 89) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Fire_Field"); //Fire Field
            if (input == 90) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Sleep_Field"); //Sleep Field
        }
        if (input == 59) //Ground is a Mountain
        {
            ground = Instantiate(GameManager.PART.GetModel("Mountain"), new Vector3(x, z, -y), GameManager.PART.GetModel("Mountain").transform.rotation);
        }
        if (input > 59 && input < 64) //Ground is a Mountain Cave
        {
            ground = Instantiate(GameManager.PART.GetModel("Cave"), new Vector3(x, z, -y), GameManager.PART.GetModel("Cave").transform.rotation);
            if (input == 60) ground.transform.rotation = Quaternion.Euler(-90f, 90f, 0);
            if (input == 61) ground.transform.rotation = Quaternion.Euler(-90f, 180f, 0);
            if (input == 62) ground.transform.rotation = Quaternion.Euler(-90f, 270f, 0);
            if (input == 63) ground.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        }

        if (input == 58) //Hill
        {
            ground = Instantiate(GameManager.PART.GetModel("Hill"), new Vector3(x, z+0.085f, -y), GameManager.PART.GetModel("Hill").transform.rotation);
            ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Hill");
        }

        if (input == 103) //Pillar
        {
            ground = Instantiate(GameManager.PART.GetModel("Pillar"), new Vector3(x, z, -y), GameManager.PART.GetModel("Pillar").transform.rotation);
        }
        if (input == 104) //Broken Pillar 1
        {
            ground = Instantiate(GameManager.PART.GetModel("Broken_Pillar1"), new Vector3(x, z, -y), GameManager.PART.GetModel("Broken_Pillar1").transform.rotation);
        }
        if (input == 105) //Broken Pillar 2
        {
            ground = Instantiate(GameManager.PART.GetModel("Broken_Pillar2"), new Vector3(x, z, -y), GameManager.PART.GetModel("Broken_Pillar2").transform.rotation);
        }
        if(input == 8) //Small Bridge
        {
            ground = Instantiate(GameManager.PART.GetModel("Small_Bridge"), new Vector3(x, z - .45f, -y), GameManager.PART.GetModel("Small_Bridge").transform.rotation);
        }
        if (input == 9) //North Bridge
        {
            ground = Instantiate(GameManager.PART.GetModel("NBridge"), new Vector3(x, z - .45f, -y), GameManager.PART.GetModel("NBridge").transform.rotation);
        }
        if (input == 10) //South Bridge
        {
            ground = Instantiate(GameManager.PART.GetModel("SBridge"), new Vector3(x, z - .45f, -y), GameManager.PART.GetModel("SBridge").transform.rotation);
        }
        if (input == 0) //scrub
        {
            ground = Instantiate(GameManager.PART.GetDeco("Scrub"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 1) //Tree
        {
            ground = Instantiate(GameManager.PART.GetDeco("Tree"), new Vector3(x, z+.5f, -y), Quaternion.identity);
        }
        if (input == 2) //Town
        {
            ground = Instantiate(GameManager.PART.GetDeco("Town"), new Vector3(x, z + .5f, -y), Quaternion.identity);
        }
        if (input == 3) //Keep
        {
            ground = Instantiate(GameManager.PART.GetDeco("Keep"), new Vector3(x, z + .5f, -y), Quaternion.identity);
        }
        if (input == 4) //village
        {
            ground = Instantiate(GameManager.PART.GetDeco("Village"), new Vector3(x, z + .5f, -y), Quaternion.identity);
        }
        if (input == 6) //castle
        {
            ground = Instantiate(GameManager.PART.GetDeco("Castle"), new Vector3(x, z+.5f, -y), Quaternion.identity);
        }
        if (input == 11) //Ladder Up
        {
            ground = Instantiate(GameManager.PART.GetDeco("Ladder_Up"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 12) //Ladder Down
        {
            ground = Instantiate(GameManager.PART.GetDeco("Ladder_Down"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 13) //Ruins
        {
            ground = Instantiate(GameManager.PART.GetDeco("Ruins"), new Vector3(x, z + .5f, -y), Quaternion.identity);
        }
        if (input == 14) //Shrine
        {
            ground = Instantiate(GameManager.PART.GetDeco("Shrine"), new Vector3(x, z + .5f, -y), Quaternion.identity);
        }
        if (input == 16) //Boulder
        {
            ground = Instantiate(GameManager.PART.GetModel("Boulder"), new Vector3(x, z - .19f, -y), GameManager.PART.GetModel("Boulder").transform.rotation);
        }
        if (input == 17) //Chest
        {
            ground = Instantiate(GameManager.PART.GetDeco("Chest"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 18) //Portal
        {
            ground = Instantiate(GameManager.PART.GetDeco("Portal"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 19) //Altar
        {
            ground = Instantiate(GameManager.PART.GetDeco("Altar"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 20) //Spit
        {
            ground = Instantiate(GameManager.PART.GetDeco("Spit"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 21) //Red Ritual Circle
        {
            ground = Instantiate(GameManager.PART.GetDeco("RitualCircle_Red"), new Vector3(x, z - .49f, -y), Quaternion.Euler(-90f, 0, 0));
        }
        if (input == 37) //Black Ritual Circle
        {
            ground = Instantiate(GameManager.PART.GetDeco("RitualCircle_Black"), new Vector3(x, z - .49f, -y), Quaternion.Euler(-90f, 0, 0));
        }
        if (input == 24) //Armor_Sign
        {
            ground = Instantiate(GameManager.PART.GetDeco("ArmorSign"), new Vector3(x, z + .25f, -y), Quaternion.identity);
        }
        if (input == 25) //Inn_Sign
        {
            ground = Instantiate(GameManager.PART.GetDeco("InnSign"), new Vector3(x, z + .25f, -y), Quaternion.identity);
        }
        if (input == 26) //Magic_Sign
        {
            ground = Instantiate(GameManager.PART.GetDeco("MagicSign"), new Vector3(x, z + .25f, -y), Quaternion.identity);
        }
        if (input == 27) //Potion_Sign
        {
            ground = Instantiate(GameManager.PART.GetDeco("PotionSign"), new Vector3(x, z + .25f, -y), Quaternion.identity);
        }
        if (input == 28) //Sign
        {
            ground = Instantiate(GameManager.PART.GetDeco("Sign"), new Vector3(x, z + .25f, -y), Quaternion.identity);
        }
        if (input == 29) //Weapon_Sign
        {
            ground = Instantiate(GameManager.PART.GetDeco("WeaponSign"), new Vector3(x, z + .25f, -y), Quaternion.identity);
        }
        if (input == 32) //Bones
        {
            ground = Instantiate(GameManager.PART.GetDeco("Bones"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 38) //Skull
        {
            ground = Instantiate(GameManager.PART.GetDeco("Skull"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 39) //Mini tree for overworld maps
        {
            ground = Instantiate(GameManager.PART.GetDeco("Small_Tree"), new Vector3(x, z, -y), Quaternion.identity);
        }
        if (input == 40) //Cactus
        {
            ground = Instantiate(GameManager.PART.GetDeco("Large_Cactus"), new Vector3(x, z + .5f, -y), Quaternion.identity);
        }
        if (input == 41) //Mini cactus for overworld maps
        {
            ground = Instantiate(GameManager.PART.GetDeco("Small_Cactus"), new Vector3(x, z, -y), Quaternion.identity);
        }


        if (ground == null && input >= 0) //Solid Ground
        {
            ground = Instantiate(GameManager.PART.GetModel("Cube"), new Vector3(x, z, -y), GameManager.PART.GetModel("Cube").transform.rotation);
            if (input == 54) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Swamp"); //Swamp
            if (input == 70) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Tile_Floor"); //Tile
            if (input == 80) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Stone_Wall"); //Stone Wall
            if (input == 81) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Locked_Door"); //Locked Door
            if (input == 82) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Door"); //Door
            if (input == 84) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Brick_Floor"); //Brick Floor
            if (input == 85) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Planks"); //Planks
            if (input == 90) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("White"); //White Block
            if (input == 92) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Secret_Passage"); //Secret Passage
            if (input == 94) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Brick_Wall"); //Brick Wall
            if (input == 95) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Black"); //Black
            if (input == 98) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Dirt"); //Dirt
            if (input == 99) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Grass"); //Grass
            if (input == 100) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Sand"); //Sand
            if (input == 101) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Snow"); //Snow
            if (input == 102) ground.GetComponent<MeshRenderer>().material = GameManager.PART.GetMat("Stone"); //Stone
        }
    }

    private void Toon(int input, int x, int y, int z)
    {
        /*
        GameObject toon = null;
        if (input == 0) toon = Instantiate(GameManager.PART.GetToon("Mage"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 1) toon = Instantiate(GameManager.PART.GetToon("Bard"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 2) toon = Instantiate(GameManager.PART.GetToon("Fighter"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 3) toon = Instantiate(GameManager.PART.GetToon("Druid"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 4) toon = Instantiate(GameManager.PART.GetToon("Tinker"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 5) toon = Instantiate(GameManager.PART.GetToon("Paladin"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 6) toon = Instantiate(GameManager.PART.GetToon("Ranger"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 7) toon = Instantiate(GameManager.PART.GetToon("Shepherd"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 8) toon = Instantiate(GameManager.PART.GetToon("Guard"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 9) toon = Instantiate(GameManager.PART.GetToon("Citizen"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 10) toon = Instantiate(GameManager.PART.GetToon("Singing_Bard"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 11) toon = Instantiate(GameManager.PART.GetToon("Jester"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 12) toon = Instantiate(GameManager.PART.GetToon("Beggar"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 13) toon = Instantiate(GameManager.PART.GetToon("Child"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 14) toon = Instantiate(GameManager.PART.GetToon("King"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 15) toon = Instantiate(GameManager.PART.GetToon("Pirate_Ship"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 16) toon = Instantiate(GameManager.PART.GetToon("Nixie"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 17) toon = Instantiate(GameManager.PART.GetToon("Giant_Squid"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 18) toon = Instantiate(GameManager.PART.GetToon("Ghost"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 19) toon = Instantiate(GameManager.PART.GetToon("Mimic"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 20) toon = Instantiate(GameManager.PART.GetToon("Bugs"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 21) toon = Instantiate(GameManager.PART.GetToon("Gazer"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 22) toon = Instantiate(GameManager.PART.GetToon("Phantom"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 23) toon = Instantiate(GameManager.PART.GetToon("Skeleton"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 24) toon = Instantiate(GameManager.PART.GetToon("Rogue"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 25) toon = Instantiate(GameManager.PART.GetToon("Cyclops"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 26) toon = Instantiate(GameManager.PART.GetToon("Wisp"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 27) toon = Instantiate(GameManager.PART.GetToon("Sorceror"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 28) toon = Instantiate(GameManager.PART.GetToon("Lich"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 29) toon = Instantiate(GameManager.PART.GetToon("Bat"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 30) toon = Instantiate(GameManager.PART.GetToon("Goblin"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 31) toon = Instantiate(GameManager.PART.GetToon("Ogre_Mage"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 32) toon = Instantiate(GameManager.PART.GetToon("Orc"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 33) toon = Instantiate(GameManager.PART.GetToon("Snake"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 34) toon = Instantiate(GameManager.PART.GetToon("Troll"), new Vector3(x, z, -y), Quaternion.identity);
        if (input == 35) toon = Instantiate(GameManager.PART.GetToon("Avatar"), new Vector3(x, z, -y), Quaternion.identity);
        */
        if (input == 36 && GameManager.TOON.ToonExists("Frid")) toonMap[x, y] = Instantiate(GameManager.TOON.GetToon("Frid"), new Vector3(x, z, -y), Quaternion.identity);        
    }
}
