using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private bool _tryLeft;
    private bool _tryRight;
    [SerializeField] private Animator _animator;

    public bool TriedLeft()
    {
        _tryLeft = true;
        return CheckTutorial();
    }

    public bool TriedRight() {
        _tryRight = true;
        return CheckTutorial();
    }

    public bool CheckTutorial()
    {
        if (_tryLeft && _tryRight)
        {
            _animator.SetTrigger("Complete");
            return true;
        }
        return false;
    }

}
