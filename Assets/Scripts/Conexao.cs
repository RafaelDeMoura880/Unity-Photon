using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Conexao : MonoBehaviourPunCallbacks
{
    string versao = "1";
    bool conectando = false;
    GameObject conectandoObjectText;
    GameObject conectarObjectButton;
    
    private void Start()
    {
        //Conectar();
        conectandoObjectText = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        conectarObjectButton = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
    }

    public void Conectar()
    {
        if (!PhotonNetwork.IsConnected)
        {
            conectando = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = versao;
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }

        conectandoObjectText.gameObject.SetActive(true);
        conectarObjectButton.gameObject.SetActive(false);
    }

    public void Desconectar()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        if (conectando)
        {
            Debug.Log("Conectado ao servidor. A lógica do jogo pode iniciar aqui");
            conectandoObjectText.gameObject.GetComponent<Text>().text = "Conectado!";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado. Causa: " + cause);
        conectandoObjectText.gameObject.SetActive(false);
        conectarObjectButton.gameObject.SetActive(true);
        conectando = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Juntou-se a uma sala de jogo!");
        //PhotonNetwork.LoadLevel("cenario-jogo1");
        UnityEngine.SceneManagement.SceneManager.LoadScene("sala-espera");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Falhou ao tentar juntar a uma sala de jogo code: " 
            + returnCode + "; msg: " + message + ". Vamos criar uma sala"); 
        PhotonNetwork.CreateRoom("sala-jogo", new RoomOptions() { MaxPlayers = 4 });
    }
}
