using CustomMVC.App.Core.Http;
using CustomMVC.App.MVC.Controllers.Common.ModelBinding.Binders;
using CustomMVC.App.MVC.Controllers.Routing;
using Moq;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Xunit;

public class QueryBinderTests
{
    public class SampleDto
    {
        public string Name { get; set; }
        public int Password { get; set; }
    }

    public class MockedUri : Uri
    {
        public MockedUri([StringSyntax("Uri")] string uriString) : base(uriString)
        {
            
        }

        public new string Query {  get; set; }
    }

    [Fact]
    public void Bind_OnSimpleObject_ExcpectSuccess()
    {
        var parameterDescriptor = new Mock<ParameterDescriptor>();

        parameterDescriptor.SetupGet(p => p.ParameterType).Returns(typeof(string));
        parameterDescriptor.SetupGet(p => p.Name).Returns("Name");

        var requestMock = new Mock<HttpRequest>();
        requestMock.SetupGet(r => r.Uri)
                   .Returns(new MockedUri("http://localhost:8888/"));

        requestMock.SetupGet(r => r.Uri.Query)
            .Returns("?Name=Dima&Passwordd=1234");

        var contextMock = new Mock<HttpContext>(null, null);
        contextMock.SetupGet(c => c.Request).Returns(requestMock.Object);

        var binder = new FromQueryBinder();

        var result = binder.Bind(contextMock.Object, parameterDescriptor.Object);

        Assert.Equal("Dima", result?.ToString());
    }

    [Fact]
    public void Bind_OnDtoClass_ExcpectSuccess()
    {
        var parameterDescriptor = new Mock<ParameterDescriptor>();

        parameterDescriptor.SetupGet(p => p.ParameterType).Returns(typeof(SampleDto));
        parameterDescriptor.SetupGet(p => p.Name).Returns("SampleDto");

        var requestMock = new Mock<HttpRequest>();
        requestMock.SetupGet(r => r.Uri)
                   .Returns(new Uri("http://localhost/?Name=Dima&Password=1234"));

        var contextMock = new Mock<HttpContext>(null, null);
        contextMock.SetupGet(c => c.Request).Returns(requestMock.Object);

        var binder = new FromQueryBinder();

        var result = binder.Bind(contextMock.Object, parameterDescriptor.Object) as SampleDto;

        Assert.Equal("Dima", result?.Name);
        Assert.Equal(1234, result?.Password);
    }
}