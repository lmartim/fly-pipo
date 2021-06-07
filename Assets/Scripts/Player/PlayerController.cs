using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameController _gameController;
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimation _playerAnimation;
    private Rigidbody2D _rb;

    public GameObject transitioner;

    public GameObject heartPrefab;
    public GameObject healthBar;
    public GameObject startCounterText;
    public GameObject gameOverText;
    public GameObject obstracleSpawner;
    public GameObject mainCamera;

    public AudioSource audioSource;
    public AudioClip impulseSfx;
    public AudioClip hitSfx;
    public AudioClip countSfx;

    private int _maxHp = 6;
    private int _hp;

    private bool _playerInvincible = false;
    public bool facingRight = true;
    public float hSpeed;
    public float vSpeed;

    private Vector2 _newMovement;
    private bool _isActionBlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        _isActionBlocked = true;

        _hp = _maxHp;
        healthBar.GetComponent<HealthController>().SetMaxHealth(_hp);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();

        _gameController = GameObject.Find("Game Controller").GetComponent<GameController>();

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
        {
            StartCoroutine(GameOver());
        }

        if (_isActionBlocked) return;
    }

    private void FixedUpdate()
    {
        if (_isActionBlocked) return;

        _rb.velocity = _newMovement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                if (!_playerInvincible)
                {
                    _hp--;
                    _playerInvincible = true;
                    healthBar.GetComponent<HealthController>().SetHealth(_hp);
                    audioSource.PlayOneShot(hitSfx, .4f);
                    StartCoroutine(Suffering());
                }

                Destroy(collision.gameObject);
                break;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Moving(float direction)
    {
        if (_isActionBlocked) return;

        _newMovement = new Vector2(direction * hSpeed, _rb.velocity.y);
        
        if ((facingRight && direction < 0) || (!facingRight && direction > 0))
        {
            Flip();
        }
    }

    public void Fly()
    {
        if (_isActionBlocked) return;

        _rb.AddForce(new Vector2(0f, vSpeed));
        audioSource.PlayOneShot(impulseSfx, .4f);
    }

    public void StartActionCoroutine()
    {
        StartCoroutine(Action());
    }

    public IEnumerator Action()
    {
        if (_isActionBlocked) yield break;

        _playerAnimation.SetInteraction(true);

        yield return new WaitForSeconds(.2f);

        float xDiff = .4f;
        if (facingRight)
            xDiff *= 1f;
        else
            xDiff *= -1f;

        Instantiate(heartPrefab, new Vector3(gameObject.transform.position.x+xDiff, gameObject.transform.position.y-1.7f, gameObject.transform.position.z), Quaternion.identity);

        _isActionBlocked = true;

        yield return new WaitForSeconds(.3f);

        _isActionBlocked = false;
        _playerAnimation.SetInteraction(false);
    }

    IEnumerator Suffering()
    {
        for (float i = 0f; i < 1; i += .1f)
        {
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }

        _playerInvincible = false;
    }

    IEnumerator GameOver()
    {
        _isActionBlocked = true;
        gameOverText.SetActive(true);
        _playerAnimation.SetTrigger("isDefeated");

        StateController.totalScore = _gameController.score;

        yield return new WaitForSeconds(2f);

        transitioner.GetComponent<SceneController>().GoToSceneOnClick("Game Over Scene");

    }

    IEnumerator StartGame()
    {
        int count = 3;

        while (count > 0)
        {
            count--;

            yield return new WaitForSeconds(1f);

            startCounterText.GetComponent<Text>().text = count.ToString();
            audioSource.PlayOneShot(countSfx, .1f);
        }

        startCounterText.GetComponent<Text>().text = "Voa!!";

        _isActionBlocked = false;
        obstracleSpawner.SetActive(true);

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        _rb.AddForce(new Vector2(0f, 0f));

        mainCamera.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1f);

        startCounterText.SetActive(false);
    }
}
