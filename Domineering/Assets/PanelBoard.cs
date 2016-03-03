using UnityEngine;
using System.Collections.Generic;

public class PanelBoard : MonoBehaviour {
    [SerializeField]
    private int _column = 8;
    [SerializeField]
    private int _line = 8;

    private int _case = 0;

    [SerializeField]
    private GameObject ItemCase;
    [SerializeField]
    private PanelCase _caseScript;
    [SerializeField]
    private Transform _contentPanel;

    [SerializeField]
    private List<PanelCase> _cases = new List<PanelCase>();

    [SerializeField]
    private Board _board;

    void Start() {
        Init();
    }

    void Display() {
        for (int i = 0; i < _cases.Count; ++i) {
            var currentLine = i % _column;
            _cases[i].IsSelected = _board[currentLine][i - currentLine];
        }
    }

    public void Init() {
        _case = _column * _line;

        for (int i = 0; i < _case; i++)
            _popCell();
    }

    private void _popCell() {
        var cell = Instantiate(ItemCase).GetComponent<PanelCase>();
        cell.IsSelected = true;
        cell.transform.SetParent(_contentPanel, false);

        _cases.Add(cell);
    }
}
