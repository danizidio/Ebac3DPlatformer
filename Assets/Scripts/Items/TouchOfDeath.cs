using UnityEngine;

public class TouchOfDeath : MonoBehaviour
{

    GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();

        if (p != null)
        {
            MessageLog.OnCallMessage("Dead... Respawning!");
            GameplaySateMachine.OnGameStateChange(StateMachines.GameStates.GAMEOVER, _gameManager);
        }
    }
}
