using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public static Action<int> OnChooseWeapon;
    public static Action<bool> OnRealoding;
    public static Action<string> OnUpdateAmmo;

    [SerializeField] Image[] _images;

    [SerializeField] Image _reloadImg;

    [SerializeField] TMP_Text _text;

    private void Start()
    {
        _reloadImg.enabled = false;
    }

    void ChooseWeapon(int n)
    {
        for (int i = 0; i < _images.Length; i++)
        {
            if (n == i)
            {
                _images[i].gameObject.SetActive(true);

                _text = _images[i].GetComponentInChildren<TMP_Text>();
            }
            else
            {
                _images[i].gameObject.SetActive(false);
            }
        }
    }

    void Reloading(bool b)
    {
        _reloadImg.enabled = b;
    }

    void UpdateAmmo(string i)
    {
            _text.text = i;
    }

    private void OnEnable()
    {
        OnChooseWeapon = ChooseWeapon;
        OnRealoding = Reloading;
        OnUpdateAmmo = UpdateAmmo;
    }
}
