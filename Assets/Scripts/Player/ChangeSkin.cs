using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] _meshes;

    [SerializeField] List<Material> _defaultMaterial;
    [SerializeField] List<Material> _material;

    public void ResetMaterial()
    {
        foreach (var mesh in _meshes)
        {
            mesh.SetMaterials(_defaultMaterial);
        }
    }

    public void ChangingTexture(Material material)
    {
        _material.Add(material);

        foreach (var mesh in _meshes)
        {
            mesh.SetMaterials(_material);
        }

        _material.Clear();
    }
}
