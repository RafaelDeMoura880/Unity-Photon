using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviourPunCallbacks
{
    public GameObject PrefabJogador;
    
    private void Start()
    {
        if(PrefabJogador != null)
        {
            Vector3 posicao = new Vector3(0, 5, 0);
            PhotonNetwork.Instantiate(PrefabJogador.name, posicao, Quaternion.identity);
        }
    }

    public void SairDaSala()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Conexao");
    }
}
