using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// �w�肵���}�e���A�����t���Ă郌���_���[�̌�����u��������G�f�B�^�g��
/// </summary>
public class MaterialSearcher : EditorWindow
{

    //�X�N���[���ʒu
    private Vector2 _scrollPosition = Vector2.zero;

    //�Ώۂ̃}�e���A��
    private Material _targetMaterial, _replaceTargetMaterial;

    //�Ώۂ�Material���t���Ă郌���_���[
    private readonly List<Renderer> _targetRenderers = new List<Renderer>();

    //=================================================================================
    //������
    //=================================================================================

    //���j���[����E�B���h�E��\��
    [MenuItem("Tools/Open/MaterialSearcher")]
    public static void Open()
    {
        MaterialSearcher.GetWindow(typeof(MaterialSearcher));
    }

    //=================================================================================
    //�\������GUI�̐ݒ�
    //=================================================================================

    private void OnGUI()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUI.skin.scrollView);

        //�Ώۂ̃}�e���A���ݒ�
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        _targetMaterial = (Material)EditorGUILayout.ObjectField("�����Ώۂ�Material", _targetMaterial, typeof(Material), false);
        _replaceTargetMaterial = (Material)EditorGUILayout.ObjectField("�u���Ώۂ�Material", _replaceTargetMaterial, typeof(Material), false);
        EditorGUILayout.EndHorizontal();

        //�����A�u���{�^��
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        if (GUILayout.Button("����"))
        {
            Search();
        }
        if (GUILayout.Button("�u��"))
        {
            Replace();
        }
        EditorGUILayout.EndHorizontal();

        //�Ώۂ̃����_���[�\��
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("�Ώۂ�Material���t���Ă郌���_���[");
        foreach (var renderer in _targetRenderers)
        {
            //�{�^������������Hierarchy��őI������悤��
            if (GUILayout.Button(renderer.name))
            {
                Selection.activeGameObject = renderer.gameObject;
            }
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndScrollView();
    }

    //=================================================================================
    //����
    //=================================================================================

    //����
    private void Search()
    {
        _targetRenderers.Clear();

        //�V�[����̗L����Renderer���������A�Ώۂ̃}�e���A�����t���Ă���ΐݒ�
        foreach (var renderer in FindObjectsOfType<Renderer>())
        {
            if (renderer.sharedMaterials.Any(material => material == _targetMaterial))
            {
                _targetRenderers.Add(renderer);
            }
        }

        //���O���Ƀ\�[�g
        _targetRenderers.Sort((a, b) => String.CompareOrdinal(a.name, b.name));
    }

    //=================================================================================
    //�u��
    //=================================================================================

    //�u��
    private void Replace()
    {
        Search();
        if (_targetRenderers.Count == 0)
        {
            EditorUtility.DisplayDialog("�u���Ώۂ̃����_���[������܂���", "�u���Ώۂ̃����_���[������܂���", "OK");
            return;
        }

        Undo.RecordObjects(_targetRenderers.ToArray(), "�}�e���A���̒u��");

        foreach (var targetRenderer in _targetRenderers)
        {
            Material[] newMaterials = targetRenderer.sharedMaterials;
            newMaterials[newMaterials.ToList().IndexOf(_targetMaterial)] = _replaceTargetMaterial;
            targetRenderer.materials = newMaterials;
        }

        EditorUtility.DisplayDialog($"�u�����������܂���", $"{_targetRenderers.Count}�̃����_���[�̒u�����s���܂���", "OK");
        _targetRenderers.Clear();
    }

}