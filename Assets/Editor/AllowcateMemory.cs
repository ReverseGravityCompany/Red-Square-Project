using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEditor;
using UnityEngine;

public class AllowcateMemory : EditorWindow
{
    [MenuItem("Window/MemeoryUsage")]
    public static void MemeoryUsage()
    {
        PlayerSettings.WebGL.memorySize = 1024;
        Debug.Log(GetWindow<AllowcateMemory>());
        Debug.Log(PlayerSettings.WebGL.memorySize);
    }
}
