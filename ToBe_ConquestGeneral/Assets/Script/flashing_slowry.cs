using UnityEngine;
using UnityEngine.UI;

public class flashing_slowry : MonoBehaviour
{
    private float alpha;
    private SpriteRenderer target;
    [SerializeField] private float cycle = 1;

    void Start()
    {
        //targetにこのオブジェクトのSpriteRendererの情報を格納
        target = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        alpha = Mathf.Cos(Time.time / cycle) / 2 + 0.5f; //０から１の間を行ったり来たりする
        Color color = target.color;                      //targetの色を変数colorに格納
        color.a      = alpha;
        target.color = color; 
        //Debug.Log("aaa" + color);
    }
}

