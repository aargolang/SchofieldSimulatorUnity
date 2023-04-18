using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Constants
    static Vector3 _s_positionReady = new Vector3(0.0067f,0.4884f,0.0121f);
    static Quaternion _s_rotationPositionReady = Quaternion.Euler(0.0f,0.0f,0.0f);
    static Vector3 _s_positionReload = new Vector3(0.003f,0.48f,0.01f);
    static Quaternion _s_rotationPositionReload = Quaternion.Euler(-36.7f,0.0f,0.0f);
    const float _s_rotationSpeed = 0.8f;
    const float _s_positionSpeed = 0.0001f;
    

    // Gun state
    [SerializeField]
    bool _reloadState; // TODO: change this out for an enumeration of reload, ready, aim, brass check
    [SerializeField]
    Vector3 _positionTarget;
    [SerializeField]
    Quaternion _positionRotationTarget;

    // Start is called before the first frame update
    void Start()
    {
        _positionRotationTarget = _s_rotationPositionReady;
        transform.localRotation = _s_rotationPositionReady;
        _positionTarget = _s_positionReady;
        transform.localPosition = _s_positionReady;

        _reloadState = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown("t"))
        // {
        //     _ToggleHammer();
        // }
        if(Input.GetKeyDown("r"))
        {
            _ToggleReloadState();
        }
        // if(Input.GetMouseButtonDown(0)) // LMB
        // {
        //     // TODO: this
        // }
        // if(Input.GetMouseButtonDown(1)) // RMB
        // {
        //     // TODO: this
        // }
        // if(Input.GetMouseButtonDown(2)) // Middle mouse
        // {
        //     // TODO: this
        // }
        // if(Input.GetMouseButtonDown(3)) // Mouse back
        // {
        //     // TODO: this
        // }
        // if(Input.GetMouseButtonDown(4)) // Mouse forwards
        // {
        //     // TODO: this
        // }
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            _positionTarget,
            _s_positionSpeed
        );
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation, // From here
            _positionRotationTarget, // To here
            _s_rotationSpeed         // This many degrees (up to target position)
        );
    }

    // local animation
    void _ToggleReloadState()
    {
        if (!_reloadState)
        {
            _positionRotationTarget = _s_rotationPositionReload;
            _positionTarget = _s_positionReload;
        }
        else
        {
            _positionRotationTarget = _s_rotationPositionReady;
            _positionTarget = _s_positionReady;
        }
        _reloadState = !_reloadState;
    }
}
