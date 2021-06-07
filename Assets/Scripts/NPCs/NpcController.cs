using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pointSfx;

    public GameObject[] bubblePrefab;
    private GameObject _bubbleInstance;

    private Rigidbody2D _rb;
    private Vector2 _newMovement;

    private NpcSpawnerController _npcSpawner;
    private GameController _gameController;
    private NpcAnimation _npcAnimation;

    private bool _facingRight = true;

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _npcSpawner = GameObject.Find("NPC Spawner").GetComponent<NpcSpawnerController>();
        _gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        _npcAnimation = GetComponent<NpcAnimation>();
        
        StartCoroutine(MovingAwake());
        StartCoroutine(Chatting(Random.Range(3f, 8f)));
    }

    private void Update()
    {
        _rb.velocity = _newMovement;

        if (_bubbleInstance)
            _bubbleInstance.transform.position = new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Heart":
                _npcSpawner.npcCount--;

                _gameController.score += 10;
                audioSource.PlayOneShot(pointSfx, .4f);

                Destroy(gameObject);
                Destroy(collision.gameObject);

                break;
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    IEnumerator MovingAwake()
    {
        float startDirection = Random.Range(-1f, 1f);
        if (startDirection < 0)
        {
            Flip();
        }
        _newMovement = new Vector2(.1f * 10f * startDirection, _rb.velocity.y);
        _npcAnimation.SetWalking(true);

        yield return new WaitForSeconds(2f);

        _newMovement = new Vector2(0, _rb.velocity.y);
        _npcAnimation.SetWalking(false);

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            float direction = Random.Range(-1f, 1f);
            if (direction == 0f) direction = 1f;
            if ((_facingRight && direction < 0) || (!_facingRight && direction > 0))
            {
                Flip();
            }

            _newMovement = new Vector2(.1f * 10f * direction, _rb.velocity.y);
            _npcAnimation.SetWalking(true);

            yield return new WaitForSeconds(2f);

            _newMovement = new Vector2(0, _rb.velocity.y);
            _npcAnimation.SetWalking(false);
        }
    }

    IEnumerator Chatting(float awaitToStart)
    {

        yield return new WaitForSeconds(awaitToStart);

        while (true)
        {
            _bubbleInstance = Instantiate(bubblePrefab[Random.Range(0, 9)], new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y + 1f, gameObject.transform.position.z), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3f, 8f));

            _bubbleInstance.transform.GetChild(0).gameObject.GetComponent<BubbleController>().WithdrawBubble();

            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }

    private void OnDestroy()
    {
        if (_bubbleInstance)
            Destroy(_bubbleInstance);
    }

}
