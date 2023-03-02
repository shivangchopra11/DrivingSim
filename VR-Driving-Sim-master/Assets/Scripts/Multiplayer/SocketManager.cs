using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
 
public class SocketManager : MonoBehaviour
{
    WebSocket socket;
    public GameObject player1;
    public GameObject player2;
    public PlayerData playerData1;
    public PlayerData playerData2;
 
    // Start is called before the first frame update
    void Start()
    {
 
        socket = new WebSocket("ws://143.215.98.112:8080");
        //socket = new WebSocket("ws://websocket-starter-code-multiplayer-websocket-app.bsh-serverconnect-b3c-4x1-162e406f043e20da9b0ef0731954a894-0000.us-south.containers.appdomain.cloud/");
        socket.Connect();
 
        //WebSocket onMessage function
        socket.OnMessage += (sender, e) =>
        {
 
            //If received data is type text...
            if (e.IsText)
            {
                //Debug.Log("IsText");
                //Debug.Log(e.Data);
                JObject jsonObj = JObject.Parse(e.Data);
 
                //Get Initial Data server ID data (From intial serverhandshake
                if (jsonObj["id"] != null)
                {
                    if (playerData1.id == "" ^ playerData1.id == jsonObj["id"].ToString()) {
                        PlayerData tempPlayerData = JsonUtility.FromJson<PlayerData>(e.Data);
                        playerData1 = tempPlayerData;
                        Debug.Log("player ID is " + playerData1.id);
                    } else {
                        PlayerData tempPlayerData = JsonUtility.FromJson<PlayerData>(e.Data);
                        playerData2 = tempPlayerData;
                        Debug.Log("Other player ID is " + playerData2.id);
                    }
                    return;
                }
 
            }
 
        };
 
        //If server connection closes (not client originated)
        socket.OnClose += (sender, e) =>
        {
            Debug.Log(e.Code);
            Debug.Log(e.Reason);
            Debug.Log("Connection Closed!");
        };
    }
 
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.transform.position);
        if (socket == null)
        {
            return;
        }
 
        //If player is correctly configured, begin sending player data to server
        if (player1 != null && playerData1.id != "")
        {
            //Grab player current position and rotation data
            playerData1.xPos = player1.transform.position.x;
            playerData1.yPos = player1.transform.position.y;
            playerData1.zPos = player1.transform.position.z;
 
            System.DateTime epochStart =  new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
            double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
            //Debug.Log(timestamp);
            playerData1.timestamp = timestamp;
 
            string playerDataJSON = JsonUtility.ToJson(playerData1);
            socket.Send(playerDataJSON);
        }
 
        if (player2 != null && playerData2.id != "")
        {
            //Grab player current position and rotation data
            // playerData2.xPos = player2.transform.position.x;
            // playerData2.yPos = player2.transform.position.y;
            // playerData2.zPos = player2.transform.position.z;
 
            // System.DateTime epochStart =  new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
            // double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
            // //Debug.Log(timestamp);
            // playerData.timestamp = timestamp;
 
            // string playerDataJSON = JsonUtility.ToJson(playerData1);
            // socket.Send(playerDataJSON);
            player2.transform.position = new Vector3(playerData2.xPos, playerData2.yPos, playerData2.zPos);
        }
 
        if (Input.GetKeyDown(KeyCode.M))
        {
            string messageJSON = "{\"message\": \"Some Message From Client\"}";
            socket.Send(messageJSON);
        }
    }
 
    private void OnDestroy()
    {
        //Close socket when exiting application
        socket.Close();
    }
 
}