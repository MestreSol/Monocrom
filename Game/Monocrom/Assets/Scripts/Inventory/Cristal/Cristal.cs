using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CristalType
{
    Alpha,
    Beta,
    Gamma,
    Delta,
    Epsilon,
    Zeta,
    Eta,
    Theta,
    Iota
}
[CreateAssetMenu(fileName = "Cristal", menuName = "Items/Cristal")]
public class Cristal : Item
{
    public string description;
    public CristalType cristalType;
}
