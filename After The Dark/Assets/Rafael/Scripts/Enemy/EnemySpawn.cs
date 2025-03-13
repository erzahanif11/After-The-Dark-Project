using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyDrop();
    }

    public IEnumerator EnemyDrop()
    {
        enemyCount = 0;
        while (enemyCount < 20)
        {
            xPos = Random.Range(-185, -70);
            zPos = Random.Range(-45, 20);
            Instantiate(Enemy, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);

            yield return new WaitForSeconds(0f);
            enemyCount++;
        }
        Debug.Log("Enemy spawned");
    }

    //public void ResetEnemyCount()
    //{
    //    enemyCount = 0;
    //}

}
