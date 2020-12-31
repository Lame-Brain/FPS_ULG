using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string name;
    public int health, attack, damage, defense;
    public bool stationary, walk, swim, boat, ship, fly;
    public enum ai_types { friendly, animal, neutral, hostile}
    public ai_types ai;
    public string speech;

    private int wounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Billboard functionality
        transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
    }

    public void Bump()
    {
        Debug.Log(name + " says: 'Excuse you!'");
    }

    public void SpeakTo()
    {
        Debug.Log(name + " says: '" + speech + "'");
    }
}
