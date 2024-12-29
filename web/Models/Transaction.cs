using System;
using System.ComponentModel.DataAnnotations;

namespace web.Models;
public class Transaction
{
    public int Id { get; set; }

    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }
    
    public ApplicationUser? User { get; set; }

}