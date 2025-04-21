using FluentAssertions;
using WebApi.BookOperations.UpdateBook;

public class UpdateBookCommandValidatorTests
{
    [Fact]
    public void WhenModelIsInvalid_Validator_ShouldReturnError()
    {
        var command = new UpdateBookCommand(null) {
            BookId = 0,
            Model = new UpdateBookModel { Title = "", GenreId = 0 }
        };
        var validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
