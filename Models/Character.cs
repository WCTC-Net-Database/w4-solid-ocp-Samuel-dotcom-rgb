namespace W04.Models;


public class Character
{

    public string Name { get; set; } = string.Empty;


    public string Profession { get; set; } = string.Empty;


    public int Level { get; set; }

    public int HP { get; set; }

    public List<string> Equipment { get; set; } = new List<string>();


    public Character() { }


    public Character(string name, string profession, int level, int hp, List<string> equipment)
    {
        Name = name;
        Profession = profession;
        Level = level;
        HP = hp;
        Equipment = equipment ?? new List<string>();
    }

    public override string ToString()
    {
        string equipmentList = Equipment.Count > 0
            ? string.Join(", ", Equipment)
            : "none";
        return $"{Name} the {Profession} (Level {Level}, {HP} HP) - Equipment: {equipmentList}";
    }
}
