using WebApi.BookOperations.DeleteBook;
using FluentAssertions;
using Xunit;

namespace WebApi.UnitTests.BookOperationsTests
{
    public class DeleteBookCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdIsInvalid_Validator_ShouldReturnError(int bookId)
        {
            // arrange
            var command = new DeleteBookCommand(null);
            command.BookId = bookId;

            // act
            var validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenIdIsValid_Validator_ShouldNotReturnError()
        {
            var command = new DeleteBookCommand(null);
            command.BookId = 2;

            var validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}
