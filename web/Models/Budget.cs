using System.ComponentModel.DataAnnotations;

namespace web.Models;
public class Budget
{
    public int Id { get; set; }

    public required decimal Amount { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public Category? Category { get; set; }

    public ApplicationUser? User { get; set; }
}
