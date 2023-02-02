using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int numOfPlayer = 1;//ƒvƒŒƒCƒ„[‚Ì”

    [SerializeField] int numOfEnemy =10;//“G‚Ì”
    [SerializeField] GameObject RedEnemy;
    [SerializeField] GameObject BlueEnemy;
    [SerializeField] GameObject GreenEnemy;

    GameObject[] enemyOrder;//“G‚Ì•À‚Ñ
    Transform tr_enemies;

    // Start is called before the first frame update
    void Awake()
    {
        var enemies = GameObject.Find("Enemies");
        tr_enemies = enemies.transform;
        EnemyStore();
        EnemyClone();


    }

    //“G‚Ì•À‚Ñ‚ğGameObject‚ÅŠi”[‚·‚éŠÖ”
    void EnemyStore()
    {
        int enemyType; //“G‚Ìí—Ş@0:Ô 1:Â 2:—Î
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

    //“G‚ğƒNƒ[ƒ“‚·‚éŠÖ”
    void EnemyClone()
    {
        for(int i = 0; i < numOfEnemy; i++)
        {
            Instantiate(enemyOrder[i], new Vector2(5f + (3f * i), 0f), Quaternion.identity,tr_enemies);
        }
    }
}
