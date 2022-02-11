using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float Tempo = 3;
    public GameObject ObjetoExplosao;

    private float contagem = 0;
    private bool explodiu = false;
    private Renderer RendererBomba;

    private void Start()
    {
        //certifica que o objeto de explosào é desativado
        ObjetoExplosao.SetActive(false);
        //captura o componente renderizador, para sumir com a bomba na explosão
        RendererBomba = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(!explodiu && contagem >= Tempo)
        {
            //ativa o objeto de explosão, que fará as colisões
            ObjetoExplosao.SetActive(true);
            //some com a bomba
            RendererBomba.enabled = false;
            //se auto destruirá 1 segundo depois
            Destroy(gameObject, 1f);
            //reseta contagem para não repetir a operação
            explodiu = true;
        }
        contagem += Time.deltaTime;
    }
}
