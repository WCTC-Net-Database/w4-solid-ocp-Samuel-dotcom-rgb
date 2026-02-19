using W04.Interfaces;
using W04.Models;

namespace W04.Services;
public class CsvFileHandler : IFileHandler
{
    private readonly string _filePath;

    public CsvFileHandler(string filePath)
    {
        _filePath = filePath;
    }

    public List<Character> ReadAll()
    {
        var characters = new List<Character>();
        if (!File.Exists(_filePath))
            return characters;

        var lines = File.ReadAllLines(_filePath);
        if (lines.Length <= 1)
            return characters;

        // Skip header
        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            // Handle quoted names with commas
            var fields = new List<string>();
            bool inQuotes = false;
            string current = "";
            foreach (char c in line)
            {
                if (c == '"') inQuotes = !inQuotes;
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(current);
                    current = "";
                }
                else current += c;
            }
            fields.Add(current);

            if (fields.Count < 5) continue;
            var name = fields[0].Trim('"');
            var profession = fields[1];
            int.TryParse(fields[2], out int level);
            int.TryParse(fields[3], out int hp);
            var equipment = fields[4].Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
            characters.Add(new Character(name, profession, level, hp, equipment));
        }
        return characters;
    }

    public void WriteAll(List<Character> characters)
    {
        var lines = new List<string> { "Name,Profession,Level,HP,Equipment" };
        foreach (var c in characters)
        {
            // Quote name if it contains a comma
            var name = c.Name.Contains(',') ? $"\"{c.Name}\"" : c.Name;
            var equipment = string.Join("|", c.Equipment);
            lines.Add($"{name},{c.Profession},{c.Level},{c.HP},{equipment}");
        }
        File.WriteAllLines(_filePath, lines);
    }

    public void AppendCharacter(Character character)
    {
        // Quote name if it contains a comma
        var name = character.Name.Contains(',') ? $"\"{character.Name}\"" : character.Name;
        var equipment = string.Join("|", character.Equipment);
        var line = $"{name},{character.Profession},{character.Level},{character.HP},{equipment}";
        File.AppendAllText(_filePath, "\n" + line);
    }

    public Character? FindByName(List<Character> characters, string name)
    {
        return characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Character> FindByProfession(List<Character> characters, string profession)
    {
        return characters.Where(c => c.Profession.Equals(profession, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
