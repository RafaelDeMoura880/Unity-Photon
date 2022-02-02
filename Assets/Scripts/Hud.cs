using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text TxtPontuacoes;

    private void Update()
    {
        Jogador[] jogadores = GameObject.FindObjectsOfType<Jogador>();
        string textoPontuacao = "";
        foreach (Jogador j in jogadores)
            textoPontuacao += j.Pontuacao + "; ";
        TxtPontuacoes.text = textoPontuacao;
    }
}
