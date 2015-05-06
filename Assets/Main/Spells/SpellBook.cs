
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellBook : MonoBehaviour
{

    public List<Spell> spells;
    public List<Vector2> lastSpell; 

    public SpellBook()
    {
        
    }

    public Spell GetSpell(List<Vector2> spell,float width,float height)
    {
        lastSpell = spell;
        return spells.FirstOrDefault(spell1 => spell1.Check(spell, width,height));
    }
}

