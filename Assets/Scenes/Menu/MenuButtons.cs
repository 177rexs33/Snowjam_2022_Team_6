using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SJ22
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] Button startButton;
        [SerializeField] Button quitButton;

        [SerializeField] string gameSceneName;

        void Awake()
        {
            startButton.onClick.AddListener(OnStartPressed);
            quitButton.onClick.AddListener(OnQuitPressed);
        }

        void OnStartPressed()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}
