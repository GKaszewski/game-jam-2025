using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Object mainScene;
        [SerializeField] private Button exitButton;

        private void OnEnable()
        {
            DetectIfBrowser();
        }
        
        private void DetectIfBrowser()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                exitButton.gameObject.SetActive(false);
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(mainScene.name);
        }

        public void ExitGame()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                return;
            }
          
            Application.Quit();
        }
    }
}