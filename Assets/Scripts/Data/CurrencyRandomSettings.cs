using System;
using Data;

[Serializable]
public struct CurrencyRandomSettings
{
    public bool spawnOnLet;
    public bool spawnOnGround;
    public bool spawnOnHole;
    public int spawnChance;
    public CurrencyType currencyType;
    public int rangeStart;
    public int rangeEnd;
    public AccuracyLevel accuracyLevel;
}