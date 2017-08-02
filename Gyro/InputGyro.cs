using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputGyro : MonoBehaviour {

    public static InputGyro instance;
    public Text GyroText;

	// Use this for initialization
	void Start () {

        Input.gyro.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        GyroText.text = Input.gyro.enabled.ToString() + "\n";
        GyroText.text += Input.gyro.attitude.eulerAngles.ToString();
		
	}
}
