using UnityEngine;

public class SlimeSimple : EnemyBase
{
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (_player == null) return;

        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
        transform.LookAt(_player.transform.position);
    }

}
