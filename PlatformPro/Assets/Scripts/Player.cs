using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// to restart game
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField] private int _coins;
    private UIManager _uiManager;

    //Lives
    [SerializeField] private int _lives = 3;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
            Debug.LogError("Character controller is NULL");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
            Debug.LogError("UIManager is Null");
        _uiManager.UpdateLivesDisplay(_lives);

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

    //Lives Update
    public void LiveLost()
    {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);
        //added scene manager
        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
