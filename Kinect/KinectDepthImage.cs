using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinectDepthImage : MonoBehaviour {

    private KinectManager manager;

    private Texture2D DepthTex;

    public GameObject DepthImage;
    [Range(0,4)]
    public float DetectDepthRangeLow;
    [Range(0,6)]
    public float DetectDepthRangeHigh;



    // Use this for initialization
    void Start () {

        if(manager == null)
        {
            manager = KinectManager.Instance;
        }
        	
	}
	
	// Update is called once per frame
	void Update () {

        UpdateDepthTex();


    }

    private void UpdateDepthTex()
    {
        DepthTex = new Texture2D(512,424);

        /*
        Color[] color = new Color[640 * 480];
        for(int i = 0; i < 640*480; i++)
        {
            if(manager.GetDepthForIndex(i) < DetectDepthRange)
            {
                color[i] = Color.cyan;
            }
            else
            {
                color[i] = Color.black;
            }
        }

        DepthTex.SetPixels(color);
        DepthTex.Apply();
        */

        
        for(int x = 0; x < 512; x++)
        {
            for(int y = 0; y < 424; y++)
            {
                if(manager.GetDepthForPixel(x,y) > DetectDepthRangeLow * 1000 && manager.GetDepthForPixel(x, y) < DetectDepthRangeHigh * 1000)
                {
                    DepthTex.SetPixel(x, y, Color.green);
                }
                else
                {
                    DepthTex.SetPixel(x, y, Color.black);
                }
            }
        }
        Debug.Log(manager.GetDepthForPixel(300, 200));
        DepthTex.Apply();
        //DepthTex = manager.GetUsersLblTex();


        DepthImage.GetComponent<Transform>().localScale = new Vector3((float)512 / 424, 1, 1);
        DepthImage.GetComponent<MeshRenderer>().material.mainTexture = DepthTex;
    }
}
