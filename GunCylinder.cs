using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCylinder : MonoBehaviour
{
    Animator _animator;
    bool _open;
    // Start is called before the first frame update
    void Start()
    {
        // _open = false;
        // _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleCylinder()
    {
        if(!_open)
        {
            _animator.Play("TrunkOpen");
        }
        else
        {
            _animator.Play("TrunkClose");
        }
        _open = !_open;
    }
}
