using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizWebAPI.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WebAPI.Controllers
{
    public class QuizController : ApiController
    {
        [HttpGet]
        [Route("api/Questions")]
        public HttpResponseMessage GetQuestions() {
            using (DBModel db = new DBModel())
            {
                var Questions = db.Questions
                    .Select(x => new { QuizID = x.QuizID, Question = x.Ques, ImageName = x.ImageName, x.Option1, x.Option2, x.Option3, x.Option4 })
                    .OrderBy(y => Guid.NewGuid())
                    .Take(10)
                    .ToArray();
                var updated = Questions.AsEnumerable()
                    .Select(x => new
                    {
                        QuizID = x.QuizID,
                        Question = x.Question,
                        ImageName = x.ImageName,
                        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                    }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updated);
            }
        }

        [HttpPost]
        [Route("api/Answers")]
        public HttpResponseMessage GetAnswers(int[] qIDs) {
            using (DBModel db = new DBModel())
            {
               var result = db.Questions
                    .AsEnumerable()
                    .Where(y => qIDs.Contains(y.QuizID))
                    .OrderBy(x => { return Array.IndexOf(qIDs, x.QuizID); })
                    .Select(z => z.Answer)
                    .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }

        
        [HttpGet]
        [Route("api/GetSelectedOptionByCurrentUser")]
        public HttpResponseMessage GetSelectedOptionByCurrentUser()
        {
            using (DBModel db = new DBModel())
            {
                int ParticipantID = db.Participants.OrderBy(i => i.ParticipantID).Select(p => p.ParticipantID).FirstOrDefault();
                var result = db.ParticipantDetails
                    .AsEnumerable()
                    .Where(y => Convert.ToBoolean(y.ParticipantID))
                    .Select(z => z.SelectedOption)
                    .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }

        [HttpGet]
        [Route("api/GetUserGradebook")]
        public HttpResponseMessage GetUserGradebook()
        {
            using (DBModel db = new DBModel())
            {
                //var userGradebook = db.Participants.
                //Where(x => x.Score != null || x.TimeSpent != null)
                //.OrderBy(i => i.ParticipantID).Select(x => new { x.Name, x.Email, x.Score, x.TimeSpent })
                //.ToList();

               // var userGradebook1 = db.prcGetAllUsersGradeBook();

                var userGradebook = db.Database.SqlQuery<UserGradebook>(@"exec prcGetAllUsersGradeBook").ToList();

                return this.Request.CreateResponse(HttpStatusCode.OK, userGradebook);

            }
        }

    }
}
