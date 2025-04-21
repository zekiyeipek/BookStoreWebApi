using System;
using System.Collections.Generic;
using WebApi;
public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
