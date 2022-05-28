using UnityEngine;

namespace Hotfix
{
    public class PrintHello : MonoBehaviour
    {
        public string text;

        // Start is called before the first frame update
        private void Start()
        {
            Debug.LogFormat("hello, huatuo. {0}", text);
        }
    }
}