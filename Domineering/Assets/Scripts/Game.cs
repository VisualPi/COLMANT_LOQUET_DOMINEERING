﻿using UnityEngine;
using System.Collections;

public enum EJoueur { VERTICAL = 0, HORIZONTAL = 1 };

public class Game : MonoBehaviour {
    [SerializeField]
    private PanelBoard _panelBoard;


    [SerializeField]
    private int _nbPlayer;
    [SerializeField]
    private int _lines;
    [SerializeField]
    private int _columns;
    //[SerializeField] private EJoueur _playerDirection;
    private Board _board;
    private bool _isPlayerPlayed;
    private AI _ai;
    public void Start() {
        Init(1, 8, 8);
    }
    public void Init(int nb, int l, int c, EAiAlgo algo = EAiAlgo.BASIC) {
        this._lines = l;
        this._columns = c;
        _board = new Board(l, c);
        _ai = new AI(algo);
        _panelBoard.Init(_board, this);
    }

    public void Update() {
        if (this._isPlayerPlayed) {
            Coordonnee c = _ai.Move(_board);
			_panelBoard.Display();
            this._isPlayerPlayed = false;
        }
    }

    public void SetIsPlayerPlayed(bool b) {
        this._isPlayerPlayed = b;
    }
    public bool GetIsPlayerPlayed() {
        return this._isPlayerPlayed;
    }

    public bool MovePlayer(Coordonnee c, EJoueur joueur = EJoueur.VERTICAL) {
        bool valReturn = true;
        if (joueur == EJoueur.VERTICAL) {
            if (c.line + 1 >= _board.GetLines() && (_board[c.line][c.column] || _board[c.line + 1][c.column]))
                valReturn = false;
            else {
                _board[c.line][c.column] = true;
                _board[c.line + 1][c.column] = true;
            }
        } else {
            if (c.column + 1 >= _board.GetColumns() && (_board[c.line][c.column] || _board[c.line][c.column + 1]))
                valReturn = false;
            else {
                _board[c.line][c.column] = true;
                _board[c.line][c.column + 1] = true;
            }
        }
        _panelBoard.Display();
        return valReturn;
    }
    public bool UndoMovePlayer(Coordonnee c, EJoueur joueur = EJoueur.VERTICAL) {
        bool valReturn = true;
        if (joueur == EJoueur.VERTICAL) {
            if (c.line + 1 < _board.GetLines() && (_board[c.line][c.column] && _board[c.line + 1][c.column])) {
                _board[c.line][c.column] = false;
                _board[c.line + 1][c.column] = false;
            } else
                valReturn = false;
        } else {
            if (c.column + 1 < _board.GetColumns() && (_board[c.line][c.column] && _board[c.line][c.column + 1])) {
                _board[c.line][c.column] = false;
                _board[c.line][c.column + 1] = false;
            } else
                valReturn = false;
        }
        _panelBoard.Display();
        return valReturn;
    }

	public int GetLines()
	{
		return _lines;
	}
	public int GetColumns()
	{
		return _columns;
	}
}