using UnityEngine;
using System.Collections;

/**
 * the class, list of action
 * this class will be used by BattleController.cs
 * contains the methods of controlling list
 */
public class ActionStack{

	Action[] actionList = new Action[100];
	int actionStackNum = 0;

	public ActionStack(){
		actionList.Initialize ();
	}

	/**
	 * actionをlistの適切な順番に挿入する
	 */
	public void insertAction(Action action){
		int n = searchInsertIndex (action.getWT ());
		insertIndex (n, action);
	}

	/**
	 * ターン開始時用.
	 * listの全てのWT値に対して, 先頭のWT値分だけ減らし
	 * 先頭の要素（action）を返す
	 */
	public Action nextAction(){
		int reduceWT = actionList[0].getWT();
		for(int i=0; i<actionStackNum; i++){
			actionList[i].reduceWT (reduceWT);
		}
		return actionList [0];
	}

	/**
	 * ターン終了時用.
	 * listの先頭のactionに対してWTを増加させ
	 * 順番を適正な場所にもっていく
	 */
	public void finishTurn(int WT){
		int n = searchInsertIndex (WT);
		actionList [0].setWT (WT);
		insertIndex (0, n);
	}

	/**
	 * 新しくn番目にactionを挿入する
	 * 元あった要素は後ろにずれる
	 */
	void insertIndex(int n, Action action){
		
		int final = actionStackNum;
		
		for (int i=final; i > n; i--) {
			actionList[i] = actionList[i-1];
		}
		
		Debug.Log ("insert " + action.getUnitId ());
		actionList [n] = action;
		actionStackNum++;
		
	}

	/**
	 * x番目の要素をn番目にもっていく
	 * (x < n)
	 * 元あった要素は前にずれる
	 * [x]が[n-1]に移動
	 */
	void insertIndex(int x, int n){
		Action tmp = actionList [x];

		for(int i=x; i<n-1; i++){
			actionList[i] = actionList[i+1];
		}

		actionList [n - 1] = tmp;
	}
	
	/**
	 * WTがnの行動はListのどこに格納すればいいのかのindexを返す
	 * 2と3の間に入れるべきなら3が返る
	 */
	int searchInsertIndex(int n){
		int i = 0;
		//Debug.Log (" actionStack.Length -> " + actionStack.Length);
		for (i=0; i< actionStackNum; i++) {
			if(n < actionList[i].getWT ()){
				return i;
			}
		}
		
		return i;
		
	}
	
}
