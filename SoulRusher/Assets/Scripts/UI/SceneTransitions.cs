using UnityEngine;

public class SceneTransitions : MonoBehaviour
{

    [SerializeField] GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            winScreen.SetActive(true);
        }
    }
}
