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
using System.Net;

namespace Fi.Ticket.Api.IntegrationTests.Controllers;

//[TestCaseOrderer("Fi.Ticket.Api.IntegrationTests.TicketTestCaseOrderer", "Fi.Ticket.Api.IntegrationTests")]
public class SamplesControllerTests : TicketScenariosBase//ticketsenario baseden t�remek zorunda Fi senaryo baseden t�r�yor 1 startup ve moclanm�� olarak d���nebiliriz.
{
    private const string basePath = "api/v1/Ticket/Samples";
    public SamplesControllerTests(TicketApplicationFactory fiTestApplicationFactory) : base(fiTestApplicationFactory)
    {
    }

    //ger�ekten veritaban�nda i�lem yapmamam�z gerekir.
    [Fact, Trait("Sample", "Integration")]
    public async Task GetByKey_IfRequestedItemExist_ReturnsSuccess_WithItem()//uygun bir isim �a��raca��m metod ismi ve neyi kontrol edeceksem onu yaz�p ne bekliyrsam onu yaz�yorum.
    {
        await EnsureEntityIsEmpty<Sample>();//bunu �al��t�rd���m�zda di�er datalarla di�er testlerle kar��mas�n� engellemek i�in.Burada sample i�in s�f�rlama yapt�k.

        var inputModel = Builder<SampleInputModel>.CreateNew()
                          .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();//sahte sample imput model olu�turduk.

        var responsePost = await HttpClient.FiPostTestAsync<SampleInputModel,SampleOutputModel>($"{basePath}", inputModel);//SampleInputModel g�nderece�im ve geriye SampleOutputModel bekliyorum.��nk� o post metoduna gitti�im zaman geriye SampleOutputModel d�n�yor.Basepath dedi�im yere controller�n adresini veriyorum. 

        var response = await HttpClient.FiGetTestAsync<SampleOutputModel>($"{basePath}/{responsePost.Value.Id}", false);//�dsi olu�mu� bir SampleOutputModel d�n�yor geriye ve getbykey in �al��mas� i�in id sini veriyorum ve sample � tekrar geri ald�m.

        response.FiShouldBeSuccessStatus(); //1 tane sample d�nm��se
        response.Value.ShouldNotBeNull();//null de�ilse
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

    [Fact, Trait("Sample", "Integration")]
     public async Task Delete_ReturnsSuccess_WhenItemIsDeleted()
     {
         await EnsureEntityIsEmpty<Sample>();

         var inputModel = Builder<SampleInputModel>.CreateNew()
                           .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();

         var response = await HttpClient.FiPostTestAsync<SampleInputModel, SampleOutputModel>($"{basePath}", inputModel);

         response.FiShouldBeSuccessStatus();
         response.Value.ShouldNotBeNull();


         var deleteResponse = await HttpClient.FiDeleteTestAsync($"{basePath}/{response.Value.Id}");
         deleteResponse.FiShouldBeSuccessStatus();
         var getResponse = await HttpClient.FiGetTestAsync<SampleOutputModel>($"{basePath}/{response.Value.Id}");
     }

    [Fact, Trait("Sample", "Integration")]
     public async Task Update_ReturnsSuccess_WhenItemIsUpdated()
     {
         await EnsureEntityIsEmpty<Sample>();
         var inputModel = Builder<SampleInputModel>.CreateNew()
                           .Build().AddFiDefaults().AddFiSmartEnums().AddFiML().AddSchemaDefaults();

         var response = await HttpClient.FiPostTestAsync<SampleInputModel, SampleOutputModel>($"{basePath}", inputModel);

        response.FiShouldBeSuccessStatus();
        response.Value.ShouldNotBeNull();

         var updatedModel = response.Value;
         updatedModel.Name = "Updated Name"; 

         var updateResponse = await HttpClient.FiPutTestAsync<SampleOutputModel>($"{basePath}/{updatedModel.Id}", updatedModel);

         updateResponse.FiShouldBeSuccessStatus();

         var getResponse = await HttpClient.FiGetTestAsync<SampleOutputModel>($"{basePath}/{updatedModel.Id}");

         getResponse.FiShouldBeSuccessStatus();
         getResponse.Value.ShouldNotBeNull();
         getResponse.Value.Id.ShouldEqual(updatedModel.Id);
         getResponse.Value.Name.ShouldEqual(updatedModel.Name);
     }

    [Fact, Trait("Sample", "Integration")]
    public async Task Get_WithInvalidId_ReturnsNotFound()
    {
        // Ge�ersiz bir id ile get i�lemi yap
        var invalidId = "invalid_id";
        var response = await HttpClient.FiGetTestAsync<SampleOutputModel>($"{basePath}/{invalidId}", false);

        // Do�ru hata kodunun d�nd���n� kontrol et
       
        response.ShouldBeNull();
    }
}



