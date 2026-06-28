using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private float restartAfter = 5f; // Za kolik sekund restartovat

    private void Start()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(restartAfter);

        // Naète první scénu (index 0)
        SceneManager.LoadScene(0);
    }
}