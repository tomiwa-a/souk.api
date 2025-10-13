using System.Text.RegularExpressions;

namespace Souk.Domain.Inventory.ValueObjects;

public record EmailAddress
{
    private EmailAddress(string Value)
    {
        this.Value = Value;
    }

    public string Value { get; init; }

    public static EmailAddress Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new ArgumentException("Invalid email address format.", nameof(email));
        }

        return new EmailAddress(email);
    }
    
    public static implicit operator EmailAddress(string value) => Create(value);
    public static implicit operator string(EmailAddress email) => email.Value;
    public override string ToString() => Value;
}