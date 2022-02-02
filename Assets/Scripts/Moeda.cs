using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Moeda : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient && other.CompareTag("Player"))
        {
            Jogador jog = other.GetComponent<Jogador>();
            jog.Pontua();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
