using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CastRecognizer : MonoBehaviour ,IPointerDownHandler,IPointerUpHandler
{
    List<Vector2> deltaList = new List<Vector2>();
    List<Vector2> curvesList = new List<Vector2>();
    private Vector2 cur;
    private Vector2 prevPoint;
    public GameObject pointGO;
    Quaternion q = Quaternion.identity;
    public WayFollower wayFollower;
    public float offset = 11f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrag(BaseEventData eventData)
    {
        var ped = eventData as PointerEventData;
        deltaList.Add(ped.delta);
        cur += ped.delta;
        float angel = Vector2.Angle(prevPoint, ped.delta);
        //Debug.Log("drag>>>> " + ped.delta + " angel: " + angel);
        if (angel > 22f)
        {
            bool shaalCheck = (curvesList.Count == 0);

            if (shaalCheck || (curvesList[curvesList.Count - 1] - ped.position).sqrMagnitude > offset)
            {
                curvesList.Add(ped.position);
                var g = Instantiate(pointGO, ped.position, q) as GameObject;
                g.transform.SetParent(transform);
            }
        }
        prevPoint = ped.delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        curvesList = new List<Vector2>();
        cur = eventData.position;
        //Debug.Log("Stars drag " + eventData.position + "  clear:" + transform.childCount);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        curvesList.Add(eventData.position);

        Debug.Log("Ends drag " + curvesList.Count + "   by delta " + cur);
        wayFollower.Init(curvesList);
    }
}
