using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviourPunCallbacks
{
    public GameObject PrefabJogador;
    public double TempoMaximo = 45;

    bool jogoEmAndamento;
    
    private void Start()
    {
        if(PrefabJogador != null)
        {
            Vector3 posicao = new Vector3(0, 5, 0);
            PhotonNetwork.Instantiate(PrefabJogador.name, posicao, Quaternion.identity);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable propriedades =
                new ExitGames.Client.Photon.Hashtable();
            propriedades.Add("TempoInicio", PhotonNetwork.Time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(propriedades);
            jogoEmAndamento = true;
        }
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || !jogoEmAndamento)
            return;

        DefineGanhador();
    }

    void DefineGanhador()
    {
        
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
