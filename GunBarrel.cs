using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBarrel : MonoBehaviour
{
    // Constants
    static Quaternion _s_rotationPositionOpen = Quaternion.Euler(0f,305.1f,0f);
    static Quaternion _s_rotationPositionClosed = Quaternion.Euler(0f,0f,0f);
    const float _s_rotationSpeed = 0.4f;

    // References
    public Gun gunRef;
    public AnimationCurve anim;

    // Vars
    bool _open;
    Quaternion _rotationPositionTarget;
    Quaternion _rotationPositionCurrent;
    int _gunState;


    // lerp vars
    [SerializeField]
    float _lerpDuration; 
    [SerializeField]
    float _lerpCoeff;
    [SerializeField]
    float _lerpVal;
    [SerializeField]
    int _lerpMin;
    [SerializeField]
    int _lerpMax;
    [SerializeField]
    bool _lerpTransitioning;

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = _s_rotationPositionClosed;
        _rotationPositionTarget = _s_rotationPositionClosed;
        _rotationPositionCurrent = _s_rotationPositionClosed;
        _gunState = Gun.State.Ready;
        _open = false;
        _lerpDuration = 3.0f;
        _lerpMax = (int)(1/Time.deltaTime * _lerpDuration);
        _lerpVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _processLerpState();
    }

    void _lerpValAdd()
    {
        if(_lerpVal != _lerpMax)
        {
            _lerpVal += 1;
        }
    }

    void _processLerpState()
    {
        if (transform.localRotation != _rotationPositionTarget)
        {
            _lerpValAdd();

            _lerpCoeff = ((float)_lerpVal/_lerpMax);
            _lerpCoeff = anim.Evaluate(_lerpCoeff);

            transform.localRotation = Quaternion.Lerp(
                _rotationPositionCurrent,
                _rotationPositionTarget,
                _lerpCoeff
            );
        }
    }

    public void updateState(int state)
    {
        
        if(_gunState != state)
        {
            _gunState = state;
            _lerpVal = 0;
            _rotationPositionCurrent = transform.localRotation;
            if(_gunState == Gun.State.Reload)
            {
                _rotationPositionTarget = _s_rotationPositionOpen;
            }
            else 
            {
                _rotationPositionTarget = _s_rotationPositionClosed;
            }
        }
    }

    // public void OpenCylinder()
    // {
    //     _rotationPositionTarget = _s_rotationPositionOpen;
    //     _open = true;
    // }

    // public void CloseCylinder()
    // {
    //     _rotationPositionTarget = _s_rotationPositionClosed;
    //     _open = false;
    // }

    public void ToggleCylinder()
    {
        if(!_open)
        {
            _rotationPositionTarget = _s_rotationPositionOpen;
        }
        else
        {
            _rotationPositionTarget = _s_rotationPositionClosed;
        }
        _open = !_open;
    }
}
