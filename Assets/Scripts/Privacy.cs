using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Privacy : MonoBehaviour
{
    public UniWebView uniWebView;
    public void OpenWebview(string url)
    {
        var webviewObject = new GameObject("UniWebview");
        uniWebView = webviewObject.AddComponent<UniWebView>();
        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.SetShowToolbar(true, false, true, true);
        uniWebView.Load(url);
        uniWebView.Show();
    }
}
