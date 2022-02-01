using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GeradorCaixas : MonoBehaviourPun
{
    public GameObject PrefabCaixa;
    public int MaxCaixas = 5;

    private void Start()
    {
        StartCoroutine(RotinaCriaCaixa());
    }

    //co-rotina para a criação periódica de caixas
    IEnumerator RotinaCriaCaixa()
    {
        while (true)
        {
            CriaCaixa();
            yield return new WaitForSeconds(5);
        }
    }

    //realiza as verificações e instancia em rede de uma caixa em ponto aleatório
    void CriaCaixa()
    {
        if (!PhotonNetwork.IsMasterClient || PrefabCaixa == null)
            return;

        //quantas caixas há em cena
        // (há maneiras mais eficientes de controlar isso)
        int quantidadeDeCaixas = GameObject.FindGameObjectsWithTag("Caixa").Length;
        if(quantidadeDeCaixas < MaxCaixas)
        {
            Vector3 posicao = new Vector3();
            posicao.x = Random.Range(-15f, 15f);
            posicao.y = 5; // criará no alto
            posicao.z = Random.Range(-15f, 15f);

            //instancia em rede
            PhotonNetwork.Instantiate(PrefabCaixa.name, posicao, Quaternion.identity);
        }
    }
}
