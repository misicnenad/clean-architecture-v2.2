using Autofac;
using Autofac.Extensions.DependencyInjection;

using CleanArchitecture.API;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Moq;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

namespace CleanArchitecture.Tests.IntegrationTests
{
    public class TasksControllerTests
    {
        private const string _apiUrl = "api/users";
        private readonly IWebHostBuilder _hostBuilder;

        public TasksControllerTests()
        {
            _hostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services => services.AddAutofac());
        }

        [Fact]
        public async Task CreateUser_ReturnsOk()
        {
            // Arrange
            _hostBuilder.ConfigureTestContainer<ContainerBuilder>(builder =>
            {
                var mockIdNumberService = new Mock<IIdNumberValidationService>();
                mockIdNumberService.Setup(service =>
                        service.IsIdNumberValid(It.IsAny<string>()))
                    .ReturnsAsync(true);

                builder.Register(c => mockIdNumberService.Object)
                    .As<IIdNumberValidationService>();
            });

            var server = new TestServer(_hostBuilder);
            var client = server.CreateClient();

            // Act
            var validIdNumber = "12345";
            var response = await client.PostAsJsonAsync($"{_apiUrl}", new User
            {
                FirstName = "John",
                LastName = "Doe",
                IdNumber = validIdNumber
            });

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateUser_With_Invalid_ID_Number_Returns_BadRequest()
        {
            // Arrange
            _hostBuilder.ConfigureTestContainer<ContainerBuilder>(builder =>
            {
                var mockIdNumberService = new Mock<IIdNumberValidationService>();
                mockIdNumberService.Setup(service =>
                        service.IsIdNumberValid(It.IsAny<string>()))
                    .ReturnsAsync(false);

                builder.Register(c => mockIdNumberService.Object)
                    .As<IIdNumberValidationService>();
            });

            var server = new TestServer(_hostBuilder);
            var client = server.CreateClient();

            // Act
            var validIdNumber = "12345";
            var response = await client.PostAsJsonAsync($"{_apiUrl}", new User
            {
                FirstName = "John",
                LastName = "Doe",
                IdNumber = validIdNumber
            });

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
