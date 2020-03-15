using QuizWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ParticipantController : ApiController
    {

        [HttpPost]
        [Route("api/LogOnModel")]
        public List<GetUserToken> LogOnModel(Account objAccount)
        {
            bool isUserExist = false;
            using (DBModel db = new DBModel())
            {
                List<GetUserToken> lstUserDetails = new List<GetUserToken>();
                isUserExist = db.Accounts.Any(u => u.Email == objAccount.Email && u.Password == objAccount.Password);

                if (isUserExist)
                {


                    Guid guid = Guid.NewGuid();

                    GetUserToken objGetUserToken = new GetUserToken();
                    objGetUserToken.Email = objAccount.Email;
                    objGetUserToken.Name = db.Accounts.Where(u => u.Email == objAccount.Email && u.Password == objAccount.Password).Select(n => n.Name).FirstOrDefault();
                    objGetUserToken.AccountTypeID = db.Accounts.Where(u => u.Email == objAccount.Email && u.Password == objAccount.Password).Select(a => a.AccountTypeID).FirstOrDefault();
                    objGetUserToken.Token = guid.ToString();

                    lstUserDetails.Add(objGetUserToken);
                    return lstUserDetails;

                  //  return Json(objGetUserToken, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    lstUserDetails.Add(null);
                    return lstUserDetails;

                }
            }
        }

        //[HttpPost]
        //[Route("api/GetCurrentParticipantID")]
        //public int GetCurrentParticipantID(Participant model)
        //{
        //    using (DBModel db = new DBModel())
        //    {
        //        var ParticipantID = db.Participants.Where(m => m.Email == model.Email).Where(m => m.Name == model.Name).Select(p => p.ParticipantID).FirstOrDefault();
        //        return ParticipantID;
        //    }
        //}

        [HttpPost]
        [Route("api/InsertParticipant")]
        public Participant Insert(Participant model)
        {
            using (DBModel db = new DBModel())
            {
                db.Participants.Add(model);
                db.SaveChanges();
                return model;
            }
        }

        [HttpPost]
        [Route("api/insertUsertoDB")]
        public Account insertUsertoDB(Account model)
        {
            using (DBModel db = new DBModel())
            {
                Boolean isUserExist = db.Accounts.Any(z => z.Email == model.Email);
                Account objAccount = new Account();
                if(!isUserExist){                        
                        objAccount.Email = model.Email;
                        objAccount.Password = model.Password;
                        objAccount.AccountTypeID = model.AccountTypeID;
                        objAccount.Name = model.Name;

                        db.Accounts.Add(objAccount);
                        db.SaveChanges();
                        return objAccount;
                  }
                  else{
                        objAccount = null;
                        return objAccount;
                      }
            }
        }
        

        [HttpPost]
        [Route("api/UpdateOutput")]
        public void UpdateOutput(Participant model)
        {
            using (DBModel db = new DBModel())
            {
                model.ParticipantID = db.Participants.OrderByDescending(i => i.ParticipantID).Select(p => p.ParticipantID).FirstOrDefault();
                db.Entry(model).State = System.Data.Entity.EntityState.Modified; 
                db.SaveChanges();
            }
        }

        [HttpPost]
        [Route("api/UpdateAttemptedQuestion")]
        public void UpdateAttemptedQuestion(ParticipantDetail objParticipantDetails)
        {
            using (DBModel db = new DBModel())
            {
                int ParticipantID = db.Participants.OrderByDescending(i => i.ParticipantID).Select(p => p.ParticipantID).FirstOrDefault();

                //int ParticipantID = (from a in db.Participants
                //                     orderby a.ParticipantID
                //                     select a.ParticipantID).First();


                objParticipantDetails.ParticipantID = ParticipantID;
                objParticipantDetails.SubmissionTime = System.DateTime.Now;

                //db.ParticipantDetails.InsertOnSubmit(objParticipantDetails);
                //db.SubmitChanges();

                db.ParticipantDetails.Add(objParticipantDetails);
                db.SaveChanges();
            }
        }

    }
}
