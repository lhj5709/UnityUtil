using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.UI;


public class PingChecker : MonoBehaviour {

    public static PingChecker instance;

    public Text debugText;

    public InputField ipInput;
    public Text pingAVG;
    public Text pingCUR;
    public Text pingHigh;
    public Text TargetIPtext;

    public string targetIP;
    public float duration;

    Queue<int> pingDB;
    public double ping_AVG;
    public int ping_HIGH;
    public int ping_LOW = 100000;
    public int ping_CUR;

    public string UserID;

    public Coroutine pingCO;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        Screen.SetResolution(355, 1080, false);

        Initialize();
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.P))
        {
            ThrowPing(duration);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Initialize();
        }

        
        if (Input.GetKeyUp(KeyCode.G))
        {
            StartCoroutine(GetTest());
        }


        TargetIPtext.text = targetIP;
        pingAVG.text = ping_AVG.ToString();
        pingCUR.text = ping_CUR.ToString();
        pingHigh.text = ping_HIGH.ToString();

    }

    public IEnumerator GetTest()
    {
        while(true)
        {
            WWW www = new WWW("http://211.110.139.143/RestService/get/" + UserID);
            yield return www;
            Debug.Log(www.text);

            yield return new WaitForSeconds(1.0f);
        }
    }

    // x button => initialize
    public void Initialize()
    {
        if (pingDB != null) pingDB.Clear();
        else pingDB = new Queue<int>();

        ping_AVG = 0;
        ping_CUR = 0;
        ping_HIGH = 0;
        
    }

    public void CancelCurPing()
    {
        StopAllCoroutines();
    }

    public void ThrowPing(float duration)
    {
        Debug.Log("Throw");
        IPAddress ipadd;
        if(IPAddress.TryParse(ipInput.text, out ipadd))
        {
            targetIP = ipadd.ToString();

            if(pingCO != null)
            {
                StopCoroutine(pingCO); 
            }

            pingCO = StartCoroutine(ThrowPingCoroutine(duration));
            Debug.Log("Start Ping Test");
        }
        else
        {
            Debug.Log("Invalid IP");
        }
    }
    
    public void HowAboutPing(Ping ping)
    {
        if(pingDB.Count > 100)
        {
            pingDB.Dequeue();
        }

        pingDB.Enqueue(ping.time);
        ping_CUR = ping.time;
        ping_HIGH = ping_HIGH < ping_CUR ? ping_CUR : ping_HIGH;
        ping_LOW = ping_LOW > ping_CUR ? ping_CUR : ping_LOW;

        double avg = 0;
        avg = pingDB.Average();
        if (avg != 0) ping_AVG = double.Parse(avg.ToString("N2"));

        PrintPingData();
    }

    public void PrintPingData()
    {
        debugText.text = "CUR : " + ping_CUR + "ms \n" + "HIGH : " + ping_HIGH + "ms \n" + "LOW : " + ping_LOW
            + "ms \n" + "AVG : " + ping_AVG + "ms";
    }

    IEnumerator ThrowPingCoroutine(float duration)
    {
        while(true)
        {
            Ping ping = new Ping(targetIP);

            int pingWait = 1;
           
            while(ping.isDone != true)
            {
                Debug.Log("Not Complete ping");
                if(pingWait++ % 100 == 0)
                {
                    ping = new Ping(targetIP);
                }
                yield return null;             
            }

            pingWait = 0;

            //debugText.text = ping.time + "ms";

            // 평균, 최고, 최저 기록체크
            HowAboutPing(ping);

            yield return new WaitForSeconds(duration);
        }     
    }
}
