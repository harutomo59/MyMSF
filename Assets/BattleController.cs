using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Generic.ArrayList;

public class BattleController : MonoBehaviour {
	//Action[] actionStack = new Action[100];
	//int actionStackNum = 0;
	//LinkedList<Action> actionList = new LinkedList<Action>();

	GameObject[] units = new GameObject[20];  //キャラオブジェを格納
	ActionStack actionStack = new ActionStack();  //WT値を持つactionやunitの管理
	int unitNum = 0;  //戦場に居るWT値持ちキャラの総数

	int controlUnitId = 0; //操作中のキャラのID

	BattleMap map = new BattleMap (); // information of battlemap
	//CursorController cur = new CursorController();

	int MPCounter;  //MP回復タイミング管理用カウンタ
	int MPWT = 20;  //MPの回復速度. この値ごとのWT経過でMP回復


	//info of controlling unit
	Vector3 ctrlUnitPos;
	int ctrlUnitMove;

	int movedDist;


	//action flags
	static bool canMove;
	static bool canAction;


	//components
	CommandController cmd;
	GameKeyInput key;
	CursorController cur;
	UnitAction uniact;  //component of controlling unit's

	//commands
	const int move = 0;
	const int attack = 1;
	const int skill = 2;
	const int guard = 3;
	const int end = 4;
	
	public enum GAMESTATE
	{
		BEFORE,
		//BOSS,
		WT,
		THINK,
		ACTION,
		CLEAR,
		DEAD,
		ERROR,
		NONE
	}

	public enum TURNSTATE
	{
		MENU,
		MOVE,
		MOVING,
		ATTACK,
		ATTACKING,
		NONE
	}

	public static GAMESTATE gameState;
	public static TURNSTATE turnState;

	// Use this for initialization
	void Start () {

		//get components
		cmd = this.GetComponent<CommandController> ();
		key = this.GetComponent<GameKeyInput> ();
		cur = this.GetComponent<CursorController> ();

		map = new BattleMap ();

		/*
		 * 初期ユニットを行動listに入れる作業
		 * タグBattleUnitのやつが対象
		 */
		GameObject[] tmp = GameObject.FindGameObjectsWithTag ("BattleUnit");
		unitNum = tmp.Length;

		for (int i=0; i<unitNum; i++) {
			units[i] = tmp[i];
			units[i].GetComponent<UnitStatus>().setUnitId (i);

			/*
			 * 初期WT値を取得
			 * 現在はキャラMAX値をとってる　将来的にはスキルで初期値変わったり？
			 */
			int wt = units[i].GetComponent<UnitStatus>().getWTmax ();

			Action a = new Action(i,wt,"unit");
			a.setUnitInfo (1,i);

			actionStack.insertAction (a);


			/*
			 * register unit info to map
			 */
			map.setUnit ((int)units[i].transform.position.x,
			             (int)units[i].transform.position.z,
			             i);
		}

		//ゲーム情報初期化
		gameState = GAMESTATE.WT;
		turnState = TURNSTATE.NONE;
		MPCounter = 0;
		cmd.setVisible (false);

	}


	
	// Update is called once per frame
	void Update () {

		/*ゲーム状態によって処理を分ける*/
		if(gameState == GAMESTATE.WT){  //行動待ち,WTを減らす時間

			//さーて,次のactionはー？
			Action act = actionStack.nextAction ();

			if(act.getWT () <= 0){
				/*actionのtypeによって処理を分ける*/
				if(act.getType () == "unit"){
					controlUnitId = act.getUnitId();   //操作キャラクタのidをset
					key.setNowUnit (units[controlUnitId]);
					gameState = GAMESTATE.THINK;
					turnState = TURNSTATE.MENU;

					//reset flags
					canMove = true;
					canAction = true;

					//操作キャラの情報を引き出しておきたい
					uniact = units[controlUnitId].GetComponent<UnitAction>();
					ctrlUnitPos = uniact.getNowPos ();
					//ctrlUnitMove= units[controlUnitId].GetComponent<UnitStatus>().
					ctrlUnitMove = 5;

					//コマンドの可視化
					cmd.setVisible (true);
				}

			}
		}// end of "if(gameState == GAMESTATE.ET)"

		if (gameState == GAMESTATE.THINK) {

			//選択状態によって処理を分ける
			if(turnState == TURNSTATE.MENU){
				//行動
				if(GameKeyInput.decide == 1){
					//コマンド取得
					int command = cmd.getCommandNum ();
					
					//コマンドにより処理を分ける
					switch(command){
					case move:
						modeMove ();
						break;
						
					case attack:
						attackMode();
						break;
						
					case skill:
						
						break;
						
					case guard:
						
						break;
						
					case end:
						finishTurn ();
						break;
					}
				}
			}else if(turnState == TURNSTATE.MOVE){
				//移動コマンド中

				if(GameKeyInput.decide == 1){
					Vector2 p = cur.getSelectedPosition();
					//移動距離の計算
					movedDist = map.getMass2D (p, new Vector2(ctrlUnitPos.x, ctrlUnitPos.z));
				
					uniact.UnitMoveTo((int) p.x, (int) p.y);
					map.moveUnit (this.controlUnitId,(int) p.x, (int) p.y);
					ctrlUnitPos = new Vector3(p.x, ctrlUnitPos.y, p.y);

					if(movedDist > 0){
						finMoveMode(false);
					}else{
						finMoveMode (true);
					}
				}

				if(GameKeyInput.cancel == 1){
					finMoveMode (true);
				}

				if(GameKeyInput.up == 1){
					cur.cursorMove (0,1);
				}
				if(GameKeyInput.down == 1){
					cur.cursorMove (0,-1);
				}
				if(GameKeyInput.left == 1){
					cur.cursorMove (-1,0);
				}
				if(GameKeyInput.right == 1){
					cur.cursorMove (1,0);
				}

			}else if(turnState == TURNSTATE.ATTACK){
				//通常攻撃モード

				if(GameKeyInput.decide == 1){
					Vector2 p = cur.getSelectedPosition();
					int targetId = map.getUnitId((int)p.x, (int)p.y);
					//攻撃処理

					Debug.Log("BattleController update attack AttackTo" + targetId);

					finAttackMode (false);
				}
				if(GameKeyInput.cancel == 1){
					finMoveMode (true);
				}
				
				if(GameKeyInput.up == 1){
					cur.cursorMove (0,1);
				}
				if(GameKeyInput.down == 1){
					cur.cursorMove (0,-1);
				}
				if(GameKeyInput.left == 1){
					cur.cursorMove (-1,0);
				}
				if(GameKeyInput.right == 1){
					cur.cursorMove (1,0);
				}
			}


			//行動終了
			if(GameKeyInput.cancel == 1){
				//finishTurn ();
			}


		}
	}

