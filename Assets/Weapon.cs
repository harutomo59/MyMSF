using UnityEngine;
using System.Collections;

/**
 * 一つの武器を表すクラス
 */
public class Weapon{

	string name;

	//status
	int weight;
	int atk;
	int matk;
	int def;
	int mdef;
	float hitRate; //悩み:絶対値にするか,キャラに係る補正値にするか
	float avoidRate;
	int range;

	public void setName(string name){

	}

}
