using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Unit
{
    public int damage;
    [SerializeField] int growth;
    [SerializeField] int maxDamage;
    public override void Ability()
    {
        if (damage <= maxDamage)
        {
            damage += growth;
        }
    }
    public override void Effect()
    {
        
    }
    private void Start()
    {
        base.Init();
        Level();
    }
    void Level()
    {
        switch (level)
        {
            case 1:
                damage = 3;
                growth = 0;
                break;
            case 2:
                damage = 5;
                growth = 1;
                break;
            case 3:
                damage = 7;
                growth = 2;
                break;
        }
    }
}
