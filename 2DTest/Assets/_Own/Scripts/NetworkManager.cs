using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviour
{
    public DropdownCustomization dC;
    public DropdownHandler dH;
    GameObject joiningPanel;
    GameObject colorPanel;
    GameObject colorTakenWarning;

    public Button joinButton;
    public Button colorButton;
    public InputField nickNameIF;

    public string nickName;
    public bool admin;

    //Methode um Serververbindung aufzubauen
    public void Connect()
    {
        Debug.Log("Connect wird ausgeführt");
        //Befehl für die Verbindung zum Photon Network
        PhotonNetwork.ConnectUsingSettings("v01");
    }

    void OnConnectedToMaster()
    {
        Debug.Log("Mit Master verbunden. Szene für Lobby laden.");
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        Debug.Log("Wir sind mit der Lobby verbunden.");
        joiningPanel.SetActive(false);
        colorPanel.SetActive(true);
        //Zufälligen Raum betreten
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        if(!admin)
        {
            //Spawn ausführen
            setColor();
        }
        else
        {
            colorPanel.SetActive(false);
        }
    }

    void Spawn()
    {
        //Erscheinen des Prefabs
        PhotonNetwork.Instantiate(dH.circlePrefab, new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    void Start()
    {
        joiningPanel = GameObject.Find("JoiningPanel");
        colorPanel = GameObject.Find("ColorPanel");
        colorTakenWarning = GameObject.Find("ColorTakenWarning");
        nickName = GameObject.Find("NickNameText").GetComponent<Text>().text.ToString();

        nickNameIF.onValueChanged.AddListener(ValidateInput);

        admin = false;
        colorTakenWarning.SetActive(false);
    }

    void Update()
    {
        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
        Debug.Log("Anzahl weiterer Spieler: " + PhotonNetwork.otherPlayers.Length);
    }

    public void ValidateInput(string input)
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            nickName = input;
        }
    }


    public void SaveNickname()
    {
        //Nicknamen in der Szene finden und abspeichern
        print(nickName);
        //Gespeicherten Nicknamen an Photon übergeben
        PhotonNetwork.player.NickName = nickName;

        if (nickName != "")
        //if (nickName != "" && dH.colorChosen == true)
        {
            Connect();
        }

        if (nickName == "1234")
        {
            admin = true;
            Connect();
        }
    }

    public void setColor()
    {
        //if optionChosen == false then
        foreach (var player in PhotonNetwork.otherPlayers)
        {
            if (dH.selectedColor == (string)player.CustomProperties["color"])
            {
                colorTakenWarning.SetActive(true);
                return;
            }
        }
        if (dH.colorChosen == true)
        {
            colorTakenWarning.SetActive(false);
            Hashtable colorProp = new Hashtable();
            colorProp.Add("color", dH.selectedColor);
            PhotonNetwork.player.SetCustomProperties(colorProp);
            colorPanel.SetActive(false);
            Spawn();
        }
    }
}
