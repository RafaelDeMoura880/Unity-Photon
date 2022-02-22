using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Jogador MeuPlayer;
    public Text TxtPontuacao;

    private void Update()
    {
        TxtPontuacao.text = "$ " + MeuPlayer.Pontuacao;
    }
}
