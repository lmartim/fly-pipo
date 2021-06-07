using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private Rigidbody2D _rb;
    private bool isReady = false;
    private Vector2 _newMovement;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isReady)
            _rb.velocity = _newMovement;
    }

    public void TriggerObject(float direction)
    {
        isReady = true;
        _newMovement = new Vector2(direction * 7f, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Remover":
                Destroy(gameObject);
                break;
        }
    }
}
