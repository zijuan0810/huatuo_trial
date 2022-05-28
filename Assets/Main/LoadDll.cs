using System;
using System.Linq;
using UnityEngine;

public class LoadDll : MonoBehaviour
{
    private void Start()
    {
        BetterStreamingAssets.Initialize();
        LoadGameDll();
        RunMain();
    }

    private System.Reflection.Assembly _assembly;

    private void LoadGameDll()
    {
        AssetBundle dllAB = BetterStreamingAssets.LoadAssetBundle("common");
#if UNITY_EDITOR
        _assembly = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "HotFix2");
#else
        TextAsset dllBytes1 = dllAB.LoadAsset<TextAsset>("HotFix.dll.bytes");
        System.Reflection.Assembly.Load(dllBytes1.bytes);
        TextAsset dllBytes2 = dllAB.LoadAsset<TextAsset>("HotFix2.dll.bytes");
        _assembly = System.Reflection.Assembly.Load(dllBytes2.bytes);
#endif

        //实例化一个对象
        Instantiate(dllAB.LoadAsset<GameObject>("HotUpdatePrefab.prefab"));
    }

    private void RunMain()
    {
        if (_assembly == null)
        {
            Debug.LogError("dll未加载");
            return;
        }

        var appType = _assembly.GetType("Hotfix2.AppStart2");
        var method = appType.GetMethod("Test");
        if (method != null) 
            method.Invoke(null, null);

        // 如果是Update之类的函数，推荐先转成Delegate再调用，如
        //var updateMethod = appType.GetMethod("Update");
        //var updateDel = System.Delegate.CreateDelegate(typeof(Action<float>), null, updateMethod);
        //updateMethod(deltaTime);
    }
}