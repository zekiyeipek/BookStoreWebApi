using FluentAssertions;
using WebApi.GenreOperations.DeleteGenre;

public class DeleteGenreCommandValidatorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void WhenIdIsInvalid_Validator_ShouldReturnError(int id)
    {
        var command = new DeleteGenreCommand(null) { GenreId = id };
        var validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
