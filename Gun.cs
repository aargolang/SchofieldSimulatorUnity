using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Components
    GunHammer _hammer;
    GunCylinder _cylinder;
    Animator _animator;

    // Gun state
    bool _gunCylinderOpen;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Gun.cs start()");
        _gunCylinderOpen = false;
        _animator = gameObject.GetComponent<Animator>();
        // _hammer = GetComponentInChildren<GunHammer>();
        _cylinder = GetComponentInChildren<GunCylinder>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown("t"))
        // {
        //     _ToggleHammer();
        // }
        // if(Input.GetKeyDown("r"))
        // {
        //     _ToggleCylinder();
        // }
    }

    void _ToggleHammer()
    {
        _hammer.ToggleHammer();
    }

    void _ToggleCylinder()
    {
        _ToggleGunCylinder();
        _cylinder.ToggleCylinder();
    }

    // local animation
    void _ToggleGunCylinder()
    {
        if(!_gunCylinderOpen)
        {
            _animator.Play("GunCylinderOpen");
        }
        else
        {
            _animator.Play("GunCylinderClose");
        }
        _gunCylinderOpen = !_gunCylinderOpen;
    }
}
