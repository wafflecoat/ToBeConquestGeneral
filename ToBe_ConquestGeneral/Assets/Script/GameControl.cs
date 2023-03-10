using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int numOfPlayer = 1;//vC[Μ

    [SerializeField] int numOfEnemy =10;//GΜ
    [SerializeField] GameObject RedEnemy;
    [SerializeField] GameObject BlueEnemy;
    [SerializeField] GameObject GreenEnemy;

    GameObject[] enemyOrder;//GΜΐΡ
    Transform tr_enemies;

    // Start is called before the first frame update
    void Awake()
    {
        var enemies = GameObject.Find("Enemies");
        tr_enemies = enemies.transform;
        EnemyStore();
        EnemyClone();


    }

    //GΜΐΡπGameObjectΕi[·ιΦ
    void EnemyStore()
    {
        int enemyType; //GΜνή@0:Τ 1:Β 2:Ξ
        enemyOrder = new GameObject[numOfEnemy];
        for (int i = 0; i < numOfEnemy; i++)
        {
            enemyType = Random.Range(0, 3);
            if (enemyType == 0)
            {
                enemyOrder[i] = RedEnemy;
            }
            else if (enemyType == 1)
            {
                enemyOrder[i] = BlueEnemy;
            }
            else if (enemyType == 2)
            {
                enemyOrder[i] = GreenEnemy;
            }
            Debug.Log($"{enemyOrder[i]}");
        }
    }

    //GπN[·ιΦ
    void EnemyClone()
    {
        for(int i = 0; i < numOfEnemy; i++)
        {
            Instantiate(enemyOrder[i], new Vector2(5f + (3f * i), 0f), Quaternion.identity,tr_enemies);
        }
    }
}
