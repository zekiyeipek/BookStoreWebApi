using System;

public class CreateAuthorDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UpdateAuthorDto : CreateAuthorDto
{
    public int Id { get; set; }
}

public class AuthorDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
}
