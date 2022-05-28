using UnityEngine;

namespace Hotfix2
{
    public class AppStart2
    {
        public static int Test()
        {
            Debug.Log("这里是热更工程2: hello, huatuo");

            var go = new GameObject("HotFix2");
            go.AddComponent<Hotfix.CreateByHotFix2>();

            return 0;
        }
    }
}