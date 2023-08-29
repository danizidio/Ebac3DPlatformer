using UnityEngine;
using NaughtyAttributes;

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

    [Button]
    public void ForceWin()
    {
        CameraBehaviour.OnChangeCam?.Invoke(CamType.VICTORY_CAM, _rocket);

        _levelFinished = true;

        GameplaySateMachine.OnGameStateChange(StateMachines.GameStates.VICTORY, null);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();

        if(p != null )
        {
            CameraBehaviour.OnChangeCam?.Invoke(CamType.VICTORY_CAM, _rocket);
            p.transform.SetParent(_rocket.transform);
            p.transform.localPosition = Vector3.zero;
            p.ForceStop();
            p.GetComponent<Rigidbody>().isKinematic = true;
            p.enabled = false;

            _levelFinished = true;

            GameplaySateMachine.OnGameStateChange(StateMachines.GameStates.VICTORY, null);
        }
    }
}
