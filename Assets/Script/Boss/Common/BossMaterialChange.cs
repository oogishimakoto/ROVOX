using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �w�肵���}�e���A�����t���Ă郌���_���[�̌�����u��������G�f�B�^�g��
/// </summary>
public class BossMaterialChange : MonoBehaviour
{
    // �Ώۂ̃}�e���A��
    [SerializeField] private Material[] _targetMaterial;
    [SerializeField] private Material _replaceTargetMaterial;

    // �Ώۂ�Material���t���Ă郌���_���[
    private readonly List<Renderer> _targetRenderers = new List<Renderer>();

    // �u���ɂ����鎞��
    [SerializeField] private float _transitionDuration = 2.0f;
    [SerializeField] private GameObject exprosionEffect;

    private void Start()
    {
        Search();
    }

    //=================================================================================
    // ����
    //=================================================================================

    // ����
    private void Search()
    {
        _targetRenderers.Clear();

        // �V�[����̗L����Renderer���������A�Ώۂ̃}�e���A�����t���Ă���ΐݒ�
        foreach (var renderer in FindObjectsOfType<Renderer>())
        {
            if (renderer.sharedMaterials.Any(material => _targetMaterial.Contains(material)))
            {
                _targetRenderers.Add(renderer);
            }
        }

        // ���O���Ƀ\�[�g
        _targetRenderers.Sort((a, b) => String.CompareOrdinal(a.name, b.name));
    }

    //=================================================================================
    // �u��
    //=================================================================================

    // �u��
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

        // �ۑ����Ă������߂̌��̃}�e���A��
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

                        // �J���[�̕��
                        if (originalMaterial.HasProperty("_Color") && replaceMaterial.HasProperty("_Color"))
                        {
                            Color originalColor = originalMaterial.color;
                            Color replaceColor = replaceMaterial.color;
                            newMaterials[i].color = Color.Lerp(originalColor, replaceColor, t);
                        }

                        // �G�~�b�V�����̕��
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

        // �Ō�Ɋ��S�ɒu��
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

    //��莞�Ԍ�Ƀu���b�N�I�u�W�F�N�g���폜����֐�
    private IEnumerator CreateEffect()
    {
        //���b�҂�
        yield return new WaitForSeconds(1f);

        GameObject Obj = Instantiate(exprosionEffect, transform);


        //���b�҂�
        yield return new WaitForSeconds(3f);

        //�폜
        Destroy(Obj);

    }
}
