using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    public string HudInformation { get; }
    public void Use();
}
