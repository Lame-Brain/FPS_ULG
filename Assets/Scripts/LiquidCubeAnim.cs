using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidCubeAnim : MonoBehaviour
{
    private Material cube;
    private float time, wait = .1f;

    // Start is called before the first frame update
    void Start()
    {
        cube = gameObject.GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > wait)
        {
            time = time - wait;
            cube.mainTextureOffset = new Vector2(cube.mainTextureOffset.x, cube.mainTextureOffset.y - .05f);
        }
    }
}
