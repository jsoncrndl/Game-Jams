using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public UnityEvent onWin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onWin.Invoke();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
