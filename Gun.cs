using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Constants
    static Vector3 _s_positionReady = new Vector3(0.0067f,0.4884f,0.0121f);
    static Quaternion _s_rotationReady = Quaternion.Euler(17.7f,0.0f,0.0f);
    static Vector3 _s_positionReload = new Vector3(0.003f,0.48f,0.01f);
    static Quaternion _s_rotationReload = Quaternion.Euler(-36.7f,0.0f,0.0f);
    static Vector3 _s_positionAim = new Vector3(-0.0005f,0.49f,0.01f);
    static Quaternion _s_rotationAim = Quaternion.Euler(3.0f,0.0f,0.0f);
    const float _s_positionSpeed = 0.0001f;
    public float _lerpDuration = 6.0f; 
    public AnimationCurve anim;
    
    // Stuct for position rotation combos
    public struct PosRot
    {
        public PosRot(Vector3 position, Quaternion rotation)
        {
            pos = position;
            rot = rotation;
        }

        public void Set(Vector3 position, Quaternion rotation)
        {
            pos = position;
            rot = rotation;
        }

        public Vector3 pos;
        public Quaternion rot;
    }

    // 'int only' enumeration (no casting needed)
    public static class State
    {
        public const int Ready = 0;
        public const int Reload = 1;
        public const int Aim = 2;
    }

    // The array of states we can be in that includes position and rotation
    PosRot[] _stateArray =
    {
        new PosRot(_s_positionReady, _s_rotationReady),     // Ready 
        new PosRot(_s_positionReload, _s_rotationReload),   // Reload
        new PosRot(_s_positionAim, _s_rotationAim)          // Aim
    };

    // Gun state
    [SerializeField]
    int _gunState;

    PosRot _posRotSource;
    PosRot _posRotTarget;

    // Interpolation values
    float _lerpCoeff;
    float _lerpVal;
    int _lerpMin;
    int _lerpMax;
    bool _lerpTransitioning;

    // Component variables
    GunCylinder cylinder; 

    // Start is called before the first frame update
    void Start()
    {
        _gunState = State.Ready;
        // set local PosRot
        transform.localRotation = _stateArray[_gunState].rot;
        transform.localPosition = _stateArray[_gunState].pos;
        _posRotSource.rot = _stateArray[_gunState].rot;
        _posRotSource.pos = _stateArray[_gunState].pos;
        _posRotTarget.rot = _stateArray[_gunState].rot;
        _posRotTarget.pos = _stateArray[_gunState].pos;

        // interpolation
        // _lerpTransitioning = true;
        // _lerpMin = 0;
        _lerpMax = (int)(1/Time.deltaTime * _lerpDuration);

        cylinder = GameObject.GetC
    }

    // Update is called once per frame
    void Update()
    {


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
        // transform.localPosition = Vector3.MoveTowards(
        //     transform.localPosition,
        //     _positionTarget,
        //     _s_positionSpeed
        // );
        // transform.localRotation = Quaternion.RotateTowards(
        //     transform.localRotation, // From here
        //     _posRotTarget, // To here
        //     _s_rotationSpeed         // This many degrees (up to target position)
        // );
        if(Input.GetMouseButtonDown(1)) // RMB
        {
            _toggleState(State.Aim);
        }
        if(Input.GetKeyDown("r"))
        {
            // _toggleReloadState();
            _toggleState(State.Reload);
        }

        _processLerpState();
    }

    void _processLerpState()
    {
        _lerpValAdd();

        _lerpCoeff = ((float)_lerpVal/_lerpMax);
        _lerpCoeff = anim.Evaluate(_lerpCoeff);

        transform.localPosition = Vector3.Lerp(
            _posRotSource.pos,
            _posRotTarget.pos,
            _lerpCoeff
        );

        transform.localRotation = Quaternion.Lerp(
            _posRotSource.rot,
            _posRotTarget.rot,
            _lerpCoeff
        );
    }

    void _lerpValAdd()
    {
        if(_lerpVal != _lerpMax)
        {
            _lerpVal += 1;
        }
    }

    // local animation
    void _toggleState(int state)
    {
        if (_gunState == state)
        {
            // Set state
            _gunState = State.Ready;

            // Moving from where we are now
            _posRotSource.Set(
                transform.localPosition,
                transform.localRotation
            );

            // To ready
            _posRotTarget = _stateArray[_gunState];
        }
        else
        {
            // Set state
            _gunState = state;

            // Moving from where we are now
            _posRotSource.Set(
                transform.localPosition,
                transform.localRotation
            );
            
            // To reload
            _posRotTarget = _stateArray[_gunState];
        }
        _lerpVal = 0;
    }
}
