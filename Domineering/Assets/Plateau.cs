using UnityEngine;
using System.Collections;

public class Plateau : MonoBehaviour {
    [SerializeField] private int _height  = 8;
    [SerializeField] private int _width   = 8;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetHeight(int height){
        _height = height;
    }

    public void SetWidth(int width) {
        _width = width;
    }
}
