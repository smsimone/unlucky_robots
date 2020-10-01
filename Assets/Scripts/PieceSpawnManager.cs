using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceSpawnManager : MonoBehaviour
{
    public List<GameObject> pieces;
    public float spawnRate = 1.5f;

    public bool isGameActive = true;

    private System.Random rnd;

    void Start()
    {
        rnd = new System.Random();
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = rnd.Next(pieces.Count);
            GameObject item = pieces[index];
            Instantiate(item, new Vector3(item.transform.position.x, item.transform.position.y, -11), item.transform.rotation);
        }
    }
}
