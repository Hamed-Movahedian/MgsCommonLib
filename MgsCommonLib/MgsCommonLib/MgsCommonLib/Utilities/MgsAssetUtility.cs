using UnityEngine;

namespace MgsCommonLib.Utilities
{
    public static class MgsAssetUtility
    {
        public static string FullPathToAssetPath(string fullName)
        {
            return 
                "Assets" + 
                fullName
                    .Replace('\\', '/')
                    .Substring(Application.dataPath.Length);
        }
    }
}
