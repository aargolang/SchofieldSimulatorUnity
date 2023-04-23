using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GunCylinder : MonoBehaviour
{
    // Static constants
    static Quaternion _s_rotationPositionStart = Quaternion.Euler(0f,0f,0f);
    int _cylinderCapacity = 8;

    // references
    [SerializeField]
    public Gun gunRef;

    // Vars
    [SerializeField]
    bool _indexed;
    [SerializeField]
    float _rachetProgress;
    [SerializeField]
    Quaternion _cylinderIndexRotation;
    [SerializeField]
    Quaternion _rotationPositionCurrent;
    [SerializeField]
    Quaternion _rotationPositionNext;



    // Start is called before the first frame update
    void Start()
    {
        _rotationPositionCurrent = _s_rotationPositionStart;
        _cylinderIndexRotation = Quaternion.Euler(
            360f/_cylinderCapacity,
            0f,
            0f
        );
        _rachetProgress = 0.0f;
        _rotationPositionNext = _rotationPositionCurrent * _cylinderIndexRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pushCylinderRachet(float amount)
    {
        if (gunRef.gunState != Gun.State.Reload)
        {
            if(!_indexed)
            {
                if(amount > _rachetProgress)
                {
                    _rachetProgress = amount;
                    transform.localRotation = Quaternion.Lerp(
                        _rotationPositionCurrent,
                        _rotationPositionNext,
                        _rachetProgress
                    );
                }
            if(_rachetProgress == 1.0f)
                {
                    _rachetProgress = 0f;
                    _indexed = true;
                    transform.localRotation = _rotationPositionNext;
                    _rotationPositionCurrent = _rotationPositionNext;
                    _rotationPositionNext *= _cylinderIndexRotation;
                }
            }
            else // _indexed == true
            {
                if (amount == 0f)
                {
                    _indexed = false;
                }
            }
        }
    }

}
