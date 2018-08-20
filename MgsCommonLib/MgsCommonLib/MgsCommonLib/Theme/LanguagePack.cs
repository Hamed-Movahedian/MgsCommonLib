using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MgsCommonLib.Theme
{
    [CreateAssetMenu(fileName = "LanguagePack", menuName = "Theme/LanguagePack", order = 1)]
    public class LanguagePack : ScriptableObject
    {
        public List<LangugeLable> Lables;

        public string GetLable(string lable)
        {
            lable = lable.ToLower().Trim();

            foreach (var langugeLable in Lables)
                if (lable == langugeLable.Name.ToLower().Trim())
                    return langugeLable.Text;

            throw new Exception("Lable ("+lable+") not found!!!");
        }

        public List<string> GetEntryNameList()
        {
            return Lables.Select(l => l.Name).ToList();
        }
    }

    [Serializable]
    public class LangugeLable
    {
        public string Name;
        [Multiline] public string Text;
    }
}