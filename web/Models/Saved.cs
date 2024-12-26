using System;
using System.ComponentModel.DataAnnotations;

namespace web.Models;
public class Saved
{
    public int Id { get; set; }

    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }

    public ApplicationUser? User { get; set; }

}