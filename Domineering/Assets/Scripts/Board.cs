using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {
    [SerializeField] private int _lines  = 8;
    [SerializeField] private int _columns   = 8;
	private List<List<bool>> _board;

	public Board(int l, int c)
	{
		_board = new List<List<bool>>();
		this._lines = l;
		this._columns = c;
	    for (int i = 0; i < _lines ; i++) {
	        _board.Add(new List<bool>(_lines));
            for (int j = 0; j < _columns ; j++)
                _board[i].Add(false);
	    }
	}

    public void SetLines(int lines){
		_lines = lines;
    }
	public int GetLines()
	{
		return _lines;
	}
	public void SetColumns(int columns) {
		_columns = columns;
    }
	public int GetColumns()
	{
		return _columns;
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
