using UnityEngine;

public class PanelCase : MonoBehaviour {
    private Game _game;
    [SerializeField]
    private Animator _animator;

    private bool isTmp;
    private Coordonnee _myCoordonnee;

    void Start() {
        isTmp = false;
    }

    public void Init(Coordonnee coordonnee, Game game) {
        _game = game;
        _myCoordonnee = coordonnee;
    }

    public bool IsSelected {
        get { return _animator.GetBool("IsSelected"); }
        set { _animator.SetBool("IsSelected", value); }
    }

    public void MouseEnter() {
        if (!IsSelected) {
            isTmp = true;
            _game.MovePlayer(_myCoordonnee);
        }
    }

    public void MouseExit() {
        if (isTmp) {
            isTmp = true;
            _game.UndoMovePlayer(_myCoordonnee);
        }
    }
}
