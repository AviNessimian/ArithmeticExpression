using ArithmeticExpression.Host;
using ArithmeticExpression.IntegrationTests.Setup;

namespace ArithmeticExpression.IntegrationTests;

public class GetUsersIntegrationTests : TestingCaseFixture<Startup>, IDisposable
{
    //[Fact(DisplayName = "Get users with pagination")]
    //public async Task Get_CCC_ShouldReturnCCC()
    //{
    //    // Arrange
    //    var skipCount = 0;
    //    var pageSize = 1;

    //    // Act
    //    var getUserResponse = await Client.GetAsync($"/api/Users?skip={skipCount}&pageSize={pageSize}");
    //    var json = await getUserResponse.Content.ReadAsStringAsync();
    //    var users = JsonConvert.DeserializeObject<List<UserEntity>>(json);

    //    // Assert
    //    getUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    //    users.Should().NotBeNullOrEmpty();
    //    users.Should().HaveCount(pageSize);
    //}

    //    [Fact(DisplayName = "Get user with subjectId")]
    //    public async Task Get_ExistentUserWithSubjectId_ShouldReturnSubjectIdResult()
    //    {
    //        // Act & Arrange
    //        var userToCreate = UserFactory.CreateLocalUser(
    //            email: $"{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10)}@scuticode.com",
    //            password: "Um123456&",
    //            givenName: "givenName",
    //            familyName: "familyName");

    //        await UserWriteRepository.AddAsync(userToCreate);

    //        var getUserById = await Client.GetAsync($"/api/Users/{userToCreate.SubjectId}");
    //        var json = await getUserById.Content.ReadAsStringAsync();
    //        var actualUser = JsonConvert.DeserializeObject<UserEntity>(json) ?? new();

    //        // Assert
    //        getUserById.StatusCode.Should().Be(HttpStatusCode.OK);
    //        getUserById.Should().NotBeNull();
    //        actualUser.SubjectId.Should().Be(userToCreate.SubjectId);
    //        actualUser.Email.Should().Be(userToCreate.Email);
    //    }
}