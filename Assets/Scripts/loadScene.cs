using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadScene : MonoBehaviour {

    //public GameObject loadingScreen;
    //public Slider loadingSlider;

	//public void loadGameScene(int sceneIndex)
 //   {
 //       StartCoroutine(loadSceneAsync(sceneIndex));
 //   }
    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //IEnumerator loadSceneAsync(int sceneIndex)
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

    //    while (!operation.isDone)
    //    {
    //        loadingScreen.SetActive(true);
    //        float progress = Mathf.Clamp01(operation.progress / 0.9f);

    //        loadingSlider.value = progress;
            
    //        yield return null;
    //    }
        
    //}


}
