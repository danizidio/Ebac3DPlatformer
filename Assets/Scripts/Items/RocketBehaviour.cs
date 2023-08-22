using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    bool _levelFinished;
    [SerializeField] float _rocketSpeed;
    [SerializeField] GameObject _rocket;

    private void Update()
    {
        if(_levelFinished)
        {
            _rocket.transform.localPosition = new Vector3(0,_rocket.transform.localPosition.y + (_rocketSpeed + Time.deltaTime),0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();

        if(p != null )
        {
            p.canMove = false;

            p.transform.SetParent(this.transform);

            _levelFinished = true;

            GameplaySateMachine.OnGameStateChange(StateMachines.GameStates.VICTORY, null);
        }
    }
}
