using Souk.Domain.Inventory.ValueObjects;

namespace Souk.Domain.Inventory.Entities;

public class Supplier
{

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string EmailAddress { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Supplier() { }

    public static Supplier Create(string name, EmailAddress emailAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(emailAddress);

        return new Supplier
        {
            Name = name,
            EmailAddress = emailAddress,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

        };
    }

    public void Update(string name, EmailAddress emailAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(emailAddress);
        Name = name;
        EmailAddress = emailAddress;
        UpdatedAt = DateTime.UtcNow;
    }
}