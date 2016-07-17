using UnityEngine;
using System.Collections;

public class CommandController : MonoBehaviour {

	int count = 0;
	int cursorNum = 5;
	string[] commands;

	bool visible = true;

	// Use this for initialization
	void Start () {
		commandMenu ();
	}

	void commandMenu(){
		cursorNum = 5;
		commands = new string[cursorNum];
		commands [0] = "move";
		commands [1] = "attack";
		commands [2] = "skill";
		commands [3] = "guard";
		commands [4] = "end";
	}

	void commandSkill(){

	}

	void commandAttack(){

	}

	public string getCommand(){
		return commands [count];
	}

	public int getCommandNum(){
		return count;
	}

	public void setVisible(bool flag){
		visible = flag;
		count = 0;
	}

	public void cursorPlus(){
		count++;
		if (count >= cursorNum) {
			count = cursorNum-1;
		}
	}

	public void cursorMinus(){
		count--;
		if (count < 0) {
			count = 0;
		}
	}

	void OnGUI(){

		if (visible == true) {
			GUI.color = Color.black;
			for(int i=0; i<cursorNum; i++){
				GUI.Label (new Rect(Screen.width - 150, Screen.height - 300 + (i*30), 150, 30),commands[i]);
			}
			GUI.Label (new Rect(Screen.width - 170, Screen.height - 300 + (count*30), 20, 30),"→");
		}
	}
}
