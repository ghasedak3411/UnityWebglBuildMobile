using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;

public class webglbuild : MonoBehaviour
{
    static string TEXT_TO_COMMENT_OUT = "unityShowBanner('WebGL builds are not supported on mobile devices.');";
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string targetPath)
    {
        if (target != BuildTarget.WebGL)
        {
            return;
        }

        var info = new DirectoryInfo(targetPath);
        var files = info.GetFiles("index.html");
        for (int i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var filePath = file.FullName;
            var text = File.ReadAllText(filePath);
            text = text.Replace(TEXT_TO_COMMENT_OUT, "//" + TEXT_TO_COMMENT_OUT);

            Debug.Log("Removing mobile warning from " + filePath);
            File.WriteAllText(filePath, text);
        }
    }
}
