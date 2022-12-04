using PsixLibrary.Entity;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Requests;

[RequestHandlerPath("/types-appeal")]
public class TypeAppealHandler: RequestHandler
{
    [Get("get")]
    public void GetTypesAppeal()
    {
        var types = TypeAppeal.GetAllTypesAppeal();
        if (!types.Any())
        {
            Send(new AnswerModel(false, null, 401, "incorrect request body"));
            return;
        }
        Send(new AnswerModel(true, new { typesAppeal = types }, null, null));
    }
}