using InboxAPI.Interfaces;
using InboxAPI.Models;
using InboxAPI.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Security.Claims;
using System.Web;
using System.Linq;

namespace InboxAPI.Controllers
{
    [RoutePrefix("Inbox")]
    [Authorize(Roles = "user")]
    public class InboxController : ApiController
    {
        IDataLayer _dataLayer = new DataLayer();

        /// <summary>
        /// Gets all labels
        /// </summary>
        /// <returns>List of all label</returns>
        [HttpGet]
        [Route("GetAllLabels")]
        public Response<List<Label>> GetAllLabels()
        {
            try
            {
                return new Response<List<Label>>() { Data = _dataLayer.getAllLabels(), IsSuccessful = true };
            }
            catch
            {
                return new Response<List<Label>>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }
        }

        /// <summary>
        /// Adds a label
        /// </summary>
        /// <param name="labelName">Label name</param>
        /// <returns>Label</returns>
        [HttpPost]
        [Route("AddLabel")]
        public Response<Label> AddLabel(string labelName)
        {
            if (string.IsNullOrEmpty(labelName))
                return new Response<Label>() { IsSuccessful = false, Error = HttpStatusCode.BadRequest.ToString() };

            try
            {
                var result = _dataLayer.AddLabel(labelName);

                if (result == null)
                    return new Response<Label>() { Error = HttpStatusCode.InternalServerError.ToString(), IsSuccessful = result.IsSuccessful };

                else if (!result.IsSuccessful)
                    return new Response<Label>() { IsSuccessful = result.IsSuccessful, Error = result.Message };

                return new Response<Label>() { IsSuccessful = true };
            }
            catch
            {
                return new Response<Label>() { Error = HttpStatusCode.InternalServerError.ToString(), IsSuccessful = false };
            }
        }

        /// <summary>
        /// Deletes a label
        /// </summary>
        /// <param name="labelName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteLabel")]
        public Response<OperationOutcome> DeleteLabel(string labelName)
        {
            try
            {
                var outcome = _dataLayer.DeleteLabel(labelName);

                if (!outcome.IsSuccessful)
                    return new Response<OperationOutcome>() { Error = outcome.Message, IsSuccessful = outcome.IsSuccessful };

                return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful };

            }
            catch
            {
                return new Response<OperationOutcome>() { Error = HttpStatusCode.InternalServerError.ToString(), IsSuccessful = false };
            }
        }

        /// <summary>
        /// Gets deleted messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDeletedMessages")]
        public Response<List<InboxMessageViewModel>> GetDeletedMessages()
        {
            try
            {
                var outcome = _dataLayer.GetDeletedMessages(GetAuthenticatedUserProfile().UserId);

                List<InboxMessageViewModel> result = new List<InboxMessageViewModel>();

                if (outcome.Count() > 0)
                    foreach (var message in outcome)
                    {
                        result.Add(new InboxMessageViewModel()
                        {
                            DateDeleted = message.DateDeleted,
                            DateReceived = message.DateReceived,
                            Deleted = message.Deleted,
                            FromAddress = message.FromAddress,
                            Id = message.Id,
                            Label = message.LabelId == null ? "" : _dataLayer.GetLabelbyID(message.LabelId).Name,
                            Message = message.Message,
                            ToAddress = message.ToAddress
                        });
                    }

                return new Response<List<InboxMessageViewModel>>() { IsSuccessful = true, Data = result };
            }
            catch
            {
                return new Response<List<InboxMessageViewModel>>() { Error = HttpStatusCode.InternalServerError.ToString(), IsSuccessful = false };
            }

        }

        /// <summary>
        /// Gets All inbox messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllMessages")]
        public Response<List<InboxMessageViewModel>> GetAllMessages()
        {
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var outcome = _dataLayer.GetAllMessages(GetAuthenticatedUserProfile().UserId);

                List<InboxMessageViewModel> result = new List<InboxMessageViewModel>();

                if (outcome.Count() > 0)
                    foreach (var message in outcome)
                    {
                        result.Add(new InboxMessageViewModel()
                        {
                            DateDeleted = message.DateDeleted,
                            DateReceived = message.DateReceived,
                            Deleted = message.Deleted,
                            FromAddress = message.FromAddress,
                            Id = message.Id,
                            Label = message.LabelId == null ? "" : _dataLayer.GetLabelbyID(message.LabelId).Name,
                            Message = message.Message,
                            ToAddress = message.ToAddress
                        });
                    }

                return new Response<List<InboxMessageViewModel>>() { Data = result, IsSuccessful = true };
            }

