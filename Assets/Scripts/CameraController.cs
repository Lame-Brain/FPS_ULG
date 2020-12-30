using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int facing = 0;
    private bool moveForward = false, moveBackward = false;
    private float x, y;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(16, 0, -16);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)) moveForward = true;
        if (Input.GetKeyUp(KeyCode.RightArrow)) facing++;
        if (Input.GetKeyUp(KeyCode.DownArrow)) moveBackward = true;
        if (Input.GetKeyUp(KeyCode.LeftArrow)) facing--;
        if (facing > 3) facing = 0;
        if (facing < 0) facing = 3;
        if (facing == 0) Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (facing == 1) Camera.main.transform.rotation = Quaternion.Euler(0, 90, 0);
        if (facing == 2) Camera.main.transform.rotation = Quaternion.Euler(0, 180, 0);
        if (facing == 3) Camera.main.transform.rotation = Quaternion.Euler(0, -90, 0);
        x = Camera.main.transform.position.x; y = Camera.main.transform.position.z;
        if (moveForward)
        {
            if (facing == 0) y++;
            if (facing == 1) x++;
            if (facing == 2) y--;
            if (facing == 3) x--;
            moveForward = false;
        }
        if (moveBackward)
        {
            if (facing == 0) y--;
            if (facing == 1) x--;
            if (facing == 2) y++;
            if (facing == 3) x++;
            moveBackward = false;
        }
        Camera.main.transform.position = new Vector3(x, 0, y);
    }
}
