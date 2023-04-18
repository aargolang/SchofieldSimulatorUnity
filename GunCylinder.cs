using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCylinder : MonoBehaviour
{
    // Constants
    static Quaternion _s_rotationPositionOpen = Quaternion.Euler(0f,305.1f,0f);
    static Quaternion _s_rotationPositionClosed = Quaternion.Euler(0f,0f,0f);
    const float _s_rotationSpeed = 0.4f;

    // Vars
    bool _open;
    Quaternion _rotationPositionTarget;

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = _s_rotationPositionClosed;
        _rotationPositionTarget = _s_rotationPositionClosed;
        _open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            ToggleCylinder();
        }
        // Rotate the hammer towards the target
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation, // From here
            _rotationPositionTarget, // To here
            _s_rotationSpeed         // This many degrees (up to target position)
        );
    }

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
