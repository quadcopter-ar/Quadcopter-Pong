using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I'm here first");
        ConnectToServer();
    }

    // Update is called once per frame
    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try connect to Server..");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server..");
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);


    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();

        Debug.Log("Creating Player");
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Paddle1"), new Vector3(0,0,0),Quaternion.Euler(0,0,0));
            GameObject emptyPaddle = GameObject.Find("Paddle1(Clone)");
            GameObject paddle = GameObject.Find("Paddle");
            GameObject rig = GameObject.Find("XR Rig");
            //Vector3 temp = new Vector3(-18.7f,-0.28f,1.08f);
            Vector3 temp = new Vector3(0.6f, 0f, 0.75f);
            //Vector3 temp = new Vector3(0f,0f,1.08f);
            rig.transform.parent = emptyPaddle.transform;
            paddle.transform.parent = emptyPaddle.transform;
            GameObject rigOffset = GameObject.Find("Camera Offset");
            rigOffset.transform.localPosition = temp;
            rig.transform.rotation = Quaternion.Euler(0, 0, 0);
            rigOffset.transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject drone_sys = GameObject.Find("DroneSystem");
            drone_sys.GetComponent<DroneConnection>().droneObject = emptyPaddle;
        }
        else
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Paddle2"), new Vector3(0,2,19), Quaternion.Euler(0, 0, 0));
            GameObject emptyPaddle = GameObject.Find("Paddle2(Clone)");
            GameObject paddle = GameObject.Find("Paddle2nd");
            GameObject rig = GameObject.Find("XR Rig");
            Vector3 temp = new Vector3(0f,0f,0f);
            //Vector3 temp = new Vector3(0f,0f,1.08f);
            rig.transform.parent = emptyPaddle.transform;
            rig.transform.localPosition = temp;
            rig.transform.rotation = Quaternion.Euler(0, 0, 0);
            paddle.transform.parent = emptyPaddle.transform;
            GameObject rigOffset = GameObject.Find("Camera Offset");
            rigOffset.transform.localPosition = temp;
            rigOffset.transform.rotation = Quaternion.Euler(0,0,0);

            GameObject drone_sys = GameObject.Find("DroneSystem");
            drone_sys.GetComponent<DroneConnection>().droneObject = emptyPaddle;
        } 
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    void Update()
    {
        GameObject paddle = GameObject.Find("Paddle");
        paddle.transform.localPosition = Vector3.zero;
    }
}
