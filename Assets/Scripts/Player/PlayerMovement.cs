using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerBody;

    private Rigidbody _rb;

    private void Awake() => Init();

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public Vector3 MoveHorizontal(float moveSpeed)
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        _rb.velocity = new Vector3(x, _rb.velocity.y, _rb.velocity.z);
        return _rb.velocity;
    }

    public void Rotate()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (moveDir != Vector3.zero)
        {
            _playerBody.rotation = Quaternion.LookRotation(moveDir);
        }
    }

    public void Jump(float jumpPower)
    {
        _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
