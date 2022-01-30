using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Conexao : MonoBehaviourPunCallbacks
{
    string versao = "1";
    bool conectando = false;
    
    private void Start()
    {
        //Conectar();
    }

    public void Conectar()
    {
        if (!PhotonNetwork.IsConnected)
        {
            conectando =PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = versao;
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnConnectedToMaster()
    {
        if (conectando)
        {
            Debug.Log("Conectado ao servidor. A lógica do jogo pode iniciar aqui");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado. Causa: " + cause);
        conectando = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Juntou-se a uma sala de jogo!");
        //aqui vai chamar a cena de jogo
        PhotonNetwork.LoadLevel("cenario-jogo1");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Falhou ao tentar juntar a uma sala de jogo code: " 
            + returnCode + "; msg: " + message + ". Vamos criar uma sala"); 
        PhotonNetwork.CreateRoom("sala-jogo", new RoomOptions() { MaxPlayers = 4 });
    }
}
