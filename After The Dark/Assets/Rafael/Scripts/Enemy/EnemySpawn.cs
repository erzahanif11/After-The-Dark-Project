using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public TimeManager Timer;
    public int xPos;
    public int zPos;
    public int enemyCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer = FindAnyObjectByType<TimeManager>();
        EnemyDrop();
    }

    public IEnumerator EnemyDrop()
    {
        enemyCount = 0;
        if (Timer.currentTime >= 22)
        {
            while (enemyCount < 25)
            {
                xPos = Random.Range(-20, 103);
                zPos = Random.Range(52, 117);

                Instantiate(Enemy, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);

                yield return new WaitForSeconds(0f);
                enemyCount++;
            }Debug.Log("25 Enemy Spawned");
        }
        else
        {
            while (enemyCount < 20)
            {
                xPos = Random.Range(-20, 103);
                zPos = Random.Range(52, 117);

                Instantiate(Enemy, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);

                yield return new WaitForSeconds(0f);
                enemyCount++;     
            }Debug.Log("20 Enemy Spawned");
        }
        
        Debug.Log("Enemy spawned");
    }
}
