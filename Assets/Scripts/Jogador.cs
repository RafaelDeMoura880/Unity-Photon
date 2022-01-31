using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Jogador : MonoBehaviourPun
{
    public float Velocidade = 7;
    public float VelocidadeGiro = 3;

    Rigidbody rb;
    float inputH;
    float inputV;

    SkinnedMeshRenderer playerColor;

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
}
