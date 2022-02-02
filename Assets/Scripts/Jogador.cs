using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Jogador : MonoBehaviourPun, IPunObservable
{
    public float Velocidade = 7;
    public float VelocidadeGiro = 3;

    Rigidbody rb;
    float inputH;
    float inputV;

    public int Pontuacao;

    SkinnedMeshRenderer playerColor;

    void Awake()
    {
        Pontuacao = 0;
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerColor = this.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

        if (photonView.IsMine)
            playerColor.material.color = Color.blue;
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;
        
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;
        
        //caminhada
        Vector3 novaVelocidade = transform.forward * inputV * Velocidade;
        //manter a velocidade em y (para não afetar gravidade)
        novaVelocidade.y = rb.velocity.y;
        rb.velocity = novaVelocidade;

        //giro
        rb.angularVelocity = new Vector3(0, inputH * VelocidadeGiro, 0);
    }

    [PunRPC]
    void RPCPontua()
    {
        //isto vai executar a partir de uma chamada
        //remota, do Master para o cliente
        Pontuacao++;
    }

    public void Pontua()
    {
        //isto vai iniciar a chamada remota (Pontua() será invocado pelo Master)
        photonView.RPC("RPCPontua", RpcTarget.All);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //stream é o fluxo de dados na comunicação do Photon
        if (stream.IsWriting)
        {
            //está em ooperação de escrita de dados para a rede?
            //então vamos mandar a informação de Pontuacao nesse fluxo
            stream.SendNext(Pontuacao);
        }
        else
        {
            //senão, está em operação de leitura de dados da rede
            //vamos capturar a informação de Pontuacao nesse fluxo
            Pontuacao = (int)stream.ReceiveNext();
        }
    }
}
