using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour, UnitInterface, IComparable<Unit>
{
    [SerializeField] bool isUnitClick;
	[SerializeField] public int coast;
	[SerializeField] public int level;
	[SerializeField] public SpriteRenderer character;
    [SerializeField] protected Skill skill;
    [SerializeField] public PlaceObject _currPlace { get; private set;}
    public string _name = "TmpName";
    public int _cost { get; set; }
    public int _speed;
    public Define.UnitCamp _unitCamp;
    public bool valid = true;
    protected float _effectSize = 0.3f;
    protected float _currTime = 0;
    public int stealCount;
    public bool isBackCheck;
    public bool isBoomDamageMiss;
    public bool isSeedStealDamage;
    public bool isBoss;
    public int Turn = 0;
    public int firstTurn = 0;
    public int CompareTo(Unit other) // 스피드 순서로 정렬하기 위한 함수
    {
        if (_speed == other._speed)
            return 0;
        return (_speed < other._speed) ? 1 : -1;
    }
	public void setUnitPos(PlaceObject _place) {
		_currPlace = _place;
        transform.position = _currPlace.transform.position - Vector3.forward;
	}

    protected void Init()
    {
        character = gameObject.GetComponent<SpriteRenderer>();
        if (_currPlace.isPlayerPlace)
        {
            gameObject.tag = "Player";
            character.flipX = false;
        }
        else
        {
            gameObject.tag = "Enemy";
            character.flipX = true;
        }
        if (skill != null)
        {
            Debug.Log("IF문 실행됨");
            skill.unit = this;
        }
        isBackCheck = false;
    }
    public virtual float Ability()
    {
        return 0;
    }
    public virtual void Effect()
    {
        
    }
    void isPlayer()
    {
        if (_currPlace.isPlayerPlace)
        {
            gameObject.tag = "Player";
        }
        else
            gameObject.tag = "Enemy";

    }
 
    void OnMouseDown()
    {
        if (_unitCamp == Define.UnitCamp.enemyUnit)
        {
            Debug.Log("상대방 유닛입니다.");
            GameManager.inputManager.e_CLICKERSTATE = InputManager.E_CLICKERSTATE.STANDBY;
            return;
        }
        if (GameManager.battleManager._isBattle)
        {
            Debug.Log("전투 중엔 유닛을 이동할 수 없습니다.");
            GameManager.inputManager.e_CLICKERSTATE = InputManager.E_CLICKERSTATE.STANDBY;
            return;
        }
        if(isUnitClick)
        {
            isUnitClick = false;
        }
        else
        {
            isUnitClick = true;
        }
        if (GameManager.sceneManager._currMoveCount <= GameManager.sceneManager._maxMoveCount - 1)
        {
            GameManager.inputManager.e_CLICKERSTATE = InputManager.E_CLICKERSTATE.MOVE;
        }
        GameManager.inputManager._currSelectedUnit = this;
    }
	public void clickfunc() {
    }

	public bool checkPos(PlaceObject place) {
        if (place == _currPlace)
        {
            return true;
        }
		return false;
	}

    public void setSprite()
    {

    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public IEnumerator CoAttackedOrUsed(Unit target, float time)
    {
        target.valid = false;
        GameManager.unitManager.DeleteUnit(target);
        yield return new WaitForSeconds(time);
        target.character.enabled = false;
    }
}