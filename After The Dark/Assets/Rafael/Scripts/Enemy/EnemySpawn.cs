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
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 30)
        {
            xPos = Random.Range(-40, 40);
            zPos = Random.Range(-40, 40);
            Instantiate(Enemy,new Vector3(xPos,1,zPos), Quaternion.identity);

            yield return new WaitForSeconds(0.1f);
            enemyCount++;
        }
    }

}
