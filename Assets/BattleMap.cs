using UnityEngine;
using System.Collections;

public class BattleMap{

	//basic map max Size
	int sx = 25;
	int sz = 25;

	/**
	 * MAP情報のみ　ユニット情報は無し
	 * what kind of information???
	 * maybe we can use this for move-able info
	 */
	int[,] map;

	/**
	 * height of the map
	 */
	int[,] height;

	/**
	 * if false, there is obstacle
	 */
	bool[,] canStand;

	/**
	 * type of ground
	 */
	int[,] type;

	/**
	 * unitId
	 * if (value<0) then noone
	 */
	int[,] unit;

	/**
	 * set unit
	 */
	public void setUnit(int x, int z, int unitId){
		unit [x,z] = unitId;
	}

	/**
	 * get unitId by (x,y)
	 */
	public int getUnitId(int x, int z){
		return unit [x,z];
	}

	/**
	 * get unitId's position
	 */
	public Vector2 getUnitPos(int unitId){
		for (int i=0; i<this.sx; i++) {
			for(int j=0; j<this.sz; j++){
				if(unit[i,j] == unitId){
					return new Vector2(i,j);
				}
			}
		}

		return new Vector2 (-1,-1);
	}

	/**
	 * move unitId's unit to (x,z)
	 */
	public void moveUnit(int unitId, int x, int z){
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				if(unit[i,j] == unitId){
					unit[i,j] = -1;
					unit[x,z] = unitId;
					return;
				}
			}
		}
	}

	/**
	 * 平面でのマス距離を計算する
	 */
	public int getMass2D(int x1, int z1, int x2, int z2){
		int x = x1 - x2;  
		int z = z1 - z2;
		if (x < 0) x = -x;
		if (z < 0) z = -z;

		return x + z;
	}

	/**
	 * 平面でのマス距離を計算する
	 */
	public int getMass2D(Vector2 pos1, Vector2 pos2){
		return this.getMass2D ((int)pos1.x, (int)pos1.y, (int)pos2.x, (int)pos2.y);
	}


	/**
	 * 移動できる座標を取得する
	 * 
	 * 現在は障害物等を考えずに距離だけです
	 */
	public bool[,] getMoveablePositions(int myX, int myZ, int dist, int team){
		bool[,] result = new bool[sx, sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				if((getMass2D (myX, myZ, i, j) <= dist && this.canStand[i,j] == true)){
					result[i,j] = true;
				}else{
					result[i,j] = false;
				}
			}
		}
		return result;
	}

	/**
	 * 対象指定できる位置を取得する（ターゲット指定）
	 * 敵味方区別なく指定できる
	 * myselfをtrueにすると自分自身も指定できる
	 * 
	 * 現在は障害物等を考えずに距離だけです
	 */
	public bool[,] getTarget(int myX, int myZ, int dist, bool myself){
		bool[,] result = new bool[sx, sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				if((getMass2D (myX, myZ, i, j) <= dist && this.unit[i,j] >= 0)){
					result[i,j] = true;
				}else{
					result[i,j] = false;
				}
			}
		}
		result [myX, myZ] = false;
		return result;
	}

	/**
	 * 指定できる位置を取得する（位置指定）
	 * 障害物等の位置も指定できる
	 * 
	 * 現在は高さを考慮せずに距離だけです
	 */
	public bool[,] getTargetGround(int myX, int myZ, int dist){
		bool[,] result = new bool[sx, sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				if((getMass2D (myX, myZ, i, j) <= dist)){
					result[i,j] = true;
				}else{
					result[i,j] = false;
				}
			}
		}
		return result;
	}

	/**
	 * コンストラクタ
	 * 引数無しだとMAPサイズは２５×２５扱い
	 */
	public BattleMap(){
		initialBattleMap ();
	}

	/**
	 * コンストラクタ
	 * sizeX×sizeZのサイズで生成
	 */
	public BattleMap(int sizeX, int sizeZ){

		this.sx = sizeX;
		this.sz = sizeZ;

		initialBattleMap ();
	}

	/**
	 * initialize method
	 */
	private void initialBattleMap(){
		map = new int[sx,sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				map[i,j] = 0;
			}
		}
		
		height = new int[sx,sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				height[i,j] = 0;
			}
		}
		
		canStand = new bool[sx,sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				canStand[i,j] = true;
			}
		}
		
		type = new int[sx,sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				type[i,j] = 0;
			}
		}
		
		unit = new int[sx,sz];
		for (int i=0; i<sx; i++) {
			for(int j=0; j<sz; j++){
				unit[i,j] = -1;
			}
		}
	}

}

