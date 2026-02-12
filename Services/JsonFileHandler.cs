using System.Text.Json;
using W04.Interfaces;
using W04.Models;

namespace W04.Services;

/// <summary>
/// JSON implementation of IFileHandler.
///
/// This is the NEW implementation you're adding this week!
/// Notice how we can add JSON support without modifying CsvFileHandler at all.
/// That's the Open/Closed Principle in action:
/// - Open for extension (adding JsonFileHandler)
/// - Closed for modification (CsvFileHandler unchanged)
/// </summary>
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
        // TODO: Implement JSON reading logic
        // Hint:
        // string json = File.ReadAllText(_filePath);
        // return JsonSerializer.Deserialize<List<Character>>(json) ?? new List<Character>();

        throw new NotImplementedException("Implement JSON reading logic");
    }

    public void WriteAll(List<Character> characters)
    {
        // TODO: Implement JSON writing logic
        // Hint:
        // string json = JsonSerializer.Serialize(characters, _jsonOptions);
        // File.WriteAllText(_filePath, json);

        throw new NotImplementedException("Implement JSON writing logic");
    }

    public void AppendCharacter(Character character)
    {
        // TODO: Implement append logic for JSON
        // Note: JSON doesn't support simple "append" like CSV does.
        // You need to: 1) Read existing, 2) Add to list, 3) Write all back
        //
        // Hint:
        // var characters = new List<Character>();
        // if (File.Exists(_filePath))
        // {
        //     string existingJson = File.ReadAllText(_filePath);
        //     characters = JsonSerializer.Deserialize<List<Character>>(existingJson) ?? new List<Character>();
        // }
        // characters.Add(character);
        // WriteAll(characters);

        throw new NotImplementedException("Implement JSON append logic");
    }

    public Character? FindByName(List<Character> characters, string name)
    {
        // The LINQ logic is the same as CSV - that's the beauty of interfaces!
        // The only difference is HOW we read the data, not how we search it.
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
