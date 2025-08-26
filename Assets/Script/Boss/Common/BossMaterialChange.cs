using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 指定したマテリアルが付いてるレンダラーの検索や置換をするエディタ拡張
/// </summary>
public class BossMaterialChange : MonoBehaviour
{
    // 対象のマテリアル
    [SerializeField] private Material[] _targetMaterial;
    [SerializeField] private Material _replaceTargetMaterial;

    // 対象のMaterialが付いてるレンダラー
    private readonly List<Renderer> _targetRenderers = new List<Renderer>();

    // 置換にかける時間
    [SerializeField] private float _transitionDuration = 2.0f;
    [SerializeField] private GameObject exprosionEffect;

    private void Start()
    {
        Search();
    }

    //=================================================================================
    // 検索
    //=================================================================================

    // 検索
    private void Search()
    {
        _targetRenderers.Clear();

        // シーン上の有効なRendererを検索し、対象のマテリアルが付いていれば設定
        foreach (var renderer in FindObjectsOfType<Renderer>())
        {
            if (renderer.sharedMaterials.Any(material => _targetMaterial.Contains(material)))
            {
                _targetRenderers.Add(renderer);
            }
        }

        // 名前順にソート
        _targetRenderers.Sort((a, b) => String.CompareOrdinal(a.name, b.name));
    }

    //=================================================================================
    // 置換
    //=================================================================================

    // 置換
    public void Replace()
    {
        Search();
        if (_targetRenderers.Count == 0)
        {
            return;
        }

        StartCoroutine(ReplaceCoroutine());
    }

    private IEnumerator ReplaceCoroutine()
    {
        float elapsedTime = 0f;

        // 保存しておくための元のマテリアル
        Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
        foreach (var targetRenderer in _targetRenderers)
        {
            originalMaterials[targetRenderer] = targetRenderer.sharedMaterials;
        }

        while (elapsedTime < _transitionDuration)
        {
            float t = elapsedTime / _transitionDuration;

            foreach (var targetRenderer in _targetRenderers)
            {
                Material[] newMaterials = targetRenderer.sharedMaterials;
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    if (_targetMaterial.Contains(originalMaterials[targetRenderer][i]))
                    {
                        Material originalMaterial = originalMaterials[targetRenderer][i];
                        Material replaceMaterial = _replaceTargetMaterial;

                        // カラーの補間
                        if (originalMaterial.HasProperty("_Color") && replaceMaterial.HasProperty("_Color"))
                        {
                            Color originalColor = originalMaterial.color;
                            Color replaceColor = replaceMaterial.color;
                            newMaterials[i].color = Color.Lerp(originalColor, replaceColor, t);
                        }

                        // エミッションの補間
                        if (originalMaterial.HasProperty("_EmissionColor") && replaceMaterial.HasProperty("_EmissionColor"))
                        {
                            Color originalEmission = originalMaterial.GetColor("_EmissionColor");
                            Color replaceEmission = replaceMaterial.GetColor("_EmissionColor");
                            newMaterials[i].SetColor("_EmissionColor", Color.Lerp(originalEmission, replaceEmission, t));
                        }
                    }
                }
                targetRenderer.sharedMaterials = newMaterials;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最後に完全に置換
        foreach (var targetRenderer in _targetRenderers)
        {
            Material[] newMaterials = targetRenderer.sharedMaterials;
            for (int i = 0; i < newMaterials.Length; i++)
            {
                if (_targetMaterial.Contains(originalMaterials[targetRenderer][i]))
                {
                    newMaterials[i] = _replaceTargetMaterial;
                }
            }
            targetRenderer.sharedMaterials = newMaterials;
        }
        StartCoroutine(CreateEffect());

        _targetRenderers.Clear();
    }

    //一定時間後にブロックオブジェクトを削除する関数
    private IEnumerator CreateEffect()
    {
        //ｎ秒待つ
        yield return new WaitForSeconds(1f);

        GameObject Obj = Instantiate(exprosionEffect, transform);


        //ｎ秒待つ
        yield return new WaitForSeconds(3f);

        //削除
        Destroy(Obj);

    }
}
