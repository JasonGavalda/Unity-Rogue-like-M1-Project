using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] Seed seed;
    [SerializeField] TMP_Text seedText;

    public const byte SendSeed = 2;
    public const byte AskSeed = 1;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        Debug.Log("Connecting");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("JoinedLobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
                        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master Seed");
            seed.seedValue = 42;//Random.Range(0, 1000);
            seedText.text = seed.GetSeedValue().ToString("0000");

        }
        //else
        //{
        //    //Debug.Log("Client ask seed value");
        //    //AskSeedEvent();
        //    PhotonView photonView = PhotonView.Get(this);
        //    photonView.RPC("SetSeedValue",RpcTarget.All,)
        //}

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    //private void AskSeedEvent()
    //{
    //    //object[] content = new object[] { new Vector3(10.0f, 2.0f, 5.0f), 1, 2, 5, 10 }; // Array contains the target position and the IDs of the selected units
    //    object[] content = new object[] { seed.GetSeedValue() };
    //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
    //    PhotonNetwork.RaiseEvent(AskSeed, content, raiseEventOptions, SendOptions.SendReliable);
    //}

    //private void SendSeedEvent()
    //{
    //    //object[] content = new object[] { new Vector3(10.0f, 2.0f, 5.0f), 1, 2, 5, 10 }; // Array contains the target position and the IDs of the selected units
    //    object[] content = new object[] { seed.GetSeedValue() };
    //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
    //    PhotonNetwork.RaiseEvent(SendSeed, content, raiseEventOptions, SendOptions.SendReliable);
    //}

    //new private void OnEnable()
    //{
    //    PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    //}

    //new private void OnDisable()
    //{
    //    PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    Debug.Log("Event received");
    //    if (obj.Code == SendSeed)
    //    {
    //        Debug.Log("Changing Seed Value");
    //        object[] data = (object[]) obj.CustomData;
    //        seed.SetSeedValue((int)data[0]);
    //        seedText.text = seed.GetSeedValue().ToString("0000");
    //    }

    //    if (obj.Code == AskSeed)
    //    {
    //        Debug.Log("Asking Seed Value");
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            Debug.Log("master send value");
    //            SendSeedEvent();
    //        }
    //    }
    //}


    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed : " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        seed.seedValue = 0;
        seedText.text = "Seed";
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("master send seed");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("SetSeedValue", RpcTarget.All, seed.GetSeedValue());
        }
    }

    [PunRPC]
    public void SetSeedValue(int _seed)
    {
        Debug.Log("entré");
        //if (seed.seedValue == -1) { 
            Debug.Log("seed reçue");
            seed.seedValue = _seed;
            seedText.text = seed.GetSeedValue().ToString("0000");
        //}
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(2);

    }
}
