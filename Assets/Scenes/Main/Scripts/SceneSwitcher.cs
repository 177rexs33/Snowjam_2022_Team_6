using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SJ22
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] float targetTime;
        [SerializeField] string sceneName;

        bool loadedScene = false;

        void Update()
        {
            if(time.Time > targetTime && !loadedScene)
            {
                SceneManager.LoadScene(sceneName);
                loadedScene = true;
            }
        }
    }
}
