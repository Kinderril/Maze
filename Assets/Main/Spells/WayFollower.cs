
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WayFollower : MonoBehaviour
{
    public float speed;
    public List<Vector2> way;
    private bool isMove = false;
    public int currentIndexTarget;
    public float time;
    public float delta;
    public float deltaTime;
    private ParticleSystem pSys;
    public float emitRate = 130;

    public bool IsMove
    {
        get { return isMove; }
        set
        {
            isMove = value;
            pSys.enableEmission = isMove;
            //pSys.emissionRate = isMove ? emitRate : 0;
            //gameObject.SetActive(isMove);

           // pSys.startLifetime = isMove ?2.58f : 0f;

        }
    }

    void Update()
    {
        if (isMove)
        {
            time += Time.deltaTime *  delta;
            deltaTime = Time.deltaTime;
            if (time > 1)
            {
                currentIndexTarget++;
                if (way.Count <= currentIndexTarget)
                {
                    IsMove = false;
                }
                else
                {
                    CountDelta();
                    time = 1 - time;
                    transform.position = Vector2.Lerp(way[currentIndexTarget - 1], way[currentIndexTarget], time);
                }
            }
            else 
            { 
                transform.position = Vector2.Lerp(way[currentIndexTarget - 1], way[currentIndexTarget], time);
            }
            
        }
    }

    private void CountDelta()
    {
        delta = speed / (way[currentIndexTarget - 1] - way[currentIndexTarget]).magnitude;
    }

    public void Init(List<Vector2> way)
    {
        pSys = GetComponent<ParticleSystem>();
        IsMove = false;
        float s = Camera.main.orthographicSize;
        if (way.Count > 1)
        {
            for (int i = 0; i < way.Count; i++)
            {
                way[i] = Camera.main.ScreenToWorldPoint(way[i]);
                //Debug.Log("way " + i + "  :  " + way[i]);
                //way[i] = new Vector2(way[i].x * s * 2 / Screen.width - s , way[i].y * s  * 2/ Screen.height - s );
            }
            pSys.Clear();
            time = 0;
            currentIndexTarget = 1;
            this.way = way;
            transform.position = way[0];
            StartCoroutine(PreCast());
        }
    }

    private IEnumerator PreCast()
    {
        yield return new WaitForSeconds(0.1f);
        CountDelta();
        IsMove = true;
    } 
}
