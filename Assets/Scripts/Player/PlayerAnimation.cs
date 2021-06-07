using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject playerContainer;
    private Animator _anim;

    private void Awake()
    {

        _anim = playerContainer.GetComponent<Animator>();
    }
    public void SetInteraction(bool acting)
    {
        _anim.SetBool("isActing", acting);
    }

    public void SetTrigger(string trigger)
    {
        _anim.SetTrigger(trigger);
    }
}
