using UnityEngine;
using System.Collections;

public class PanelCase : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

    public bool IsSelected {
        get { return _animator.GetBool("IsUp"); }
        set { _animator.SetBool("IsUp", value); }
    }
}
