using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float Velocidade = 7;
    public float VelocidadeGiro = 3;

    Rigidbody rb;
    float inputH;
    float inputV;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //caminhada
        Vector3 novaVelocidade = transform.forward * inputV * Velocidade;
        //manter a velocidade em y (para não afetar gravidade)
        novaVelocidade.y = rb.velocity.y;
        rb.velocity = novaVelocidade;

        //giro
        rb.angularVelocity = new Vector3(0, inputH * VelocidadeGiro, 0);
    }
}
