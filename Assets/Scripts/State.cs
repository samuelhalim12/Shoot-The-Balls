using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Idle()
    {
        yield break;
    }
    public virtual IEnumerator Attack()
    {
        yield break;
    }
}
