using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController 
{
   public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneDelayed(MonoBehaviour owner, float delay, int sceneIndex)
    {
        owner.StartCoroutine(LoadDelayed(delay, sceneIndex));
    }

    private IEnumerator LoadDelayed(float delay, int sceneIndex)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneIndex);
    }
}
