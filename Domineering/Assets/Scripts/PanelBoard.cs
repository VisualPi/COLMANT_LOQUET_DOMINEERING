using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelBoard : MonoBehaviour {
    [SerializeField]
    private GridLayoutGroup _glg;
    [SerializeField]
    private GameObject _itemCase;
    [SerializeField]
    private Transform _contentPanel;
    private Game _game;
    [SerializeField]
    private RectTransform _rt;

    [SerializeField]
    private List<PanelCase> _cases = new List<PanelCase>();

    [SerializeField]
    private Board _board;

    public void Display() {

        for (var curLine = 0; curLine < _board.GetLines(); ++curLine )
            for (var curColumn = 0; curColumn < _board.GetColumns(); ++curColumn ) {
                _cases[curColumn + ( curLine * _board.GetLines())].IsSelected = _board[curLine][curColumn];
            }
    }

    public void Init(Board board, Game game) {
        _game = game;
        _board = board;
        _glg.constraintCount = _board.GetLines();
        _glg.cellSize = Vector2.one * Mathf.RoundToInt(_rt.rect.height / _board.GetLines());

        for (var curLine = 0; curLine < _board.GetLines(); ++curLine )
            for (var curColumn = 0; curColumn < _board.GetColumns(); ++curColumn )
                _popCell(curLine, curColumn);
    }

    private void _popCell(int line, int column) {
        var cell = Instantiate(_itemCase).GetComponent<PanelCase>();
        cell.IsSelected = _board[line][column];
        cell.transform.SetParent(_contentPanel, false);
        cell.Init(new Coordonnee { line = line, column = column }, _game);

        _cases.Add(cell);
    }
}
