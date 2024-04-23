using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticulateManager : MonoBehaviour
{
    public void TestMethod(int value)
    {
        Debug.Log("inside test method, value is: " + value);
        GameObject.Find("notepad").GetComponent<MeshRenderer>().enabled=true;
        GameObject.Find("notepad_bottom").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("notepad_top").GetComponent<MeshRenderer>().enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
