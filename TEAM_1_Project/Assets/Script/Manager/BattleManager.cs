using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    float _battleTime = 1.0f;
    int _basicallyProvideResource = 1;

    public bool _isBattle = false;
    public void Init()
    {
        
    }
    public void OnUpdate()
    {

    }
    public void StartBattle()
    {
        if (_isBattle)
        {
            Debug.Log("이미 전투중 입니다.");
            return;
        }
        _isBattle = true;
        StartCoroutine("BattleCoroutine");
    }
    IEnumerator BattleCoroutine()
    {
        BattleLogic();
        for(int i = 0; i < GameManager.unitManager.UnitList.Count; i++)
        {
            yield return new WaitForSeconds(GameManager.unitManager.doBattle(i));
            GameManager.uiManager.ChangeInfoBar();
        }
        GameManager.unitManager.RemoveUnits();
        EndBattle();
    }
    void BattleLogic()
    {
        GameManager.inputManager.e_CLICKERSTATE = InputManager.E_CLICKERSTATE.STANDBY;
        GameManager.placeManager.display(false);
        GameManager.uiManager.ChangePlaceToBattle();
        GameManager.unitManager.UnitList.Sort();
        Debug.Log("전투 시작!");
    }
    void EndBattle()
    {
        _isBattle = false;
        Debug.Log("전투 끝!");

        ref int _turn = ref GameObject.Find("UI_Turn_End_Button").GetComponentInChildren<UI_Turn_End_Button>().turn;
        _turn++;
        ProvideResource();
        UpdateUI(ref _turn);
        GameManager.sceneManager._currMoveCount = 0;
    }
    void UpdateUI(ref int _turn)
    {
        GameManager.uiManager.changeTurn(_turn);
        GameManager.uiManager.ChangeBattleToPlace();
        GameManager.uiManager.ChangeInfoBar();
    }
    void ProvideResource()
    {
        Player player = GameManager.sceneManager.getPlayer(true);
        Player enemy = GameManager.sceneManager.getPlayer(false);
        player._currResource = Mathf.Clamp(player._currResource + _basicallyProvideResource, 0, player._maxResource);
        enemy._currResource = Mathf.Clamp(enemy._currResource + _basicallyProvideResource, 0, enemy._maxResource);
    }
}
