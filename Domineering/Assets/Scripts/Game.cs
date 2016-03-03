﻿using UnityEngine;
using System.Collections;

public enum EJoueur { VERTICAL = 0, HORIZONTAL = 1 };

public class Game : MonoBehaviour {
    [SerializeField]
    private PanelBoard _panelBoard;


    [SerializeField]
    private int _nbPlayer;
    [SerializeField]
    private int _height;
    [SerializeField]
    private int _width;
    //[SerializeField] private EJoueur _playerDirection;
    private Board _board;
    private bool _isPlayerPlayed;
    private AI _ai;
    public void Start() {
        Init(1, 8, 8);
    }
    public void Init(int nb, int h, int w, EAiAlgo algo = EAiAlgo.BASIC) {
        this._height = h;
        this._width = w;
        _board = new Board(h, w);
        _ai = new AI(algo);
        _panelBoard.Init(_board, this);
    }

    public void Update() {
        if (this._isPlayerPlayed) {
            _ai.Move(_board);
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
            if (_board[c.x][c.y] || _board[c.x + 1][c.y])
                valReturn = false;
            else {
                _board[c.x][c.y] = true;
                _board[c.x + 1][c.y] = true;
            }
        } else {
            if (_board[c.x][c.y] || _board[c.x][c.y + 1])
                valReturn = false;
            else {
                _board[c.x][c.y] = true;
                _board[c.x][c.y + 1] = true;
            }
        }
        _panelBoard.Display();
        return valReturn;
    }
    public bool UndoMovePlayer(Coordonnee c, EJoueur joueur = EJoueur.VERTICAL) {
        bool valReturn = true;
        if (joueur == EJoueur.VERTICAL) {
            if (_board[c.x][c.y] && _board[c.x + 1][c.y]) {
                _board[c.x][c.y] = false;
                _board[c.x + 1][c.y] = false;
            } else
                valReturn = false;
        } else {
            if (_board[c.x][c.y] && _board[c.x][c.y + 1]) {
                _board[c.x][c.y] = false;
                _board[c.x][c.y + 1] = false;
            } else
                valReturn = false;
        }
        _panelBoard.Display();
        return valReturn;
    }
}