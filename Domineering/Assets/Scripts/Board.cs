using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {
    [SerializeField] private int _height  = 8;
    [SerializeField] private int _width   = 8;
	private List<List<bool>> _board;

	public Board(int h, int w)
	{
		this._height = h;
		this._width = w;
	}

	void Start () {
		for (int i = 0 ; i < _height ; i++ )
			_board.Add(new List<bool>(_width));
	}

    public void SetHeight(int height){
        _height = height;
    }

    public void SetWidth(int width) {
        _width = width;
    }
	public List<bool> this[int key]
	{
		get	{ return _board[key]; }
		set	{ _board[key] = value; }
	}

}
