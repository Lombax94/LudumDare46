using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gittest : MonoBehaviour
{

    public int NodeType = 0;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().SetNodeType(transform.position, NodeType);
        Debug.Log("FIND");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SendMessageHere(int a) {
        Debug.Log("did work " + a);
    }
}
