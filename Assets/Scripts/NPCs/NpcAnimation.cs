using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        // Cria uma instância do Animator do Object
        anim = GetComponent<Animator>();
    }

    public void SetWalking(bool walking)
    {
        anim.SetBool("isWalking", walking);
    }
}
