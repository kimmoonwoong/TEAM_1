using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealer : Unit
{
    [SerializeField] public int attackpower;
    [SerializeField] int stealCoast;

    [SerializeField] public bool isSteal;
    public bool isPlayer;
    GameObject target_place;
    private void Start()
    {
        base.Init();
        //character.sprite = GameManager.resourceManager.LoadSprite("squirrel");
        level = 1;
        Level();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Ability();
    }
    public override void Ability()
    {
        Debug.Log("스틸러 실행됨");
        if (character.flipX == true) // 적군이 아군에게
        {
            var target_unit = gameObject;
            var target_unit2 = gameObject;
            for (int i = 0; i < 2; i++)
            {
                target_place = GameManager.placeManager.getPlaceObject(true, this._currPlace.x, i);
                target_unit = GameManager.unitManager.GetUnit(target_place.GetComponent<PlaceObject>());
                if (target_unit != null)
                {
                    target_unit2 = target_unit;
                    break;
                }
            }
            Debug.Log(target_unit2);
            if (target_unit2.GetComponent<Seed>() != null)
            {
                Seed seed = target_unit2.GetComponent<Seed>();
                GameManager.sceneManager.getPlayer(_currPlace)._currResource += Rip_Seed(seed);
                GameManager.unitManager.isSteal = true;
            }
            else if (target_unit2.GetComponent<Boom>() != null)
            {
                Boom boom = target_unit2.GetComponent<Boom>();
                GameManager.sceneManager.getEnemy(_currPlace)._currHP -= Rip_Boom(boom);
                GameManager.unitManager.isSteal = true;
            }
            else if(target_unit == null)
            {
                GameManager.sceneManager.getEnemy(_currPlace)._currHP -= attackpower;
            }
        }
        else if (character.flipX == false) // 아군이 적군에게
        {
            var target_unit = gameObject;
            var target_unit2 = gameObject;
            for (int i = 0; i < 2; i++) 
            {
                target_place = GameManager.placeManager.getPlaceObject(false, this._currPlace.x, i);
                target_unit = GameManager.unitManager.GetUnit(target_place.GetComponent<PlaceObject>());
                if (target_unit != null)
                {
                    target_unit2 = target_unit;
                    break;
                }
                else if (target_unit == null)
                    target_unit2 = target_unit;
            }
            if (target_unit2 == null)
            {
                GameManager.sceneManager.getEnemy(_currPlace)._currHP -= attackpower;
            }
            else if (target_unit2.GetComponent<Seed>() != null)
            {
                Seed seed = target_unit2.GetComponent<Seed>();
                GameManager.sceneManager.getPlayer(_currPlace)._currResource += Rip_Seed(seed);
                GameManager.unitManager.isSteal = true;
            }
            else if(target_unit2.GetComponent<Boom>() != null)
            {
                Boom boom = target_unit2.GetComponent<Boom>();
                GameManager.sceneManager.getPlayer(_currPlace)._currHP -= Rip_Boom(boom);
            }
        }
    }

    //if (target_unit.layer == LayerMask.NameToLayer("Seed"))
    public void Level()
    {
        switch (level)
        {
            case 1:
                attackpower = 1;
                stealCoast = 1;
                break;
            case 2:
                attackpower = 3;
                stealCoast = 3;
                break;
            case 3:
                attackpower = 5;
                stealCoast = 5;
                break;
        }
    }

    public int Rip_Seed(Seed seed)
    {
        int result;
        result = seed.myresource;
        GameManager.unitManager.DeleteUnit(seed);
        return result;
    }
    public int Rip_Boom(Boom boom)
    {
        int result;
        result = boom.damage;
        GameManager.unitManager.DeleteUnit(boom);
        return result;
    }

}
