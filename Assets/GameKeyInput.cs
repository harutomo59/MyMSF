using UnityEngine;
using System.Collections;

public class GameKeyInput : MonoBehaviour {

	GameObject nowUnit = null;

	public static int decide = 0;
	public static int cancel = 0;
	public static int up = 0;
	public static int down = 0;
	public static int left = 0;
	public static int right = 0;


	CommandController cmd;

	// Use this for initialization
	void Start () {
		nowUnit = GameObject.Find ("Unit01") as GameObject;
		cmd = this.GetComponent<CommandController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.C)) {
			decide++;
		} else {
			decide = 0;
		}
		if (Input.GetKey (KeyCode.X)) {
			cancel++;
		} else {
			cancel = 0;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			up++;
		} else {
			up = 0;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			down++;
		} else {
			down = 0;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			left++;
		} else {
			left = 0;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			right++;
		} else {
			right = 0;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			//nowUnit.GetComponent<UnitAction>().UnitMoveZ (1);
			cmd.cursorMinus ();
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//nowUnit.GetComponent<UnitAction>().UnitMoveZ (-1);
			cmd.cursorPlus ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			//nowUnit.GetComponent<UnitAction>().UnitMoveX (1);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			//nowUnit.GetComponent<UnitAction>().UnitMoveX (-1);
		}
	}

	public void setNowUnit(GameObject obj){
		nowUnit = obj;
	}
}
