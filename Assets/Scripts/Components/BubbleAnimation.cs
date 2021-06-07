using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        // Cria uma instância do Animator do Object
        anim = GetComponent<Animator>();
    }

    public void SetWithdraw(bool withdraw)
    {
        anim.SetBool("isWithdrawing", withdraw);
    }
}
