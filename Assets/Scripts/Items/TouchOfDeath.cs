using UnityEngine;

public class TouchOfDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();

        if (p != null)
        {
            print("touch");
            StartCoroutine(p.AnimGameOver());
        }
    }
}
