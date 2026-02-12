# Week 4: Open/Closed Principle (OCP) & Interfaces

> **Template Purpose:** This template represents a working solution through Week 3. Use YOUR repo if you're caught up. Use this as a fresh start if needed.

---

## Overview

This week introduces **interfaces** and the **Open/Closed Principle (OCP)**: software should be open for extension but closed for modification. You'll create an `IFileHandler` interface that combines your Week 3 reading and writing logic, then add a JSON implementation. This is a key concept - when your boss says "we need JSON support," you can add it without breaking existing CSV code.

## The Big Picture: Building Toward Databases

```
Week 3:  CharacterReader + CharacterWriter  (concrete classes, CSV only)
            ↓
Week 4:  IFileHandler  (interface!)
            ├── CsvFileHandler   ← your Week 3 logic
            └── JsonFileHandler  ← NEW!
            ↓
Week 9:  DbContext  (same pattern, but with SQL Server!)
```

The pattern you learn this week - swapping implementations without changing business logic - is **exactly** how Entity Framework Core works. When you reach Week 9, you'll recognize the pattern immediately!

---

## Learning Objectives

By completing this assignment, you will:
- [ ] Understand and apply the Open/Closed Principle
- [ ] Create an interface from existing classes
- [ ] Add a new implementation (JSON) without modifying existing code (CSV)
- [ ] See how interfaces enable extensibility

## Prerequisites

Before starting, ensure you have:
- [ ] Completed Week 3 assignment (or are using this template)
- [ ] Working CharacterReader and CharacterWriter classes
- [ ] Understanding of classes, methods, and LINQ basics

## What's New This Week

| Concept | Description |
|---------|-------------|
| OCP | Open for extension, closed for modification |
| Interface | A contract that classes must implement |
| `IFileHandler` | Interface for all file operations |
| `CsvFileHandler` | CSV implementation (your Week 3 code) |
| `JsonFileHandler` | NEW: JSON implementation |

---

## Assignment Tasks

### Task 1: Create the IFileHandler Interface

**What to do:**
- Create `IFileHandler.cs` that combines the methods from CharacterReader and CharacterWriter
- This defines the "contract" all file handlers must follow

**Example:**
```csharp
public interface IFileHandler
{
    // From CharacterReader
    List<Character> ReadAll();
    Character? FindByName(List<Character> characters, string name);
    List<Character> FindByProfession(List<Character> characters, string profession);

    // From CharacterWriter
    void WriteAll(List<Character> characters);
    void AppendCharacter(Character character);
}
```

**Why combine them?**
- A file handler naturally does both reading AND writing
- This mirrors how `DbContext` works in Week 9 (one class for all data operations)
- Simpler to swap implementations when it's one interface

### Task 2: Implement CsvFileHandler

**What to do:**
- Create `CsvFileHandler.cs` that implements `IFileHandler`
- Copy your Week 3 CharacterReader and CharacterWriter logic into this class

**Example:**
```csharp
public class CsvFileHandler : IFileHandler
{
    private readonly string _filePath;

    public CsvFileHandler(string filePath)
    {
        _filePath = filePath;
    }

    public List<Character> ReadAll()
    {
        // Your Week 3 CharacterReader.ReadAll() logic
    }

    public void WriteAll(List<Character> characters)
    {
        // Your Week 3 CharacterWriter.WriteAll() logic
    }

    public void AppendCharacter(Character character)
    {
        // Your Week 3 CharacterWriter.AppendCharacter() logic
    }

    public Character? FindByName(List<Character> characters, string name)
    {
        // Your Week 3 LINQ logic
        return characters.FirstOrDefault(c =>
            c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Character> FindByProfession(List<Character> characters, string profession)
    {
        // Your Week 3 LINQ logic
        return characters.Where(c =>
            c.Profession.Equals(profession, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
```

### Task 3: Implement JsonFileHandler

**What to do:**
- Create `JsonFileHandler.cs` that implements `IFileHandler`
- Use `System.Text.Json` for JSON handling
- The LINQ methods (FindByName, FindByProfession) are IDENTICAL to CSV!

**Example:**
```csharp
using System.Text.Json;

public class JsonFileHandler : IFileHandler
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public JsonFileHandler(string filePath)
    {
        _filePath = filePath;
    }

    public List<Character> ReadAll()
    {
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Character>>(json) ?? new List<Character>();
    }

    public void WriteAll(List<Character> characters)
    {
        string json = JsonSerializer.Serialize(characters, _options);
        File.WriteAllText(_filePath, json);
    }

    public void AppendCharacter(Character character)
    {
        // JSON doesn't support simple append - must read, add, write
        var characters = ReadAll();
        characters.Add(character);
        WriteAll(characters);
    }

    // LINQ methods are identical to CSV - the interface ensures consistency!
    public Character? FindByName(List<Character> characters, string name)
    {
        return characters.FirstOrDefault(c =>
            c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Character> FindByProfession(List<Character> characters, string profession)
    {
        return characters.Where(c =>
            c.Profession.Equals(profession, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
```