            catch
            {
                return new Response<List<InboxMessageViewModel>>() { Error = HttpStatusCode.InternalServerError.ToString(), IsSuccessful = false };
            }
        }

        /// <summary>
        /// Deletes label from inbox message
        /// </summary>
        /// <param name="mailId">message identifier</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteLabelfromEmail")]
        public Response<OperationOutcome> DeleteLabelfromEmail(int mailId)
        {
            try
            {
                var outcome = _dataLayer.DeleteLabelFromMail(GetAuthenticatedUserProfile().UserId, mailId);
                if (!outcome.IsSuccessful)
                    return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful, Error = outcome.Message };

                return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful };

            }
            catch
            {
                return new Response<OperationOutcome>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }
        }

        /// <summary>
        /// Adds label to inbox message
        /// </summary>
        /// <param name="label">label name</param>
        /// <param name="mailId">message identifier</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddLabelToEmail")]
        public Response<OperationOutcome> AddLabelToEmail(string label, int mailId)
        {
            if (string.IsNullOrEmpty(label))
                return new Response<OperationOutcome>() { Error = HttpStatusCode.BadRequest.ToString() };

            try
            {
                var outcome = _dataLayer.AddLabelToMail(GetAuthenticatedUserProfile().UserId, label, mailId);

                if (!outcome.IsSuccessful)
                    return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful, Error = outcome.Message };

                return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful };

            }
            catch
            {
                return new Response<OperationOutcome>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }
        }

        /// <summary>
        /// Deletes message
        /// </summary>
        /// <param name="messageId">Message identifier</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteMessage")]
        public Response<OperationOutcome> DeleteMessage(int messageId)
        {
            try
            {
                var outcome = _dataLayer.DeleteMessage(messageId, GetAuthenticatedUserProfile().UserId);
                if (!outcome.IsSuccessful)
                    return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful, Error = outcome.Message };

                return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful };

            }
            catch
            {
                return new Response<OperationOutcome>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }
        }

        /// <summary>
        /// Restore deleted messages
        /// </summary>
        /// <param name="messageId">Message identifier</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RetrieveMessage")]
        public Response<OperationOutcome> RetrieveDeletedMessage(int messageId)
        {
            try
            {
                var outcome = _dataLayer.RetrieveDeletedMessage(messageId, GetAuthenticatedUserProfile().UserId);
                if (!outcome.IsSuccessful)
                    return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful, Error = outcome.Message };

                return new Response<OperationOutcome>() { IsSuccessful = outcome.IsSuccessful };

            }
            catch
            {
                return new Response<OperationOutcome>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }
        }

        /// <summary>
        /// Gets inbox messages by label
        /// </summary>
        /// <param name="labelName">label name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInboxByLabel")]
        public Response<List<InboxMessageViewModel>> GetAllMessagesByLabel(string labelName)
        {
            try
            {
                var outcome = _dataLayer.GetAllMessagesByLabel(GetAuthenticatedUserProfile().UserId, labelName);

                List<InboxMessageViewModel> result = new List<InboxMessageViewModel>();

                if (outcome.Count() > 0)
                    foreach (var message in outcome)
                    {
                        result.Add(new InboxMessageViewModel()
                        {
                            DateDeleted = message.DateDeleted,
                            DateReceived = message.DateReceived,
                            Deleted = message.Deleted,
                            FromAddress = message.FromAddress,
                            Id = message.Id,
                            Label = message.LabelId == null ? "" : _dataLayer.GetLabelbyID(message.LabelId).Name,
                            Message = message.Message,
                            ToAddress = message.ToAddress
                        });
                    }

                return new Response<List<InboxMessageViewModel>>() { IsSuccessful = true, Data = result };
            }
            catch
            {
                return new Response<List<InboxMessageViewModel>>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }

        }

        /// <summary>
        /// Get inbox messages by email address
        /// </summary>
        /// <param name="originEmailAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInboxByLabel")]
        public Response<List<InboxMessageViewModel>> GetMessagesByAddress(string originEmailAddress)
        {
            try
            {
                var outcome = _dataLayer.GetMessagesByAddress(originEmailAddress);

                List<InboxMessageViewModel> result = new List<InboxMessageViewModel>();

                if (outcome.Count() > 0)
                    foreach (var message in outcome)
                    {
                        result.Add(new InboxMessageViewModel()
                        {
                            DateDeleted = message.DateDeleted,
                            DateReceived = message.DateReceived,
                            Deleted = message.Deleted,
                            FromAddress = message.FromAddress,
                            Id = message.Id,
                            Label = message.LabelId == null ? "" : _dataLayer.GetLabelbyID(message.LabelId).Name,
                            Message = message.Message,
                            ToAddress = message.ToAddress
                        });
                    }

                return new Response<List<InboxMessageViewModel>>() { IsSuccessful = true, Data = result };

            }
            catch
            {
                return new Response<List<InboxMessageViewModel>>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }

        }

        /// <summary>
        /// Send email 
        /// </summary>
        /// <param name="toMailAddresses">recipients separeted by ';'</param>
        /// <param name="label">label</param>
        /// <param name="message">message body</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendMail")]
        public Response<OperationOutcome> SendMail (string toMailAddresses, string label, string message)
        {
            try
            {
                var labelChek = _dataLayer.getAllLabels().FirstOrDefault(x => x.Name == label);

                if (labelChek == null)
                    _dataLayer.AddLabel(label); //create the label if it doesnt exist

                List<string> recipients = toMailAddresses.Split(';').ToList();
                _dataLayer.SendMail(GetAuthenticatedUserProfile(), message, recipients,label);

                return new Response<OperationOutcome>() { IsSuccessful = true };

            }
            catch
            {
                return new Response<OperationOutcome>() { IsSuccessful = false, Error = HttpStatusCode.InternalServerError.ToString() };
            }
        }

        private UserIdentityModel GetAuthenticatedUserProfile()
        {

            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            var fullNames = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            var user = _dataLayer.GetUserProfile(email, fullNames);

            return new UserIdentityModel() { EmailAddress = email, FullNames = fullNames, UserId = user.UserID };
        }

    }
}
