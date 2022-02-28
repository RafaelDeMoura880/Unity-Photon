using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ConexaoEspera : MonoBehaviourPunCallbacks
{
    public int MaxJogadores = 3;
    public Text TxtStatus;

    int regressiva;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.InRoom)
            TxtStatus.text = PhotonNetwork.CurrentRoom.PlayerCount + "de" + MaxJogadores;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        string textoStatus =
            PhotonNetwork.CurrentRoom.PlayerCount + "de" + MaxJogadores;

        //atualiza em todos os clientes o UI Text de status
        photonView.RPC("AtualizaTextoStatus", RpcTarget.All, textoStatus);

        //se atingiu o número de jogadores, então passar à cena de jogo
        if (PhotonNetwork.CurrentRoom.PlayerCount >= MaxJogadores)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false; //fecha a sala
            //contagem regressiva no UIText de status
            photonView.RPC("IniciaRegressiva", RpcTarget.All);
            //troca cena pelo Master Client 5 segundos depois
            StartCoroutine(IniciaJogo());
        }

    }

    [PunRPC]
    void AtualizaTextoStatus(string mensagem)
    {
        TxtStatus.text = mensagem;
    }

    [PunRPC]
    void IniciaRegressiva()
    {
        regressiva = 5;
        StartCoroutine(CorrotinaRegressiva());
    }

    IEnumerator CorrotinaRegressiva()
    {
        while(regressiva > 0)
        {
            TxtStatus.text = "Iniciando em " + regressiva;
            yield return new WaitForSeconds(1);
            regressiva--;
        }
    }

    IEnumerator IniciaJogo()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LoadLevel("cenario-jogo");
    }
}
