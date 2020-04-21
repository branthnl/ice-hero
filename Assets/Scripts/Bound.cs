using UnityEngine;

public class Bound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // other.GetComponent<Ball>().Respawn();
            if (LevelManager.instance.isGameOver || FindObjectsOfType<Ball>().Length > 1) return;
            LevelManager.instance.UserSelectRestart();
        }
        else if (other.CompareTag("Penguin"))
        {
            LevelManager.instance.AddPenguin();
        }
    }
}