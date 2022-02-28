using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text TxtPontuacoes;
    public Text TxtResultado;

    private void Update()
    {
        if(PhotonNetwork.CurrentRoom.CustomProperties["TempoInicio"] != null)
        {
            //recuperando o valor do tempo inicial e subtraindo do tempo atual
            double tempo = PhotonNetwork.Time - 
                (double)PhotonNetwork.CurrentRoom.CustomProperties["TempoInicio"];
            //ToString("0") é usado para desprezar casas decimais
            TxtPontuacoes.text = tempo.ToString("0");
        }
    }

    public void MostraResultado(bool vitoria)
    {
        TxtResultado.text = vitoria ? "Vencedor!" : "Perdedor...";
        TxtResultado.gameObject.SetActive(true);
    }

    //Previous Update counting player score

    //private void Update()
    //{
    //    Jogador[] jogadores = GameObject.FindObjectsOfType<Jogador>();
    //    string textoPontuacao = "";
    //    foreach (Jogador j in jogadores)
    //        textoPontuacao += j.Pontuacao + "; ";
    //    TxtPontuacoes.text = textoPontuacao;
    //}
}
