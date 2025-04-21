using FluentAssertions;

public class CreateAuthorDtoValidatorTests
{
    [Fact]
    public void WhenAuthorNameIsInvalid_Validator_ShouldReturnError()
    {
        var model = new CreateAuthorDto
        {
            FirstName = "",
            LastName = "",
            BirthDate = DateTime.Now.AddYears(1) // gelecekte
        };
        var validator = new CreateAuthorDtoValidator();
        var result = validator.Validate(model);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
