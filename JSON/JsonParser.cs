using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using MSLx;

public class JsonParser : MonoBehaviour {


    public GameObject DetectMap;
    public GameObject DetectObj;

    private void Awake()
    {
        Parsing();
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Parsing()
    {
        string text = System.IO.File.ReadAllText(@"C:\Users\MapControl.json");
        var N = JSON.Parse(text);
        var versionString = N["version"].Value;        // versionString will be a string containing "1.0"
        var versionNumber = N["version"].AsFloat;      // versionNumber will be a float containing 1.0
        string AreaX = N["data"]["AreaX"];// name will be a string containing "sub objec
        string AreaY = N["data"]["AreaY"];
        string AreaW = N["data"]["AreaW"];
        string AreaH = N["data"]["AreaH"];
        string PoolSize = N["data"]["PoolSize"];

        DetectMap.GetComponent<DetectingMap>().AreaX = int.Parse(AreaX);
        DetectMap.GetComponent<DetectingMap>().AreaY = int.Parse(AreaY);
        DetectMap.GetComponent<DetectingMap>().AreaW = int.Parse(AreaW);
        DetectMap.GetComponent<DetectingMap>().AreaH = int.Parse(AreaH);

        DetectObj.GetComponent<DetectObjects>().MapPoolSize = int.Parse(PoolSize);

    }
}
