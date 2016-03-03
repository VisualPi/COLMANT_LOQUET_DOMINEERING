using UnityEngine;
using System.Collections;

public class PanelCase : MonoBehaviour {

    [SerializeField]
    private Game _game;
    [SerializeField]
    private Animator _animator;

    //[SerializeField]
    //private Sprite _default;
    //[SerializeField]
    //private Sprite _selected;

    private bool isTmp;
    private Coordonnee _myCoordonnee;

    void Start() {
        isTmp = false;
    }

    public void Init(Coordonnee coordonnee) {
        _myCoordonnee = coordonnee;
    }

    public bool IsSelected {
        get { return _animator.GetBool("IsUp"); }
        set { _animator.SetBool("IsUp", value); }
    }

    public void MouseEnter() {
        Debug.Log("enter");
        if (!IsSelected) {
            isTmp = true;
            Debug.Log(_myCoordonnee.x + ", " + _myCoordonnee.y);
            _game.MovePlayer(_myCoordonnee);
        }
    }

    public void MouseExit() {
        Debug.Log("exit");
        if (isTmp) {
            isTmp = true;
            _game.UndoMovePlayer(_myCoordonnee);
        }
    }
}
