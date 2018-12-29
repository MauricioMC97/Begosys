using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BegoSys.Service.Controllers;
using BegoSys.Core;
using BegoSys.TO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;


namespace BegoSys.Service.Tests
{
    [TestClass]
    public class OperationControllerTests
    {
        //[TestMethod]
        //public void ConsultarDatLocal1()
        //{
        //    //token         
        //    var clientToken = new RestClient("http://localhost:50943/");

        //    var requestToken = new RestRequest("token", Method.POST);
        //    requestToken.AddParameter("grant_type", "password");
        //    requestToken.AddParameter("username", "apiuser_sgdhm");
        //    requestToken.AddParameter("password", "mvm2017*");

        //    // execute the request
        //    IRestResponse responseToken = clientToken.Execute(requestToken);

        //    TokenObj obj = JsonConvert.DeserializeObject<TokenObj>(responseToken.Content);

        //    var client = new RestClient("http://localhost:50943//api/Operation");

        //    var request = new RestRequest("ConsultarDatosLocal", Method.GET);

        //    request.AddHeader("Authorization", obj.token_type + " " + obj.access_token);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("id", "1");

        //    // execute the request
        //    IRestResponse response = client.ExecuteAsGet(request, "GET");

        //    var objEstadoRegistro = response.Content;
        //}

        private OperationController controller = new OperationController(new OperationRepository());

        [TestMethod]
        public async Task ConsultarDatosLocalTest()
        {
            //arrange
            long idLocal = 1;
            long idProceso = 1;

            //act
            OkNegotiatedContentResult<DatosLocalTO> resultadoConvertido = controller.ConsultarDatosLocal(idLocal) as OkNegotiatedContentResult<DatosLocalTO>;

            //assert
            Assert.IsNotNull(resultadoConvertido);
            Assert.IsNotNull(resultadoConvertido.Content);
        }
    }
}
