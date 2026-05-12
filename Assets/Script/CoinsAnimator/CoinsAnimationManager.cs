using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> itens;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        Invoke(nameof(StartAnimations), 0.5f);
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!itens.Contains(i))
        {
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
            // Desativa o colisor para o player não pegar a moeda invisível
            if (i.collider != null) i.collider.enabled = false;
        }
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimations();
        }
    }
    */

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByTime());

        // Busca moedas que acabaram de nascer no novo mapa e ainda não estão na lista
        var newCoins = FindObjectsOfType<ItemCollectableCoin>();
        foreach (var coin in newCoins)
        {
            RegisterCoin(coin);
        }

        StopAllCoroutines();
        // Adicione um pequeno delay (0.2s) para garantir que o mapa terminou de carregar
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime()
    {
        // 1. LIMPEZA: Remove da lista tudo que foi destruído ou está nulo
        itens.RemoveAll(item => item == null);

        // 2. RESET: Faz as moedas que sobraram ficarem com escala zero
        foreach (var p in itens)
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        // 3. ANIMAÇÃO: Anima apenas quem existe de verdade
        for (int i = 0; i < itens.Count; i++)
        {
            // Checagem extra de segurança
            if (itens[i] != null)
            {
                // Faz a moeda crescer
                itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);

                // ATIVA o collider para que ela possa ser coletada agora que apareceu
                if (itens[i].collider != null) itens[i].collider.enabled = true;

                yield return new WaitForSeconds(scaleTimeBetweenPieces);
            }
        }
    }

}
