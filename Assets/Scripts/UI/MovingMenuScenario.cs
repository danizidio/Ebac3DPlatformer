using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMenuScenario : MonoBehaviour
{
    [SerializeField] float _speed;

    void Update()
    {
        Vector2 v = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        transform.position = v;
    }
}
