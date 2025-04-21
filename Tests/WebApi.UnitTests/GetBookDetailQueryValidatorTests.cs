using FluentAssertions;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DbOperations;
using AutoMapper;
using Moq;
using Xunit;

namespace WebApi.UnitTests
{
    public class GetBookDetailQueryValidatorTests
    {
        [Fact]
        public void WhenBookIdIsInvalid_Validator_ShouldReturnError()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var query = new GetBookDetailQuery(null, mapper);
            query.BookId = 0;

            var validator = new GetBookDetailQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
