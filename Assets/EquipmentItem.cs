using UnityEngine;
using System.Collections;

public class EquipmentItem : Item {

	string type;  //小カテゴリ（片手剣,大剣,金属鎧,ローブ…）
	
	//status
	int weight;
	int atk;
	int matk;
	int def;
	int mdef;
	float hitRate; //悩み:絶対値にするか,キャラに係る補正値にするか
	float avoidRate;

	public void setType(string type){
		this.type = type;
	}
	public void setWeight(int weight){
		this.weight = weight;
	}
	public void setAtk(int atk){
		this.atk = atk;
	}
	public void setMAtk(int matk){
		this.matk = matk;
	}
	public void setDef(int def){
		this.def = def;
	}
	public void setMDef(int mdef){
		this.mdef = mdef;
	}
	public void setHitRate(float hitRate){
		this.hitRate = hitRate;
	}
	public void setAvoidRate(float avoidRate){
		this.avoidRate = avoidRate;
	}

	public string getType(){
		return this.type;
	}
	public int getWeight(){
		return this.weight;
	}
	public int getAtk(){
		return this.atk;
	}
	public int getMAtk(){
		return this.matk;
	}
	public int getDef(){
		return this.def;
	}
	public int getMDef(){
		return this.mdef;
	}
	public float getHitRate(){
		return this.hitRate;
	}
	public float getAvoidRate(){
		return this.avoidRate;
	}
}
