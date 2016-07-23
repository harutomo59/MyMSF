using UnityEngine;
using System.Collections;

/**
 * status of the unit
 * 戦闘中のWT値は使わなくなる（ActionStackで代用する）
 * WT以外のHP等は使う予定
 */
public class UnitStatus : MonoBehaviour {

	int unitId;  //inBattleScene

	int WT;
	public int WTmax = 400;

	int HP;
	public int HPmax = 100;
	int MP;
	public int MPmax = 100;

	public int move = 5;
	public int jump = 2;
	string moveType = "normal";

	string state = "normal";

	Weapon mainWeaponR;
	Weapon mainWeaponL;
	Weapon subWeaponR;
	Weapon subWeaponL;


	// Use this for initialization
	void Start () {
		WT = WTmax;
		HP = HPmax;
		MP = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getHP(){
		return this.HP;
	}
	public int getMP(){
		return this.MP;
	}

	/**
	 * reduce now HP value.
	 * and if damaged unit is dead, return TRUE.
	 */
	public bool dmgHP(int dmg){
		this.HP -= dmg;

		if (this.HP <= 0) {
			return true;
		} else {
			return false;
		}
	}

	/**
	 * increase unit's HP
	 */
	public void healHP(int value){
		this.HP += value;
	}

	/**
	 * 
	 */
	public void dmgMP(int value){
		this.MP -= value;
	}
	public void healMP(int value){
		this.MP += value;
	}

	/**
	 * get the "unitId" in the battle scene
	 */
	public int getUnitId(){
		return this.unitId;
	}

	/**
	 * set the "unitId" in the battle scene
	 */
	public void setUnitId(int unitId){
		this.unitId = unitId;
	}

	/**
	 * get the Now WT Value
	 */
	public int getWT(){
		return this.WT;
	}

	/**
	 * get the Max WT Value
	 */
	public int getWTmax(){
		return this.WTmax;
	}

	public void setWT(int WT){
		this.WT = WT;
	}

	/**
	 * WT値をWTmaxのx%に設定する
	 * 主にユニット行動後に.
	 */
	public void resetWT(float x){
		this.WT = (int)(WTmax * (x / 100));
	}

	/**
	 * WTmaxのx%の値を計算し返す.
	 * 行動後などWT値を変化させたいときに.
	 */
	public int calcWT(float x){
		return (int)(WTmax * (x / 100));
	}

	/**
	 * WT値をvalue減らし
	 * 0になればtrueを返す
	 * 残っていればfalse
	 */
	public bool reduceWT(int value){
		this.WT -= value;
		return this.WT == 0;
	}

	/**
	 * HP値をvalue減らし
	 * 0以下になればtrueを返す
	 * 残っていればfalse
	 */
	public bool reduceHP(int value){
		this.HP -= value;
		return this.HP <= 0;
	}

	/**
	 * MP値をvalue減らす
	 */
	public void reduceMP(int value){
		this.MP -= value;
	}
}
