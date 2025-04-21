using WebApi.GenreOperations.GetGenreDetail;
using WebApi.DbOperations;
using FluentAssertions;
public class GetGenreDetailQueryValidatorTests
{
    [Theory]
    [InlineData(0)]
    public void WhenIdIsInvalid_Validator_ShouldReturnError(int id)
    {
        var query = new GetGenreDetailQuery(null) { GenreId = id };
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
