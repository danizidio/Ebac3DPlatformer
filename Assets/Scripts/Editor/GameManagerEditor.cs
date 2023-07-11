#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GameManager))]

public class GameManagerEditor : Editor
{
  public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        GUILayout.Label("Method Buttons");
        GUILayout.Space(10);

        if (GUILayout.Button("New Random Color"))
        {
            gameManager.AddRandomColorToList();
        }

        if (GUILayout.Button("Spawn Enemy"))
        {
            gameManager.InstantiateObjects();
        }

        if (GUILayout.Button("Change Some Enemy Color"))
        {
            gameManager.ChangeEnemyColor();
        }

        GUILayout.Space(10);
    }

    [MenuItem("Módulo 24/GameObjectOnScene")]
    public static void CreateGameObject()
    {
        if (GameManager.OnCallObj != null)
            GameManager.OnCallObj?.Invoke();
        else MonoBehaviour.print("O botão irá criar o item desde que a cena esteja em 'PLAY MODE'");
    }
}
#endif