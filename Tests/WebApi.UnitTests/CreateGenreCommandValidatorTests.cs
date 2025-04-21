using WebApi.GenreOperations.CreateGenre;
using WebApi.GenreOperations.DeleteGenre;
using WebApi.GenreOperations.UpdateGenre;
using WebApi.GenreOperations.GetGenreDetail;
using FluentAssertions;
public class CreateGenreCommandValidatorTests
{
    [Fact]
    public void WhenNameIsEmpty_Validator_ShouldReturnError()
    {
        var command = new CreateGenreCommand(null) {
            Model = new CreateGenreModel { Name = "" }
        };
        var validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
