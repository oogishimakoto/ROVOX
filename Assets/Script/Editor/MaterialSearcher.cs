using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 指定したマテリアルが付いてるレンダラーの検索や置換をするエディタ拡張
/// </summary>
public class MaterialSearcher : EditorWindow
{

    //スクロール位置
    private Vector2 _scrollPosition = Vector2.zero;

    //対象のマテリアル
    private Material _targetMaterial, _replaceTargetMaterial;

    //対象のMaterialが付いてるレンダラー
    private readonly List<Renderer> _targetRenderers = new List<Renderer>();

    //=================================================================================
    //初期化
    //=================================================================================

    //メニューからウィンドウを表示
    [MenuItem("Tools/Open/MaterialSearcher")]
    public static void Open()
    {
        MaterialSearcher.GetWindow(typeof(MaterialSearcher));
    }

    //=================================================================================
    //表示するGUIの設定
    //=================================================================================

    private void OnGUI()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUI.skin.scrollView);

        //対象のマテリアル設定
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        _targetMaterial = (Material)EditorGUILayout.ObjectField("検索対象のMaterial", _targetMaterial, typeof(Material), false);
        _replaceTargetMaterial = (Material)EditorGUILayout.ObjectField("置換対象のMaterial", _replaceTargetMaterial, typeof(Material), false);
        EditorGUILayout.EndHorizontal();

        //検索、置換ボタン
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        if (GUILayout.Button("検索"))
        {
            Search();
        }
        if (GUILayout.Button("置換"))
        {
            Replace();
        }
        EditorGUILayout.EndHorizontal();

        //対象のレンダラー表示
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("対象のMaterialが付いてるレンダラー");
        foreach (var renderer in _targetRenderers)
        {
            //ボタンを押したらHierarchy上で選択するように
            if (GUILayout.Button(renderer.name))
            {
                Selection.activeGameObject = renderer.gameObject;
            }
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndScrollView();
    }

    //=================================================================================
    //検索
    //=================================================================================

    //検索
    private void Search()
    {
        _targetRenderers.Clear();

        //シーン上の有効なRendererを検索し、対象のマテリアルが付いていれば設定
        foreach (var renderer in FindObjectsOfType<Renderer>())
        {
            if (renderer.sharedMaterials.Any(material => material == _targetMaterial))
            {
                _targetRenderers.Add(renderer);
            }
        }

        //名前順にソート
        _targetRenderers.Sort((a, b) => String.CompareOrdinal(a.name, b.name));
    }

    //=================================================================================
    //置換
    //=================================================================================

    //置換
    private void Replace()
    {
        Search();
        if (_targetRenderers.Count == 0)
        {
            EditorUtility.DisplayDialog("置換対象のレンダラーがありません", "置換対象のレンダラーがありません", "OK");
            return;
        }

        Undo.RecordObjects(_targetRenderers.ToArray(), "マテリアルの置換");

        foreach (var targetRenderer in _targetRenderers)
        {
            Material[] newMaterials = targetRenderer.sharedMaterials;
            newMaterials[newMaterials.ToList().IndexOf(_targetMaterial)] = _replaceTargetMaterial;
            targetRenderer.materials = newMaterials;
        }

        EditorUtility.DisplayDialog($"置換が完了しました", $"{_targetRenderers.Count}個のレンダラーの置換を行いました", "OK");
        _targetRenderers.Clear();
    }

}