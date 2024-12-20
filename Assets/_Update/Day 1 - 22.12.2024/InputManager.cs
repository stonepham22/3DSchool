using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private float _horizontal;
    public float Horizontal => _horizontal;
    [SerializeField] private float _vertical;
    public float Vertical => _vertical;
    [SerializeField] private float _jump;
    public float Jump => _jump;
    [SerializeField] private bool _isSprint;
    public bool IsSprint => _isSprint;
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _jump = Input.GetAxis("Jump");
        _isSprint = Input.GetKey(KeyCode.LeftShift);
    }

}
