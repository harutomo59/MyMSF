using UnityEngine;
using System.Collections;

/**
 * 一つの武器を表すクラス
 */
public class Weapon : EquipmentItem{

	int range;
	byte needHand;//片手なら1,両手なら2

	public Weapon(){
		this.setFullInfo ("素手", "武器", "拳", 0, 0, 0, 0, 0, 5, 5, 1, 1);
	}
	public Weapon(string name, string category, string type, int weight, int atk, int matk, int def, int mdef,
	              float hitRate, float avoidRate, int range, byte needHand){
		this.setFullInfo (name, category, type, weight, atk, matk, def, mdef, hitRate, avoidRate, range, needHand);
	}

	public void setFullInfo(string name, string category, string type, int weight, int atk, int matk, int def, int mdef,
	                        float hitRate, float avoidRate, int range, byte needHand){
		this.setName (name);
		this.setCategory (category);
		this.setType (type);
		this.setWeight (weight);
		this.setAtk (atk);
		this.setMAtk (matk);
		this.setDef (def);
		this.setMDef (mdef);
		this.setHitRate (hitRate);
		this.setAvoidRate (avoidRate);
		this.setRange (range);
		this.setNeedHand (needHand);
	}

	public void setRange(int range){
		this.range = range;
	}
	public int getRange(){
		return this.range;
	}
	public void setNeedHand(byte needHand){
		this.needHand = needHand;
	}
	public byte getNeedHand(){
		return this.needHand;
	}

}
