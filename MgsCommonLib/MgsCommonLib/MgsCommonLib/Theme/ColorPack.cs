using System;
using System.Collections.Generic;
using UnityEngine;

namespace MgsCommonLib.Theme
{
    [CreateAssetMenu(fileName = "ColorPack", menuName = "Theme/ColorPack", order = 1)]
    public class ColorPack : ScriptableObject
    {
        public List<ColorPackEntry> Colors;
        public Color GetColor(string lable)
        {
            lable = lable.ToLower().Trim();

            foreach (var entry in Colors)
                if (lable == entry.Name.ToLower().Trim())
                    return entry.Color;

            throw new Exception("Color lable with (" + lable + ") not found!!!");
        }
    }

    [Serializable]
    public class ColorPackEntry
    {
        public string Name;
        public Color Color;
    }
}