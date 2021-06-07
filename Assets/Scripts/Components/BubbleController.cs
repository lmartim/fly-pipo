using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{

    private BubbleAnimation _bubbleAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _bubbleAnimation = gameObject.GetComponent<BubbleAnimation>();
    }

    public void WithdrawBubble()
    {
        _bubbleAnimation.SetWithdraw(true);
    }
}
