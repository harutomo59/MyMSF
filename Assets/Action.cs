using UnityEngine;
using System.Collections;

/**
 * this class will be used by "BattleController.cs".
 */
public class Action {

	int actionId;
	public int WT;
	string type; //unit,action,event

	int unitId; //type==unit || type==action

	int team; //type==unit || type==action

	/**
	 * actionId - 戦闘におけるID
	 * WT - 行動までの時刻
	 * type - 種類.  "unit","action","event"
	 * タイプに合わせて追加で set<i>Type</i>Infoも設定せよ.
	 */
	public Action(int actionId, int WT, string type){
		this.actionId = actionId;
		this.WT = WT;
		this.type = type;
	}

	public int getActionId(){
		return this.actionId;
	}

	public int getWT(){
		return this.WT;
	}

	public string getType(){
		return this.type;
	}

	public int getTeam(){
		return this.team;
	}

	public int getUnitId(){
		return this.unitId;
	}

	/**
	 * Unitタイプの追加情報
	 */
	public void setUnitInfo(int team, int unitId){
		this.team = team;
		this.unitId = unitId;
	}

	/**
	 * Actionタイプの追加情報
	 */
	public void setActionInfo(int team, int unitId){
		this.team = team;
		this.unitId = unitId;
	}

	/**
	 * Eventタイプの追加情報
	 */
	public void setEventInfo(){

	}

	/**
	 * reduce WT value
	 */
	public void reduceWT(int value){
		this.WT -= value;
	}

	/**
	 * increase WT value
	 */
	public void increaseWT(int value){
		this.WT += value;
	}

	/**
	 * set WT value
	 */
	public void setWT(int value){
		this.WT = value;
	}

}
