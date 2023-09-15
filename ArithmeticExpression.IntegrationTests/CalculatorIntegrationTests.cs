using ArithmeticExpression.Core.Calculator;
using ArithmeticExpression.Host;
using ArithmeticExpression.IntegrationTests.Setup;

namespace ArithmeticExpression.IntegrationTests;


public class GetUsersIntegrationTests : TestingCaseFixture<Startup>, IDisposable
{
    [Fact(DisplayName = "Post calculator - valid request with mixed operators")]
    public async Task Post_CalculationExpressionRequest_ShouldReturnSuccessWithCorrectResult()
    {        
        var expression = "2+3*8-90/3";

        var httpResMsg = await Client.PostAsJsonAsync("/api/calculator", expression);
        httpResMsg.Should().NotBeNull();
        httpResMsg.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await httpResMsg.Content.ReadAsAsync<CalculationResponse>();

        response.IsSuccess.Should().BeTrue();
        response.Error.Should().BeNullOrEmpty();

        response.Result.Should().Be(-4);
    }
}