using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private GameObject loadingObject;
    [SerializeField] private float delay = 3f;

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        if (loadingObject != null)
            loadingObject.SetActive(true);

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("SampleScene");
    }
}