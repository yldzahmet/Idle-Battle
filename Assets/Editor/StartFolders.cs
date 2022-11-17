using UnityEngine;
using UnityEditor;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

public static class StartFolders
{
#if UNITY_EDITOR
    [MenuItem("Tools/Setup/Create Default Folders")]
#endif
    public static void CreadteDefaultFolder()
    {
        CreateFoldersToRoot("Scripts", "Arts", "Prefabs", "Scenes");
        CreateFolders("Arts", "Prefabs", "Models", "Environment", "Animations", "Animators", "Metarials", "Textures");
    }

    public static void CreateFolders(string root, params string[] directions)
    {
        string fullPath = Combine(dataPath, root);

        foreach (var item in directions)
            CreateDirectory(Combine(fullPath, item));

        Refresh();
    }

    public static void CreateFoldersToRoot(params string[] directions)
    {
        foreach (var item in directions)
            CreateDirectory(Combine(dataPath, item));

        Refresh();
    }
}
