using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviourPunCallbacks
{
    public GameObject PrefabJogador;
    public Hud HudCena;
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
        //caso a entrada TempoInicio não tenha sido ainda configurada, encerrar o método
        if (PhotonNetwork.CurrentRoom.CustomProperties["TempoInicio"] == null)
            return;

        double tempoInicio = (double)PhotonNetwork.CurrentRoom.CustomProperties["TempoInicio"];

        if(PhotonNetwork.Time - tempoInicio >= TempoMaximo)
        {
            //tempo esgotado
            //buscar por todos os jogadores e encontrar o que tem maior pontuação
            Jogador[] jogadores = GameObject.FindObjectsOfType<Jogador>();
            Jogador maiorPontuador = jogadores[0];
            for (int i = 1; i < jogadores.Length; i++)
            {
                if (jogadores[i].Pontuacao > maiorPontuador.Pontuacao)
                    maiorPontuador = jogadores[i];
            }
            foreach (var jogador in jogadores)
                photonView.RPC("MostraResultado", jogador.photonView.Owner, 
                    jogador == maiorPontuador);

            jogoEmAndamento = false;
        }
    }

    [PunRPC]
    void MostraResultado(bool vitorioso)
    {
        HudCena.MostraResultado(vitorioso);
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
