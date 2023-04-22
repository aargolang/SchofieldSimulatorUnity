using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHammer : MonoBehaviour
{
    // Static "Constants"
    static Quaternion _s_rotationPositionCocked = Quaternion.Euler(0f,74.0f,0f);
    static Quaternion _s_rotationPositionDecocked = Quaternion.Euler(0f,0f,0f);
    static Quaternion _s_rotationPositionMax = Quaternion.Euler(0f,85.0f,0f);
    const float _s_rotationSpeedNormal = 1.5f;
    const float _s_rotationSpeedShoot = 10.0f;
    const int _s_localHammerFlag = 0x1;
    const int _s_LMB = 0; // Left Mouse Button
    const int _s_RMB = 1; // Right Mouse Button

    // Vars
    [SerializeField]
    Quaternion _rotationPositionTarget;
    [SerializeField]
    Quaternion _rotationPositionCurrentMin;
    [SerializeField]
    Quaternion _rotationPositionCurrentMax;
    [SerializeField]
    float _rotationSpeedCurrent;
    [SerializeField]
    int _hammerDirectionFlags;
    [SerializeField]
    bool _cocked;
    [SerializeField]
    bool _firing;
    [SerializeField]
    bool _pullingTrigger;

    

    // Start is called before the first frame update
    void Start()
    {
        _rotationPositionCurrentMin = _s_rotationPositionDecocked;
        _rotationPositionCurrentMax = _s_rotationPositionMax;
        transform.localRotation = _rotationPositionCurrentMin;
        _rotationPositionTarget = _rotationPositionCurrentMin;
        _rotationSpeedCurrent = _s_rotationSpeedNormal;
        _hammerDirectionFlags = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            _hammerPull();
        }
        if(Input.GetKeyUp("f"))
        {
            _hammerRelease();
        }
        if(Input.GetMouseButtonDown(_s_LMB)) // LMB down
        {
            _triggerPull();
        }
        if(Input.GetMouseButtonUp(_s_LMB)) // LMB up
        {
            _triggerRelease();
        }

        _processRotateState();

        // Rotate the hammer towards the target
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation, // From here
            _rotationPositionTarget, // To here
            _rotationSpeedCurrent    // This many degrees (up to target position)
        );
    }

    void _triggerPull()
    {
        _pullingTrigger = true;
        fire();
    }

    void _triggerRelease()
    {
        _pullingTrigger = false;
    }

    void _hammerPull()
    {
        if(!_firing)
        {
            setHammerDirectionFlag(_s_localHammerFlag);
            _rotationPositionTarget = _rotationPositionCurrentMax;
        }
    }

    void _hammerRelease()
    {
        clearHammerDirectionFlag(_s_localHammerFlag);
        if(_hammerDirectionFlags==0)
        {
            _rotationPositionTarget = _rotationPositionCurrentMin;
        }
    }

    // Process firing the gun
    public void fire()
    {
        // fire
        if(_cocked && 
           _hammerDirectionFlags == 0)
        {
            _rotationPositionCurrentMin = _s_rotationPositionDecocked;
            _rotationPositionTarget = _s_rotationPositionDecocked;
            _firing = true;
            _rotationSpeedCurrent = _s_rotationSpeedShoot;
        }
        _cocked = false;
    }

    // Process rotation states and rotation for this update
    void _processRotateState()
    {
        // Check if the hammer catch passes the trigger latch, and we aren't pulling the trigger
        if (!_cocked &&
            !_pullingTrigger && 
            transform.localRotation.y > _s_rotationPositionCocked.y)
        {
            _rotationPositionCurrentMin = _s_rotationPositionCocked;
            _cocked = true;
        }

        // if holding hammer and cocked, set minimum position to decocked
        // for safely decocking hammer
        if (_hammerDirectionFlags != 0 &&
            _pullingTrigger)
        {
            _rotationPositionCurrentMin = _s_rotationPositionDecocked;
        }

        // Check that the hammer has struck the primer (or empty chamber)
        if (transform.localRotation.y == _s_rotationPositionDecocked.y &&
            _rotationSpeedCurrent != _s_rotationSpeedNormal)
        {
            _rotationSpeedCurrent = _s_rotationSpeedNormal;
            _firing = false;
            // TODO: shoot the bullet
        }
    }

    // Public Methods

    // set a hammer direction flag
    public int setHammerDirectionFlag(int flag)
    {
        _hammerDirectionFlags |= flag;
        return _hammerDirectionFlags;
    }

    // clear a hammer direction flag
    public int clearHammerDirectionFlag(int flag)
    {
        _hammerDirectionFlags &= ~flag;
        return _hammerDirectionFlags;
    }
}
