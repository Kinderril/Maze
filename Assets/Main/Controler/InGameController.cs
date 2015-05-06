
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public SpellBook spellBook;
    public CastRecognizer casteRecognizer;

    void Awake()
    {
        casteRecognizer.Init(this);
    }

    public void Init()
    {

    }

    public void FindSpell(List<Vector2> spell,float width,float height )
    {
        var spellB = spellBook.GetSpell(spell,width,height);
        if (spellB != null)
        {
            Debug.Log("spellB casted " + spellB.name);
        }
    }
}

