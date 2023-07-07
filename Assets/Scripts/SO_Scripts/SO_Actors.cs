using UnityEngine;

[CreateAssetMenu(fileName = "Actor Name", menuName = "Actor Atributes", order = 1)]
public class SO_Actors : ScriptableObject
{
    [SerializeField] float _maxLife;
    public float maxLife { get { return _maxLife; } }

    [SerializeField] float _walkSpeed;
    public float walkSpeed { get { return _walkSpeed; } }

    [SerializeField] float _jumpForce;
    public float jumpForce { get { return _jumpForce; } }
}
