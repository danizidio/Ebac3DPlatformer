using UnityEngine;

public class SlimeEnemy : EnemyBase
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

    }

}
