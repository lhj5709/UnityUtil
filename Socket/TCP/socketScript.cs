using UnityEngine;

using System.Collections;

using System.Collections.Generic;


using System.Text;

using System;



public class socketScript : MonoBehaviour
{



    //variables

    private TCPConnection myTCP;

    private string serverMsg;

    public string msgToServer;

    public string serverSays;




    void Awake()
    {

        //add a copy of TCPConnection to this game object

        myTCP = gameObject.GetComponent<TCPConnection>();

    }



    void Start()
    {

        //if connection has not been made, display button to connect

        if (myTCP.socketReady == false)
        {



            myTCP.setupSocket();



        }



        //once connection has been made, display editable text field with a button to send that string to the server (see function below)

        if (myTCP.socketReady == true)
        {
            /*
            myTCP.closeSocket();

            myTCP.setupSocket();
            */

            SendToServer(msgToServer);



        }





    }



    void Update()
    {



        //keep checking the server for messages, if a message is received from server, it gets logged in the Debug console (see function below)

        SocketResponse();



    }



  


    //socket reading script

    void SocketResponse()
    {

        serverSays = myTCP.readSocket();

        if (serverSays != "")
        {

            Debug.Log("[SERVER]" + serverSays);

        }

        //SendToServer(serverSays);

        HologramManager.instance.OrderFromUDP(serverSays);

    }



    //send message to the server

    public void SendToServer(string str)
    {

        myTCP.writeSocket(str);

        Debug.Log("[CLIENT] -> " + str);

    }



}
