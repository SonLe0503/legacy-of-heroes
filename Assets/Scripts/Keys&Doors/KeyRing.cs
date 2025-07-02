using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyRing
{
    // The IDs of the keys held by the player
    private static HashSet<int> keyIDs = new HashSet<int>() { 0 };

    public static void AddKey(int keyID)
    {
        keyIDs.Add(keyID);
    }

    public static bool HasKey(Door door)
    {
        return keyIDs.Contains(door.doorID);
    }

    public static void ClearKeyRing()
    {
        keyIDs.Clear();
    }
}
