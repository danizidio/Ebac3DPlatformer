using System;
using TMPro;
using UnityEngine;

public class VictoryText : MonoBehaviour
{
    public static Action OnFinishLevel;

    Animator _anim;
    TMP_Text _textMeshPro;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _textMeshPro = GetComponent<TMP_Text>();

        OnFinishLevel = FinishLevel;
    }

    private void Start()
    {
        _anim.enabled = false;
        _textMeshPro.enabled = false;
    }

    void FinishLevel()
    {
        _textMeshPro.enabled = true;
        _anim.enabled = true;
    }
}
