using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public class Jogador : MonoBehaviourPun, IPunObservable
{
    public float Velocidade;
    public float VelocidadeGiro = 3;

    Rigidbody rb;
    float inputH;
    float inputV;

    public int Pontuacao;

    SkinnedMeshRenderer playerColor;

    void Awake()
    {
        Pontuacao = 0;
        Velocidade = 7;
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerColor = this.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

        if (photonView.IsMine)
        {
            playerColor.material.color = Color.blue;
            GetComponent<CameraWork>().OnStartFollowing();
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (other.CompareTag("Explosao"))
            photonView.RPC("RPCPontua", RpcTarget.All, -1);
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
    void RPCPontua(int adicional)
    {
        //isto vai executar a partir de uma chamada
        //remota, do Master para o cliente
        Pontuacao++;
    }

    //[PunRPC]
    //void RPCSpeedUp()
    //{
    //    Velocidade += 2;
    //}

    //public void SpeedUp()
    //{
    //    photonView.RPC("RPCSpeedUp", RpcTarget.All);
    //}

    public void Pontua()
    {
        //isto vai iniciar a chamada remota (Pontua() será invocado pelo Master)
        photonView.RPC("RPCPontua", RpcTarget.All, 1);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //stream é o fluxo de dados na comunicação do Photon
        if (stream.IsWriting)
        {
            //está em ooperação de escrita de dados para a rede?
            //então vamos mandar a informação de Pontuacao nesse fluxo
            stream.SendNext(Pontuacao);
            //stream.SendNext(Velocidade);
        }
        else
        {
            //senão, está em operação de leitura de dados da rede
            //vamos capturar a informação de Pontuacao nesse fluxo
            Pontuacao = (int)stream.ReceiveNext();
            //Velocidade = (int)stream.ReceiveNext();
        }
    }
}
