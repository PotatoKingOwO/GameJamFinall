using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyHandler : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string sceneName;

    private void OnDestroy()
    {
        // Deaktivuje cílový objekt
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }

        // Otevøe novou scénu
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}