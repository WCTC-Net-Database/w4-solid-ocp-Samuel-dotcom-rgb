namespace W04.Models;

/// <summary>
/// Represents an RPG character with their attributes and equipment.
///
/// This is the same Character class from Week 3 - it's a data class
/// that holds character information. The Single Responsibility Principle
/// means this class only stores data, it doesn't read/write files.
///
/// Note: System.Text.Json (which we use for JSON) automatically handles
/// property serialization. No special attributes needed!
/// </summary>
public class Character
{
    /// <summary>
    /// The character's name (e.g., "John, Brave" - note: may contain commas!)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The character's profession (Fighter, Wizard, Rogue, etc.)
    /// </summary>
    public string Profession { get; set; } = string.Empty;

    /// <summary>
    /// The character's current level (starts at 1)
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Hit Points - the character's health
    /// </summary>
    public int HP { get; set; }

    /// <summary>
    /// List of equipment items the character carries.
    /// In CSV, these are separated by | (pipe character).
    /// In JSON, this serializes as an array naturally.
    /// </summary>
    public List<string> Equipment { get; set; } = new List<string>();

    /// <summary>
    /// Default constructor
    /// </summary>
    public Character() { }

    /// <summary>
    /// Convenience constructor to create a fully-initialized character
    /// </summary>
    public Character(string name, string profession, int level, int hp, List<string> equipment)
    {
        Name = name;
        Profession = profession;
        Level = level;
        HP = hp;
        Equipment = equipment ?? new List<string>();
    }

    /// <summary>
    /// Returns a formatted string representation of the character.
    /// </summary>
    public override string ToString()
    {
        string equipmentList = Equipment.Count > 0
            ? string.Join(", ", Equipment)
            : "none";
        return $"{Name} the {Profession} (Level {Level}, {HP} HP) - Equipment: {equipmentList}";
    }
}
