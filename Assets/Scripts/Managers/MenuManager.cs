using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] GameObject display;

        public void StartHost()
        {
            ToggleDisplay(false);
            NetworkManager.Singleton.StartHost();
        }

        public void StartClient()
        {
            ToggleDisplay(false);
            NetworkManager.Singleton.StartClient();
        }

        private void ToggleDisplay(bool isDisplay)
        {
            display.SetActive(isDisplay);
        }
    }
}
