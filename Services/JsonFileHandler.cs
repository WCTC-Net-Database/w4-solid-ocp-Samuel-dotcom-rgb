using System.Text.Json;
using W04.Interfaces;
using W04.Models;

namespace W04.Services;


public class JsonFileHandler : IFileHandler
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonFileHandler(string filePath)
    {
        _filePath = filePath;
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true  // Makes the JSON human-readable
        };
    }

    public List<Character> ReadAll()
    {
        if (!File.Exists(_filePath))
            return new List<Character>();
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Character>>(json) ?? new List<Character>();
    }

    public void WriteAll(List<Character> characters)
    {
        string json = JsonSerializer.Serialize(characters, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    public void AppendCharacter(Character character)
    {
        var characters = ReadAll();
        characters.Add(character);
        WriteAll(characters);
    }

    public Character? FindByName(List<Character> characters, string name)
    {

        return characters.FirstOrDefault(c =>
            c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Character> FindByProfession(List<Character> characters, string profession)
    {
        // Same LINQ logic works regardless of where the data came from
        return characters.Where(c =>
            c.Profession.Equals(profession, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
