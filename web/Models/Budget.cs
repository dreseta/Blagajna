using System.ComponentModel.DataAnnotations;

namespace web.Models;
public class Budget
{
    public int Id { get; set; }

    public required decimal Amount { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    // Foreign key to Category
    public required int CategoryId { get; set; }
    public required Category Category { get; set; }

    // Foreign key to User
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}
