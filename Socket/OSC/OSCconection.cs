using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(OscIn), typeof(OscOut))]
public class OSCconection : MonoBehaviour {

    public int oscInPort;
    public int oscOutPort;
    public string oscOutIP;

    public OscIn oscIN;
    public OscOut oscOUT;

    //  230.230.230.1   7000
    private void Awake()
    {
        oscIN = GetComponent<OscIn>();
        oscOUT = GetComponent<OscOut>();
    }
    // Use this for initialization
    void Start () {



        // OSC IN
        // oscIN.Open(7000, "224.1.1.1");

        oscIN.Open(oscInPort, oscIN.multicastAddress);
        
		
        if(oscIN == null)
        {
            Debug.Log("error");
        }

        // oscOUT.Open(7000, "224.1.1.1");

        oscOUT.Open(oscOutPort, oscOutIP);
	}

    private void OnEnable()
    {
        oscIN.MapInt("/test1", OnTest1);
        oscIN.MapFloat("/test2", OnTest2);
        oscIN.Map("/AlmalocoOn", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco1" ,HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco2", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco3", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco4", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco5", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco6", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco7", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/Almaloco8", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/AlmalocoOff", HologramManager.instance.OrderFromOSC);
        oscIN.Map("/AlmalocoNext", HologramManager.instance.OrderFromOSC);
    }

    private void OnDisable()
    {
        oscIN.UnmapInt(OnTest1);
    }

    // Update is called once per frame
    void Update () {

        if(Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            oscOUT.Send("/Almaloco", 181 , 77);
            //oscOUT.Send("/test", 77);
            //oscOUT.Send("/test1", Random.Range(0,10));
            Debug.Log("Send OSC val");
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            //oscOUT.Send("/test2", Random.Range(0, 5.0f));
            //Debug.Log("Send OSC val 2");
        }




	}

    public void SendOSC(string addr)
    {
        oscOUT.Send(addr);
    }

    void OnTest1(int value)
    {
        Debug.Log("Received : " + value);
    }
    void OnTest2(float value)
    {
        Debug.Log("Received : " + value);
    }

}