### Task 4: Update Program to Use Interface

**What to do:**
- Declare your file handler as `IFileHandler` type
- The program doesn't care which implementation it's using!

**Example:**
```csharp
// Program uses interface - it doesn't know (or care) if it's CSV or JSON!
IFileHandler fileHandler = new JsonFileHandler("input.json");
var characters = fileHandler.ReadAll();

// Later...
fileHandler.WriteAll(characters);
```

---

## Stretch Goal (+10%)

**Switch Formats at Runtime**

Add a menu option to switch between CSV and JSON without restarting:

```
1. Display Characters
2. Find Character
3. Add Character
4. Level Up Character
5. Switch File Format (CSV/JSON)
0. Exit
```

```csharp
// Switch handler based on user choice
if (userChoice == "json")
    fileHandler = new JsonFileHandler("input.json");
else
    fileHandler = new CsvFileHandler("input.csv");
```

---

## JSON File Format

Your JSON file should look like:
```json
[
  {
    "Name": "John",
    "Profession": "Fighter",
    "Level": 1,
    "HP": 10,
    "Equipment": ["sword", "shield", "potion"]
  },
  {
    "Name": "Jane",
    "Profession": "Wizard",
    "Level": 2,
    "HP": 6,
    "Equipment": ["staff", "robe", "book"]
  }
]
```

---

## Project Structure

```
YourProjectName/
├── Program.cs                    # Uses IFileHandler interface
├── Models/
│   └── Character.cs              # Same as Week 3
├── Interfaces/
│   └── IFileHandler.cs           # NEW: The interface
├── Services/
│   ├── CsvFileHandler.cs         # CSV implementation (Week 3 code)
│   └── JsonFileHandler.cs        # NEW: JSON implementation
└── Files/
    ├── input.csv                 # CSV data file
    └── input.json                # JSON data file
```

---

## The Power of OCP

**Before (violates OCP):**
```csharp
// Adding XML support requires modifying existing code
if (format == "csv") { /* csv logic */ }
else if (format == "json") { /* json logic */ }
else if (format == "xml") { /* must add here - MODIFYING! */ }
```

**After (follows OCP):**
```csharp
// Adding XML support = create new class, no existing code changes!
IFileHandler handler = new XmlFileHandler("input.xml");  // Just add new class!
// CsvFileHandler and JsonFileHandler are UNTOUCHED
```

---

## Grading Rubric

| Criteria | Points | Description |
|----------|--------|-------------|
| IFileHandler Interface | 20 | Properly defined with all methods |
| CsvFileHandler | 25 | Correctly implements interface with Week 3 logic |
| JsonFileHandler | 25 | Correctly implements interface for JSON |
| OCP Compliance | 15 | Program uses interface, not concrete classes |
| LINQ Methods | 5 | FindByName and FindByProfession work correctly |
| Code Quality | 10 | Clean, readable, follows patterns |
| **Total** | **100** | |
| **Stretch: Format Switching** | **+10** | Switch formats via menu at runtime |

---

## How This Connects to the Final Project

This is important - the pattern you're learning here **evolves** through the semester:

| Week | Pattern | What It Does |
|------|---------|--------------|
| Week 4 | `IFileHandler` | Single entity (Characters), CSV/JSON |
| Week 7 | `IContext` | Multiple entities + `SaveChanges()` |
| Week 9 | `DbContext` | Real database with EF Core |

**The progression:**
```
IFileHandler (this week)
    └── ReadAll(), WriteAll(), Find methods for Characters
            ↓
IContext (Week 7 midterm prep)
    └── Players, Monsters, Items + SaveChanges()
            ↓
DbContext (Week 9)
    └── DbSet<Player>, DbSet<Monster> + SaveChanges()
```

When you learn Entity Framework Core, you'll recognize the pattern immediately!

---

## Tips

- Start by creating the interface - list all methods from Week 3's Reader + Writer
- Implement CSV first (it's just your Week 3 code reorganized)
- JSON is easier to debug (human-readable format)
- Use `JsonSerializerOptions { WriteIndented = true }` for readable JSON output
- The LINQ methods (FindByName, FindByProfession) are IDENTICAL across implementations!

---

## Submission

1. Commit your changes with a meaningful message
2. Push to your GitHub Classroom repository
3. Submit the repository URL in Canvas

---

## Resources

- [C# Interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/)
- [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview)
- [Open/Closed Principle](https://stackify.com/solid-design-open-closed-principle/)

---

## Need Help?

- Post questions in the Canvas discussion board
- Attend office hours
- Review the in-class repository for additional examples
