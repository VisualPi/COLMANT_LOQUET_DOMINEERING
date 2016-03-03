using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {
    [SerializeField] private int _height  = 8;
    [SerializeField] private int _width   = 8;
	private List<List<bool>> _board;

	public Board(int h, int w)
	{
		_board = new List<List<bool>>();
		this._height = h;
		this._width = w;
	    for (int i = 0; i < _height; i++) {
	        _board.Add(new List<bool>(_width));
            for (int j = 0; j < _width; j++)
                _board[i].Add(false);
	    }
	}

    public void SetHeight(int height){
        _height = height;
    }
	public int GetHeight()
	{
		return _height;
	}
	public void SetWidth(int width) {
        _width = width;
    }
	public int GetWidth()
	{
		return _width;
	}
	public List<bool> this[int key]
	{
		get	{ return _board[key]; }
		set	{ _board[key] = value; }
	}
	public void SetBoard(List<List<bool>> board)
	{
		this._board = board;
	}
	public List<List<bool>> GetBoard()
	{
		return this._board;
	}
}
