using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CriaBomba : MonoBehaviourPun
{
    public GameObject PrefabBomba;

    Animator anim;
    bool hasLaunchedBomb = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetButtonDown("Fire1") && hasLaunchedBomb == false)
        {
            anim.SetTrigger("atacando");
            GeraBomba();
            hasLaunchedBomb = true;
            StartCoroutine(TimerBomba());
        }
    }

    void GeraBomba()
    {
        //a bomba será gerada à frente do player
        Vector3 posicao = transform.position + transform.forward * 2;

        //criação em rede
        GameObject novaBomba =
            PhotonNetwork.Instantiate(PrefabBomba.name, posicao, Quaternion.identity);

        //após criar, vamos lançar a bomba ao ar
        Vector3 direcao = transform.forward;
        novaBomba.GetComponent<Rigidbody>().AddForce(direcao * 10, ForceMode.Impulse);
    }

    IEnumerator TimerBomba()
    {
        yield return new WaitForSeconds(2);
        hasLaunchedBomb = false;
    }
}
