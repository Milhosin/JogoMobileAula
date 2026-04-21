using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materiais;
    public List<ColorSetup> colorSetups;

    public void ChanceColorByType(ArtManager.ArtType artType)
    {
        var setup = colorSetups.Find(i => i.artType == artType);

        for(int i = 0; i < materiais.Count; i++)
        {
            materiais[i].SetColor("_BaseColor", setup.colors[i]);
        }
    }
}

[System.Serializable]
public class ColorSetup
{
    public ArtManager.ArtType artType;
    public List<Color> colors;
}
