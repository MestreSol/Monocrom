using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NucleoType
{
    Periferico,
    Central
}
public class NucleoSlot : Slot
{
    public NucleoType nucleoType;
    public Nucleo nucleo;

}
