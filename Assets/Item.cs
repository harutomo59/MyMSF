using UnityEngine;
using System.Collections;

public class Item{

	string name;
	string category; //大カテゴリ（武器,体,靴,アクセサリー,消費アイテム…）

	int buyValue;

	public void setName(string name){
		this.name = name;
	}
	public void setCategory(string category){
		this.category = category;
	}
	public void setBuyValue(int buyValue){
		this.buyValue = buyValue;
	}

	public string getName(){
		return this.name;
	}
	public string getCategory(){
		return this.category;
	}
	public int getBuyValue(){
		return this.buyValue;
	}

}
