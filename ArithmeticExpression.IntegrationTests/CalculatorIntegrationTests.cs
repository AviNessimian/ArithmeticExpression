using ArithmeticExpression.Core.Calculator;
using ArithmeticExpression.Host;
using ArithmeticExpression.IntegrationTests.Setup;

namespace ArithmeticExpression.IntegrationTests;


public class GetUsersIntegrationTests : TestingCaseFixture<Startup>, IDisposable
{


    [Theory(DisplayName = "Post calculator - valid request with mixed operators")]
    [InlineData("1+2", 3)]
    [InlineData("8-2", 6)]
    [InlineData("8+5", 13)]
    [InlineData("12+45", 57)]
    [InlineData("123+35", 158)]
    [InlineData("2+2*2", 6)]
    [InlineData("12+5*7/4-2", 18.75)]
    [InlineData("3.5+12.3 ", 15.8)]
    [InlineData("0.5*8 ", 4)]
    public async Task Post_CalculationExpressionRequest_ShouldReturnSuccessWithCorrectResult(string expression, double result)
    {
        var httpResMsg = await Client.PostAsJsonAsync("/api/calculator", expression);
        httpResMsg.Should().NotBeNull();
        httpResMsg.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await httpResMsg.Content.ReadAsAsync<CalculationResponse>();

        response.IsSuccess.Should().BeTrue();
        response.Error.Should().BeNullOrEmpty();

        response.Result.Should().Be(result);
    }
}