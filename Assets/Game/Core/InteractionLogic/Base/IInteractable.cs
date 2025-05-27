using System;
using UnityEngine;

public interface Iinteractable
{
    void Clicked(Action removedFromHand = null);
}