using Lodging.DataAccess;
using Lodging.DataAccess.Repositories;
using Lodging.Models;
using Lodging.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingControllerTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;
    private readonly LodgingController _controller;
    private readonly UnitOfWork _unitOfWork;
    private readonly string locationUrl = "";
    public LodgingControllerTest()
    {
      var contextMock = new Mock<LodgingContext>(_options);
      var loggerMock = new Mock<ILogger<LodgingController>>();
      var repositoryMock = new Mock<Repository<LodgingModel>>(new LodgingContext(_options));
      var unitOfWorkMock = new Mock<UnitOfWork>(contextMock.Object);
      var mockUrlHelper = new Mock<IUrlHelper>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(1));
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<LodgingModel>())).Returns(Task.FromResult<LodgingModel>(null));
      repositoryMock.Setup(m => m.SelectAsync()).Returns(Task.FromResult<IEnumerable<LodgingModel>>(null));
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).Returns(Task.FromResult<LodgingModel>(null));
      repositoryMock.Setup(m => m.Update(It.IsAny<LodgingModel>()));
      mockUrlHelper.Setup(x => x.RouteUrl(It.IsAny<UrlRouteContext>()))
      .Returns(locationUrl);
      unitOfWorkMock.Setup(m => m.Lodging).Returns(repositoryMock.Object);

      _unitOfWork = unitOfWorkMock.Object;
      _controller = new LodgingController(_unitOfWork);
      _controller.Url = mockUrlHelper.Object;
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.DeleteAsync(0);
      var resultPass = await _controller.DeleteAsync(1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.GetAsync();
      var resultFail = await _controller.GetAsync(0);
      var resultOne = await _controller.GetAsync(1);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.PostAsync(new LodgingModel() {Id = 1});

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.PutAsync(new LodgingModel());

      Assert.NotNull(resultPass);
    }
  }
}