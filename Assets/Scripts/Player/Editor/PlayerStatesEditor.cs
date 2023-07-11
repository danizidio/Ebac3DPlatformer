#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using StateMachines;

[CustomEditor(typeof(PlayerStatesBehaviour))]
public class PlayerStatesEditor : Editor
{
    [SerializeField] bool _foldOut;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerStatesBehaviour gsm = (PlayerStatesBehaviour)target;

        EditorGUILayout.LabelField("STATE MACHINE");

        if (gsm.statemachine == null) return;

        if (gsm.statemachine.currentState != null)
        {
            EditorGUILayout.LabelField("CURRENT STATE: ", gsm.statemachine.currentState.ToString());
        }

        _foldOut = EditorGUILayout.Foldout(_foldOut, "AVAILABLE STATES");

        if (_foldOut)
        {
            if (gsm.statemachine != null)
            {
                var keys = gsm.statemachine.states.Keys.ToArray();
                var values = gsm.statemachine.states.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
                }
            }
        }
    }
}
#endif