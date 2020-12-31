using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int facing = 0;
    private bool moveForward = false, moveBackward = false;
    private float x, y, z;
    private bool sunk, hill;
    private MapManager map;

    private bool swim = false, boat = false, ship = false, fly = false; //DEBUG VALUES, stand-in until character is complete        

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(16, 0, -16);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        map = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>(); //get instance of current map manager
    }

    // Update is called once per frame
    void Update()
    {
        int mx = 0, my = 0;
        if (Input.GetKeyUp(KeyCode.UpArrow)) moveForward = true;
        if (Input.GetKeyUp(KeyCode.RightArrow)) facing++;
        if (Input.GetKeyUp(KeyCode.DownArrow)) moveBackward = true;
        if (Input.GetKeyUp(KeyCode.LeftArrow)) facing--;
        if (facing > 3) facing = 0;
        if (facing < 0) facing = 3;
        if (facing == 0) this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (facing == 1) this.transform.rotation = Quaternion.Euler(0, 90, 0);
        if (facing == 2) this.transform.rotation = Quaternion.Euler(0, 180, 0);
        if (facing == 3) this.transform.rotation = Quaternion.Euler(0, -90, 0);
        x = this.transform.position.x; y = this.transform.position.z;

        if (moveForward)
        {
            if (facing == 0) my = 1;
            if (facing == 1) mx = 1;
            if (facing == 2) my = -1;
            if (facing == 3) mx = -1;
            if (IsBlocked(mx, my)) { mx = 0; my = 0; };
            moveForward = false;
        }
        if (moveBackward)
        {
            if (facing == 0) my = -1;
            if (facing == 1) mx = -1;
            if (facing == 2) my = 1;
            if (facing == 3) mx = 1;
            if (IsBlocked(mx, my)) { mx = 0; my = 0; };
            moveBackward = false;
        }

        //determine z
        z = 0;
        sunk = false; hill = false;
        if ((map.blockMap[(int)x + mx, -(int)y + -my] == 1 || map.blockMap[(int)x + mx, -(int)y + -my] == 5) && !boat && !fly) sunk = true; //shallow water
        if (map.blockMap[(int)x + mx, -(int)y + -my] == 2 && swim) sunk = true; //shallow water
        if (map.f1Map[(int)x + mx, -(int)y + -my] == 58) hill = true;
        if (sunk) z = -.25f;
        if (hill) z = 0.25f;

        x += mx; y += my; this.transform.position = new Vector3(x, z, y);
    }

    private bool IsBlocked (int mx, int my)
    {
        bool result = true;
        my *= -1; //Have to switch axis on deltaX so that coordinates match up with mapping software
        int px = (int)x, py = -(int)y;

        //Check for raw collisons
        if (map.blockMap[px + mx, py + my] == 0) result = false; //Open ground
        if(map.blockMap[px + mx, py + my] == 1) //Shallow Water
        {
            if (!sunk) result = false; //Not already sunk at all
            if (sunk && swim) result = false; //Sunk already and do not have swim
        }
        if (map.blockMap[px + mx, py + my] == 2) //Medium Water
        {
            if (swim || boat || fly) result = false; //can traverse if you have swim, boat, or can fly
        }
        if (map.blockMap[px + mx, py + my] == 3) //Deep Water
        {
            if (ship || fly) result = false; //can traverse with a ship (boat upgrade) or fly
        }
        if (map.blockMap[px + mx, py + my] == 4 && fly) result = false; //HalfWall
        if (map.blockMap[px + mx, py + my] == 5 && swim) result = false; //shallow water indoors
        if (map.blockMap[px + mx, py + my] == 6 && !fly) result = false; //Indoors

        //Check for Characters
        if (map.toonMap[px + mx, py + my] != null) //ran into a toon
        {
            result = true; //block movement
            map.toonMap[px + mx, py + my].GetComponent<NPC>().Bump();
            Debug.Log("You run into Toon #" + map.toonIDMap[px + mx, py + my]);
        }

        //Check for scripts
        if (map.scriptMap[px + mx, py + my] > -1)
        {
            //TODO: need a way to call scripts and play them here.
            Debug.Log("You play script #" + map.scriptMap[px + mx, py + my]);
        }

        return result;
    }
}
