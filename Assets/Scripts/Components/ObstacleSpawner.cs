using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject obstacle;
    public List<Sprite> spriteList;
    public float direction = .6f;

    private int obstacleCount = 0;
    private bool _isSpawnDisable = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacle(Random.Range(0f, 3f)));
    }

    private void Update()
    {
        if (_isSpawnDisable && obstacleCount <= 3)
            StartCoroutine(SpawnObstacle());

        if (obstacleCount < 0) obstacleCount = 0;
    }

    public IEnumerator SpawnObstacle(float summonStart = 0f)
    {
        yield return new WaitForSeconds(summonStart);

        while (obstacleCount <= 3)
        {

            int spriteToRender = Random.Range(0, 2);
            GameObject spawnedObject = Instantiate(
                obstacle,
                new Vector3(gameObject.transform.position.x, Random.Range(-1.5f, 4.35f), 0f),
                Quaternion.identity
            );
            spawnedObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteToRender];
            spawnedObject.GetComponent<ObstacleController>().TriggerObject(direction * Random.Range(1f, 1.7f));

            yield return new WaitForSeconds(3f);
        }

        _isSpawnDisable = true;
    }

    private void OnDestroy()
    {
        obstacleCount = 0;
    }
}
