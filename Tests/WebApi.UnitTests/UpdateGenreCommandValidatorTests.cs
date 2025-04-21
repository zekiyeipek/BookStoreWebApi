using FluentAssertions;
using WebApi.GenreOperations.UpdateGenre;
public class UpdateGenreCommandValidatorTests
{
    [Fact]
    public void WhenModelIsInvalid_Validator_ShouldReturnError()
    {
        var command = new UpdateGenreCommand(null) {
            GenreId = 0,
            Model = new UpdateGenreModel { Name = "" }
        };
        var validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
