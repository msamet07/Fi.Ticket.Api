using Fi.Ticket.Api.Domain.Entity;
using Fi.Ticket.Api.IntegrationTests.Initialization;
using Fi.Ticket.Api.Persistence;
using Fi.Ticket.Schema.Model;
using Fi.Infra.Exceptions;
using Fi.Infra.Schema.Const;
using Fi.Infra.Schema.Model;
using Fi.Mediator.Message;
using Fi.Persistence.Relational.Interfaces;
using Fi.Test.Extensions;
using Fi.Test.IntegrationTests.IntegrationTestHelper;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Should;
using Xunit;
using Fi.Ticket.Api.Controllers;
using Fi.ApiBase.Controller;
using Fi.Infra.Abstraction;
using Fi.Infra.Context.Interfaces;
using Fi.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using Moq;
using Fi.Mediator.Interfaces;
using Fi.Infra.Options;
using static EA;

namespace Fi.Ticket.Api.IntegrationTests.Controllers;

//[TestCaseOrderer("Fi.Ticket.Api.IntegrationTests.TicketTestCaseOrderer", "Fi.Ticket.Api.IntegrationTests")]
public class SamplesControllerTests : TicketScenariosBase
{
    private const string basePath = "api/v1/Ticket/Samples";
    public SamplesControllerTests(TicketApplicationFactory fiTestApplicationFactory) : base(fiTestApplicationFactory)
    {
    }


    [Fact, Trait("Sample", "Integration")]
    public async Task GetByKey_IfRequestedItemExist_ReturnsSuccess_WithItem()
    {
        await EnsureEntityIsEmpty<Sample>();

        var inputModel = Builder<SampleInputModel>.CreateNew()
                          .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();

        var responsePost = await HttpClient.FiPostTestAsync<SampleInputModel,SampleOutputModel>($"{basePath}", inputModel);

        var response = await HttpClient.FiGetTestAsync<SampleOutputModel>($"{basePath}/{responsePost.Value.Id}", false);

        response.FiShouldBeSuccessStatus(); 
        response.Value.ShouldNotBeNull();
        response.Value.Id.ShouldEqual(responsePost.Value.Id);
    }


    [Fact, Trait("Sample", "Integration")]
    public async Task GetAllList_ReturnsSuccess_WithItems()
    {
        await EnsureEntityIsEmpty<Sample>();

        var inputModel = Builder<SampleInputModel>.CreateNew()
                          .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();

        var inputModelTwo = Builder<SampleInputModel>.CreateNew()
                          .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();
        inputModelTwo.Id = 2;
        inputModelTwo.Code = "DER";

        await HttpClient.FiPostTestAsync<SampleInputModel, SampleOutputModel>($"{basePath}", inputModel);

        await HttpClient.FiPostTestAsync<SampleInputModel, SampleOutputModel>($"{basePath}", inputModelTwo);

        var response = await HttpClient.FiGetTestAsync<List<SampleOutputModel>>($"{basePath}", false);

        response.FiShouldBeSuccessStatus();
        response.Value.ShouldNotBeNull();
        response.Value.ShouldNotBeEmpty();
        Assert.Equal(response.Value.Count, 2);
    }

    [Fact, Trait("Sample", "Integration")]
    public async Task GetBySampleCode_ReturnsSuccess_WithItems()
    {
        await EnsureEntityIsEmpty<Sample>();

        var inputModel = Builder<SampleInputModel>.CreateNew()
                          .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();

        await HttpClient.FiPostTestAsync<SampleInputModel, SampleOutputModel>($"{basePath}", inputModel);
        var response = await HttpClient.FiGetTestAsync<List<SampleOutputModel>>($"{basePath}/BySampleCode/{inputModel.Code}", false);

        response.FiShouldBeSuccessStatus();
        response.Value.ShouldNotBeNull();
        response.Value.ShouldNotBeEmpty();
    }


    [Fact, Trait("Sample", "Integration")]
    public async Task Create_ReturnsSuccess_WithItem()
    {
        await EnsureEntityIsEmpty<Sample>();

        var inputModel = Builder<SampleInputModel>.CreateNew()
                          .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();

        var response = await HttpClient.FiPostTestAsync<SampleInputModel, SampleOutputModel>($"{basePath}", inputModel);

        response.FiShouldBeSuccessStatus();
        response.Value.ShouldNotBeNull();
        response.Value.Id.ShouldEqual(inputModel.Id);
        response.Value.Code.ShouldEqual(inputModel.Code);

    }


}



