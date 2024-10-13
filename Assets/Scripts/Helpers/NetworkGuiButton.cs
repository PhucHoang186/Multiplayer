using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class NetworkGuiButton : MonoBehaviour
{
    void OnGUI()
    {
        if (!Application.isPlaying)
            return;

        if(GUILayout.Button("Start Host"))
        {
            NetworkManager.Singleton.StartHost();
        }

        if(GUILayout.Button("Start Server"))
        {
            NetworkManager.Singleton.StartServer();
        }

        if(GUILayout.Button("Start Client"))
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
