
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public const float spellDiffer = 10f;

    public List<Vector2> coords;
    public bool isChecked = false;

    void Awake()
    {
        //todo scale coordinates
        
    }

    public bool Check(List<Vector2> spell, float width, float height)
    {
        if (spell.Count != coords.Count)
        {
            isChecked = true;
            return false;
        }
        for (int i = 0; i < spell.Count; i++)
        {
            float dist = (spell[i] - coords[i]).sqrMagnitude;
            if (dist > spellDiffer)
            {
                Debug.Log("Bad point " +i+ "  " + spell[i] + "  " + dist);
                isChecked = true;
                return false;
            }
        }
        return true;
    }
}

