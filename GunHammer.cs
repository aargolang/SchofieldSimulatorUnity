using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHammer : MonoBehaviour
{
    [SerializeField]
    int _hammerDirectionFlags;
    [SerializeField]
    static int _localHammerFlag;

    // lerp vars
    [SerializeField]
    int _lerpState; // TODO: lerp state needs to have min/max limits sure
    // [SerializeField] // TODO: do this later if we want to use this to control speed
    // int _lerpStep;
    [SerializeField]
    int _interpolationMin;
    [SerializeField]
    int _interpolationMax;
    [SerializeField]
    float _interpolationCoefficient;
    [SerializeField]
    float _interpolationDuration = 0.9f;
    // Vector3 _cockedPosition = new Vector3(0f,74.2699966f,0f); // TODO: find actual value
    // Vector3 _decockedPosition = new Vector3(0f,0f,0f); // TOOD: find actual value
    [SerializeField]
    Quaternion _cockedPosition = Quaternion.Euler(0f,74.2699966f,0f); // TODO: find actual value
    [SerializeField]
    Quaternion _decockedPosition = Quaternion.Euler(0f,0f,0f); // TOOD: find actual value

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = _decockedPosition;
        _hammerDirectionFlags = 0;
        _localHammerFlag = 0x1;

        _interpolationMax = (int)(1/Time.deltaTime * _interpolationDuration);
        _interpolationMin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            setHammerDirectionFlag(_localHammerFlag);
        }
        if(Input.GetKeyUp("f"))
        {
            clearHammerDirectionFlag(_localHammerFlag);
        }

        _processLerpState();
    }

    void _processLerpState()
    {
        if(_hammerDirectionFlags!=0)
        {
            _lerpStateAdd();
        }
        else
        {
            _lerpStateSubtract();
        }

        // lerpState
        _interpolationCoefficient = ((float)_lerpState/_interpolationMax);
        transform.localRotation = Quaternion.Lerp(_decockedPosition, _cockedPosition, _interpolationCoefficient);
    }

    void _lerpStateAdd()
    {
        if(_lerpState != _interpolationMax)
        {
            _lerpState += 1;
        }
    }

    void _lerpStateSubtract()
    {
        if(_lerpState != _interpolationMin)
        {
            _lerpState -= 1;
        }
    }

    // Public Methods

    // clear a hammer direction flag
    public void clearHammerDirectionFlag(int flag)
    {
        // 
        _hammerDirectionFlags &= ~flag;
    }

    // set a hammer direction flag
    public void setHammerDirectionFlag(int flag)
    {
        //
        _hammerDirectionFlags |= flag;
    }

    // void _updateLerpStep()
    // {
    //     if(_hammerDirectionFlags!=0)
    //     {
    //         _lerpStep = 1;
    //     }
    //     else
    //     {
    //         _lerpStep = -1;
    //     }
    // }
}
