using PsixLibrary.Entity;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ExcelLibrary;
using WebServer.Models;

namespace WebServer.Requests
{
    [RequestHandlerPath("/answers")]
    public class AnswerHandler : RequestHandler
    {
        [Get("get-all-answers")]
        public void GetAllAnswers()
        {
            if (!Headers.TryGetValue("Access-Token", out var token) && !TokenWorker.CheckToken(token))
            {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            var user = TokenWorker.GetClientByToken(token);
            if (user is null)
            {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            var psychologist = Psychologist.GetPsychologistByUserId(user.ID);
            List<Answer> answers = Answer.GetAnswersByPsychologistId(psychologist.ID);

            if (!answers.Any())
            {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            List<PsixAnswerModel> answerModels = new List<PsixAnswerModel>();
            foreach (var a in answers)
            {
                answerModels.Add(new PsixAnswerModel(a));
            }

            Send(new AnswerModel(true, new { answers = answerModels }, null, null));
        }
        [ResponseTimeout(100000)]
        [Post("report")]
        public void GetReport()
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

            var body = Bind<DatesModel>();
            if (body is null || body.startdate is null || body.finishdate is null)
            {
                Send(new AnswerModel(false, null, 400, "incorrect request"));
                return;
            }
            var psychologist = Psychologist.GetPsychologistByUserId(user.ID);
            List<Answer> answers = Answer.GetAnswersByPsychologistId(psychologist.ID);
            
            if (!answers.Any())
            {
                Send(new AnswerModel(false, null, 401, "incorrect request body"));
                return;
            }

            answers = answers.Where(a =>
                a.DateTime >= DateTime.Parse(body.startdate) && a.DateTime <= DateTime.Parse(body.finishdate)).ToList();
            string path = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(path,$"Report.xlsx");
            using (var excel = new ExcelHelper())
            {
                try
                {
                     
                    if (excel.Open(filePath))
                    {
                        excel.SetAnswers(answers, body.startdate, body.finishdate);
                        excel.Save();
                        excel.Dispose();
                    }
                }
                catch (Exception ex) { }
            }
            
            var from = new MailAddress("auxilium_psixo@mail.ru", "Психологический центр");
            var to = new MailAddress(user.Email, "Пользователь");
            var msg = new MailMessage(from, to);
            msg.Subject = "Отчет по обращениям";
            msg.Attachments.Add(
                new Attachment(filePath));
            msg.Body = "Сформирован отчет по обработанным обращениям с " +DateTime.Parse(body.startdate).ToString("d") +" по " + DateTime.Parse(body.finishdate).ToString("d")+".";
            using (var smtp = new SmtpClient("smtp.mail.ru", 587))
            {
                smtp.Credentials = new NetworkCredential("auxilium_psixo@mail.ru", "wG9sjdgUrqbERZ8XuW5f");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            Send(new AnswerModel(true, null, null, null));
        }
        [ResponseTimeout(100000)]
        [Post("add-answer")]
        public void AddAnswer ()
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

            var body = Bind<PsixAnswerModel>();
            if (body is null || body.appeal is null || body.answertext is null)
            {
                Send(new AnswerModel(false, null, 400, "incorrect request"));
                return;
            }

            var appeal = Appeal.GetAppealById(body.appeal.id);
            var psychologist = Psychologist.GetPsychologistByUserId(user.ID);
            appeal.IsAnswered = true;
            
            Answer answer = new Answer(appeal, body.answertext, psychologist,DateParser.SetKindUtc(DateTime.Now));
            if (!answer.Add())
            {
                Send(new AnswerModel(false, null, 400, "incorrect request"));
                return;
            }
            CommitMessage(body.answertext, appeal.User);

            Send(new AnswerModel(true, null, null, null));
        }
        
        
        public static void CommitMessage(string answer, User user)
        {
            var from = new MailAddress("auxilium_psixo@mail.ru", "Психологический центр");
            var to = new MailAddress(user.Email, "Пользователь");

            var msg2 = new MailMessage(from, to);
            msg2.Subject = "Ответ от психологического центра";
            msg2.Body = $"Добрый день,{user.Surname} {user.Name}, вам пришел ответ на ваше обращение : {answer}";
            msg2.IsBodyHtml = true;
            using (var smtp = new SmtpClient("smtp.mail.ru", 587))
            {
                smtp.Credentials = new NetworkCredential("auxilium_psixo@mail.ru", "wG9sjdgUrqbERZ8XuW5f");
                smtp.EnableSsl = true;
                smtp.Send(msg2);
            }
        }
    }

    
}
