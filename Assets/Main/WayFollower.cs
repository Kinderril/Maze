
using System.Collections.Generic;
using UnityEngine;


public class WayFollower : MonoBehaviour
{
    public float speed;
    public List<Vector2> way;
    public bool isMove = false;
    public int currentIndexTarget;
    public float time;
    public float delta;
    public float deltaTime;
    private ParticleSystem pSys;

    void Update()
    {
        if (isMove)
        {
            time += Time.deltaTime *   delta;
            deltaTime = Time.deltaTime;
            if (time > 1)
            {
                currentIndexTarget++;
                if (way.Count <= currentIndexTarget)
                {
                    isMove = false;
                    Debug.Log("END MOVE");
                    pSys.enableEmission = false;
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
        delta = speed/(way[currentIndexTarget - 1] - way[currentIndexTarget]).magnitude;

    }

    public void Init(List<Vector2> way)
    {
        pSys = GetComponent<ParticleSystem>();
        float s = Camera.main.orthographicSize;
        if (way.Count > 1)
        {
            for (int i = 0; i < way.Count; i++)
            {

                //Debug.Log("way " + i + "  :  " + way[i]);
                way[i] = new Vector2(way[i].x * s * 2 / Screen.width - s , way[i].y * s  * 2/ Screen.height - s );
            }
            pSys.Clear();
            time = 0;
            currentIndexTarget = 1;
            gameObject.SetActive(true);
            transform.position = way[0];
            pSys.enableEmission = true;
            this.way = way;
            CountDelta();
            isMove = true;
        }
    }
}