	/**
	 * マップにハイライトを表示させるモードの開始
	 */
	private void startMapTarget(bool[,] highlights){
		cur.visibleCursor (true);
		cur.cursorMoveTo ((int)this.ctrlUnitPos.x, (int)this.ctrlUnitPos.z);
		cur.showHighlights (highlights);
		cmd.setVisible (false);
	}

	/**
	 * マップにハイライトを表示させるモードの終了
	 * 引数は次に移行するturnState
	 */
	private void finMapTarget(TURNSTATE state){
		cur.deleteHighlights ();
		cur.visibleCursor (false);
		turnState = state;
		cmd.setVisible (true);
	}

	/**
	 * start MoveMode
	 */
	void modeMove(){
		if (canMove == false) {
			return;
		}
		turnState = TURNSTATE.MOVE;
		bool[,] flags = map.getMoveablePositions ((int)this.ctrlUnitPos.x, (int)this.ctrlUnitPos.z, ctrlUnitMove, 1);
		this.startMapTarget (flags);
		if (this.movedDist > 0) {
		
		}
	}

	/**
	 * finish MoveMode
	 * if flag is FALSE, action of move is done.
	 */
	void finMoveMode(bool flag){
		//this.finMapTarget (TURNSTATE.MOVING);
		this.finMapTarget (TURNSTATE.MENU);
		canMove = flag;
	}

	/**
	 * start AttackMode
	 */
	void attackMode(){
		if (canAction == false) {
			return;
		}
		turnState = TURNSTATE.ATTACK;
		int range = 1; //攻撃射程の取得
		bool[,] flags = map.getTarget ((int)this.ctrlUnitPos.x, (int)this.ctrlUnitPos.z, range, false);
		this.startMapTarget (flags);
	}

	/**
	 * finish AttackMode
	 * アクションが完了したならflagはFALSEに,
	 * キャンセルされたならflagはTRUEに.
	 */
	void finAttackMode(bool flag){
		//this.finMapTarget (TURNSTATE.ATTACKING);
		this.finMapTarget (TURNSTATE.MENU);
		canAction = flag;
	}

	/**
	 * ターン終了処理
	 * unit向け？
	 */
	void finishTurn(){
		//行動後のユニットがどれだけのWT値を得るか
		float wtp = 50.0f;
		if (canMove == false) {
			wtp += 15.0f + 10.0f * ((float)movedDist / this.ctrlUnitMove);
		}
		if (canAction == false) {
			wtp += 25.0f;
		}
		Debug.Log ("BattleController finishTurn wtp->" + wtp);
		int giveWT = units [controlUnitId].GetComponent<UnitStatus>().calcWT (wtp);

		movedDist = 0;

		//コマンドを隠す
		cmd.setVisible (false);

		//WTを増加させ順番を変える
		actionStack.finishTurn (giveWT);

		//次のターンへ
		gameState = GAMESTATE.WT;
		turnState = TURNSTATE.NONE;
	}
}