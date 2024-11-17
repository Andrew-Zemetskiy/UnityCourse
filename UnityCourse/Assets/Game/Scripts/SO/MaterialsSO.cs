using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialsSO", menuName = "Scriptable Objects/MaterialsSO")]
public class MaterialsSO : ScriptableObject
{
    [System.Serializable]
    public class MaterialAndColor
    {
        public ColorLib colorName;
        public Material material;
    }
    
    public List<MaterialAndColor> materials = new List<MaterialAndColor>();

    public Material GetMaterial(ColorLib colorName)
    {
        foreach (var mat in materials)
        {
            if (mat.colorName == colorName)
                return mat.material;
        }
        Debug.LogWarning($"Material with color {colorName} not found!");
        return null;
    }

    public Color GetColor(ColorLib colorName) => GetMaterial(colorName).color;
}
