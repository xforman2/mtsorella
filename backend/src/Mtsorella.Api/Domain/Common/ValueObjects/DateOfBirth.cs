namespace Mtsorella.Api.Domain.Common.ValueObjects;

// FR-P28 — a child's date of birth on the application form.
public readonly record struct DateOfBirth(DateOnly Value)
{
    public int AgeOn(DateOnly today)
    {
        int age = today.Year - Value.Year;
        if (Value > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
