using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SignalRManager : MonoBehaviour
{
    // Start is called before the first frame update
    [DllImport("__Internal")]
    private static extern void SendMessageToParent(string message);

    public void CallSendMessageToParent(string message)
    {
        SendMessageToParent(message);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToParent("TestMethod^5");   
        }
    }
}
