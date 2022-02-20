using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Controller
    private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _yVelocity;
    private bool _canDoubleJump = false;

    //Coin
    [SerializeField] private int _coins;
    private UIManager _uiManager;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
            Debug.LogError("Character controller is NULL");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
            Debug.LogError("UIManager is Null");
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, _yVelocity, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }
            _yVelocity -= _gravity;
        }
        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

}
