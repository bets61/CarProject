using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Yar��� kazand�n!");
            SceneManager.LoadScene("WinScene"); // veya sahnede "Tebrikler" UI a�
        }
    }
}
