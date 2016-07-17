using UnityEngine;
using System.Collections;

/**
 * this class maybe added to GameController,
 * and used by BatleController.
 */
public class CursorController : MonoBehaviour {

	public GameObject cursorObj;
	public GameObject highlight;

	private GameObject csr;

	int posx = 0;
	int posy = 0;
	int posz = 0;

	bool visible = false;

	/**
	 * 現在の座標を計算しposx,posy,poszに格納
	 */
	void nowMyPos(){
		posx = (int)csr.transform.position.x;
		posy = (int)csr.transform.position.y;
		posz = (int)csr.transform.position.z;
	}

	public void cursorMoveTo(int x,int z){
		if(visible == false) return;
		csr.transform.position = new Vector3 (x + 0.5f, 0.02f, z + 0.5f);
		nowMyPos ();
	}

	public void cursorMove(int dx, int dz){
		nowMyPos ();
		if(visible == false) return;
		csr.transform.position = new Vector3 (posx + dx + 0.5f, 0.02f, posz + dz + 0.5f);
		nowMyPos ();
	}

	/**
	 * active/hide cursor object
	 */
	public void visibleCursor(bool flag){
		if (flag != visible) {
			visible = flag;
			csr.SetActive(visible);
		}
	}

	/**
	 * activate cursor object and move to (x,z)
	 */
	public void activateCursorAt(int x,int z){
		visibleCursor (true);
		cursorMoveTo (x, z);
	}

	/**
	 * get cursored (x,z) position as Vector2
	 */
	public Vector2 getSelectedPosition(){
		return new Vector2(posx, posz);
	}

	/**
	 * カーソルハイライトを表示する
	 * flagsがtrueの場所に表示
	 */
	public void showHighlights(bool[,] flags){
		//Debug.Log ("length ->" + flags.Length + "\nrank ->" + flags.Rank);
		//Debug.Log ("length0 ->" + flags.GetLength(0) + "\nlength1 ->" + flags.GetLength(1));

		for (int i=0; i<flags.GetLength (0); i++) {
			for(int j=0; j<flags.GetLength (1); j++){
				if(flags[i,j] == true){
					showObjectAt (highlight,i + 0.5f,j + 0.5f);
				}
			}
		}

	}

	/**
	 * 全てのカーソルハイライトを消去する
	 */
	public void deleteHighlights(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Highlight");
		for (int i=0; i<objs.Length; i++) {
			Destroy (objs[i]);
		}
	}

	/**
	 * instantiate object at (x,z)
	 */
	private void showObjectAt(GameObject obj, float x, float z){
		GameObject o = GameObject.Instantiate (obj);
		o.transform.position = new Vector3 (x, o.transform.position.y, z);
	}

	// Use this for initialization
	void Start () {
		csr = GameObject.Instantiate (cursorObj);
		csr.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
