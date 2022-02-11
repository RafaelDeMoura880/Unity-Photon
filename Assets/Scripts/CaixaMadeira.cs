using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CaixaMadeira : MonoBehaviourPun
{
    public GameObject PrefabMoeda;

    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (other.CompareTag("Explosao"))
        {
            PhotonNetwork.Destroy(gameObject);
            PhotonNetwork.Instantiate(PrefabMoeda.name, transform.position,
                PrefabMoeda.transform.rotation);
        }
    }
}
