using UnityEngine;
using System.Collections;

/**
 * this class will be added to BattleUnit
 */
public class UnitAction : MonoBehaviour {

	public int myX;
	public int myY;
	public int myZ;

	// Use this for initialization
	void Start () {
		nowMyPos ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * 現在の座標を計算しmyX,myY,myZに格納
	 */
	void nowMyPos(){
		myX = (int)this.transform.position.x;
		myY = (int)this.transform.position.y;
		myZ = (int)this.transform.position.z;
	}

	/**
	 * 現在の座標をVector3として返す
	 */
	public Vector3 getNowPos(){
		return new Vector3 (myX, myY, myZ);
	}
	
	/**
	 * X座標をnだけ移動
	 */
	public void UnitMoveX(int n){
		nowMyPos ();
		this.transform.position = new Vector3 (myX + n + 0.5f,myY,myZ+ 0.5f);
		nowMyPos ();
	}


	/**
	 * Z座標をnだけ移動
	 */
	public void UnitMoveZ(int n){
		nowMyPos ();
		this.transform.position = new Vector3 (myX+ 0.5f,myY,myZ + n+ 0.5f);
		nowMyPos ();
	}

	/**
	 * 指定した座標まで歩く
	 */
	public void UnitMoveTo(int x,int z){
		nowMyPos ();

		int nowx = myX;
		int nowz = myZ;

		UnitMove (x - nowx, z - nowz);

		nowMyPos ();
	}

	/**
	 * 指定した距離を歩く
	 */
	public void UnitMove(int x,int z){
		UnitMoveX (x);
		UnitMoveZ (z);
	}
}
