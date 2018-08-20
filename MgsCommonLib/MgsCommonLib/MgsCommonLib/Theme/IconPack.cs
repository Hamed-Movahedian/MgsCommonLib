using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MgsCommonLib.Theme
{
    [CreateAssetMenu(fileName = "IconPack", menuName = "Theme/IconPack", order = 1)]
    public class IconPack : ScriptableObject
    {
        public List<IconPackEntry> Icons;

        public Sprite GetIcon(string lable)
        {
            lable = lable.ToLower().Trim();

            foreach (var entry in Icons)
                if (lable == entry.Name.ToLower().Trim())
                    return entry.Icon;

            throw new Exception("Icon lable with (" + lable + ") not found!!!");
        }
        public List<string> GetEntryNameList()
        {
            return Icons.Select(l => l.Name).ToList();
        }
    }
    [Serializable]
    public class IconPackEntry
    {
        public string Name;
        public Sprite Icon;
    }
}