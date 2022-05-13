﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpHeight = 5.0f;
    [SerializeField] private float _yVelocity;
    public bool _canDoubleJump = false;
    private bool _canWallJump = false;
    private UIManager _uiManager;
    [SerializeField] private int _lives = 3;
    [SerializeField] private int _coins;
    private Vector3 _direction, _velocity;
    private Vector3 _wallSurfaceNormal;
    private float _pushPower = 2.0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    void Update()
    {
        PlayerMovement();

    }

    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
    public int CoinCount()
    { 
    return _coins;
    }

    public void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded == true)
        {
            _canWallJump = true;
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
            {       
                    _yVelocity = _jumpHeight;
                    _velocity = _wallSurfaceNormal * _speed;
            }

            _yVelocity -= _gravity * Time.deltaTime;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Check Box
        if (hit.transform.tag == "MovingBox")
        {
            // Confirm that box has RigidBody
            Rigidbody box = hit.collider.GetComponent<Rigidbody>();
            //Null Check
            if (box != null)
            {
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
                box.AddForce(pushDir);
                //Apply Push
                box.velocity = pushDir * _pushPower;
            }
        }

        //Wall Jump
        if (_controller.isGrounded == false && hit.transform.tag == "wall")
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;

        }
    }

}
