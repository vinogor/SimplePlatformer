using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}