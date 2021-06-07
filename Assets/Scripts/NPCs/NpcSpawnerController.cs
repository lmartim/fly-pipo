using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnerController : MonoBehaviour
{
    public List<GameObject> spriteList;
    public AudioSource audioSource;

    public int npcCount = 0;

    private bool _isSpawnDisable = false;
    private int _maxNpcCount = 8;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNpc(3f));
    }

    private void Update()
    {
        if (_isSpawnDisable && npcCount <= _maxNpcCount)
            StartCoroutine(SpawnNpc());

        if (npcCount < 0) npcCount = 0;
    }

    public IEnumerator SpawnNpc(float summonStart = 0f)
    {

        yield return new WaitForSeconds(summonStart);

        while (npcCount <= _maxNpcCount)
        {
            int spriteToRender = Random.Range(0, 14);
            GameObject npcSpawned = Instantiate(spriteList[spriteToRender], new Vector3(Random.Range(-14f, 12f), -4.45f, 0f), Quaternion.identity);
            npcSpawned.GetComponent<NpcController>().audioSource = audioSource;

            npcCount++;

            yield return new WaitForSeconds(3f);
        }

        _isSpawnDisable = true;
    }

    private void OnDestroy()
    {
        npcCount = 0;
    }
}
