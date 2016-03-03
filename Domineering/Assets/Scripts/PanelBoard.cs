using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelBoard : MonoBehaviour {
    private int _case = 0;
    [SerializeField]
    private GridLayoutGroup _glg;
    [SerializeField]
    private GameObject _itemCase;
    [SerializeField]
    private Transform _contentPanel;
    private Game _game;

    [SerializeField]
    private List<PanelCase> _cases = new List<PanelCase>();

    [SerializeField]
    private Board _board;

    public void Display() {

        for (var curColumn = 0; curColumn < _board.GetHeight(); ++curColumn)
            for (var curLine = 0; curLine < _board.GetWidth(); ++curLine) {
                _cases[curColumn * (curLine + 1)].IsSelected = _board[curColumn][curLine];
            }
    }

    public void Init(Board board, Game game) {
        _game = game;
        _board = board;
        _case = _board.GetHeight() * _board.GetWidth();
        _glg.constraintCount = _board.GetHeight();
        //_glg.cellSize = Vector2.one * Mathf.RoundToInt(100 * _board.GetWidth() / _board.GetHeight()+1);

        for (var curColumn = 0; curColumn < _board.GetHeight(); ++curColumn)
            for (var curLine = 0; curLine < _board.GetWidth(); ++curLine)
                _popCell(curLine, curColumn);
    }

    private void _popCell(int line, int column) {
        var cell = Instantiate(_itemCase).GetComponent<PanelCase>();
        cell.IsSelected = _board[column][line];
        cell.transform.SetParent(_contentPanel, false);
        cell.Init(new Coordonnee { x = column, y = line }, _game);

        _cases.Add(cell);
    }
}
