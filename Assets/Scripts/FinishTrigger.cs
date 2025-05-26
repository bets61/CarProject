using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Yarýþý kazandýn!");
            SceneManager.LoadScene("WinScene"); // veya sahnede "Tebrikler" UI aç
        }
    }
}
