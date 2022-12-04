using PsixLibrary.Entity;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebServer.Models;

namespace WebServer.Requests
{
    [RequestHandlerPath("/appeal")]
    public class AppealHandler:RequestHandler
    {
        [Get("get-all-appeals")]
        public void GetAllAppeal()
        {
            if (!Headers.TryGetValue("Access-Token", out var token) && !TokenWorker.CheckToken(token)) {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }
            var user = TokenWorker.GetClientByToken(token);
            if (user is null) {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            List<Appeal> appeals = new List<Appeal>();
            if (user.IsPsychologist)
            {
                appeals.AddRange(Appeal.GetAllNotAnsweredAppeals().ToList());
            }

            if (!appeals.Any())
            {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            List<AppealModel> appealModels = new List<AppealModel>();
            foreach (var a in appeals)
            {
                appealModels.Add(new AppealModel(a));
            }
            Send(new AnswerModel(true, new { appeals = appealModels }, null, null));
        }
        
        [Get("history-appeal")]
        public void GetHistoryAppeal()
        {
            if (!Headers.TryGetValue("Access-Token", out var token) && !TokenWorker.CheckToken(token)) {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }
            var user = TokenWorker.GetClientByToken(token);
            if (user is null) {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }
            

            List<Appeal> appeals = Appeal.GetAllNotAnsweredAppealsByUserId(user.ID);
            List<Answer> answers = Answer.GetAnswersByUserId(user.ID);
            var typesAppeals = TypeAppeal.GetAllTypesAppeal();

            List<PsixAnswerModel> answerModels = new List<PsixAnswerModel>();
            List<AppealModel> appealModels = new List<AppealModel>();
            foreach (var a in answers)
            {
                answerModels.Add(new PsixAnswerModel(a));
            }

            foreach (var a in appeals)
            {
                appealModels.Add(new AppealModel(a));
            }
            if (Params.TryGetValue("type", out var typeAppeal) && typeAppeal != "")
            {
                answerModels = answerModels.Where(a => a.appeal.typeappeal ==HttpUtility.UrlDecode(typeAppeal)).ToList();
                appealModels = appealModels.Where(a => a.typeappeal == HttpUtility.UrlDecode(typeAppeal)).ToList();
            }
            
            Send(new AnswerModel(true, new { answers = answerModels, appeals = appealModels, typesAppeal = typesAppeals  }, null, null));//в каждом из answers содержится appealModel, appeals - это неотвеченные обращения
        }
        
        
        [Post("add-appeal")]
        public void AddAppeal()
        {
            if (!Headers.TryGetValue("Access-Token", out var token) && !TokenWorker.CheckToken(token)) {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }
            var user = TokenWorker.GetClientByToken(token);
            if (user is null) {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            var body = Bind<AppealModel>();
            if (body is null || body.text is null || body.typeappeal is null)
            {
                Send(new AnswerModel(false, null, 400, "incorrect request"));
                return;
            }

            var typeAppeal = TypeAppeal.GetTypeAppealByName(body.typeappeal);
            Appeal appeal = new Appeal(typeAppeal, user, body.text, false, DateParser.SetKindUtc(DateTime.Now));
            if (!appeal.Add())
            {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }
            Send(new AnswerModel(true, null, null, null));
        }
    }
}
