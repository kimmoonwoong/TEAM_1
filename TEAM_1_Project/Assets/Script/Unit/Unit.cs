using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, UnitInterface
{
    [SerializeField] bool isUnitClick;
	[SerializeField] int coast;
	[SerializeField] int level;
	[SerializeField] public SpriteRenderer character;

    [SerializeField] public PlaceObject _currPlace { get; private set;}

	public void setUnitPos(PlaceObject _place) {
		_currPlace = _place;
        transform.position = _currPlace.transform.position - Vector3.forward;
	}

    private void Start()
    {
        character = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if(isUnitClick)
        {
            isUnitClick = false;
        }
        else
        {
            isUnitClick = true;
        }
        GameManager.GetInstance().inputManager.SetClickerState((int)InputManager.E_CLICKERSTATE.MOVE);
        GameManager.GetInstance().unitManager._currSelectedUnit = this;
    }
	public void clickfunc() {
    }

	public bool checkPos(PlaceObject place) {
		if(place == _currPlace)
			return true;
		return false;
	}

}
