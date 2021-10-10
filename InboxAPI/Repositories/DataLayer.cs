using System;
using System.Collections.Generic;
using System.Linq;
using InboxAPI.Interfaces;
using InboxAPI.Models;

namespace InboxAPI.Repositories
{
    public class DataLayer : IDataLayer
    {
        EmailServerDBEntities _inbox;

        public DataLayer()
        {
            _inbox = new EmailServerDBEntities();
        }
        public UserProfile Validateuser(string username, string password)
        {
            return _inbox.UserProfiles.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        #region label

        public DataOperationOutcome AddLabel(string labelName)
        {
            var label = _inbox.Labels.FirstOrDefault(x => x.Name == labelName);

            if (label != null)
                return new DataOperationOutcome() { IsSuccessful = false, Message = "Label already exist." };

            _inbox.Labels.Add(new Label()
            {
                DateCreated = DateTime.Now,
                Name = labelName
            });
            _inbox.SaveChanges();
            return DataOperationOutcome.Success;
        }

        public List<Label> getAllLabels()
        {
            return _inbox.Labels.ToList();
        }

        public DataOperationOutcome DeleteLabel(string labelName)
        {
            var label = _inbox.Labels.FirstOrDefault(x => x.Name == labelName);

            if (label == null)
                return new DataOperationOutcome() { IsSuccessful = false, Message = "Label does not exist." };

            _inbox.Labels.Remove(label);
            _inbox.SaveChanges();

            return DataOperationOutcome.Success;
        }

        #endregion


        #region Deletes

        public List<InboxMessage> GetDeletedMessages(Guid userID)
        {
            List<InboxMessage> messages = new List<InboxMessage>();
            messages = _inbox.InboxMessages.Where(x => x.UserId == userID && x.Deleted == true).ToList();

            return messages;
        }


        public DataOperationOutcome DeleteMessage(int messageId, Guid userId)
        {
            var messageResult = _inbox.InboxMessages.FirstOrDefault(x => x.Id == messageId && x.UserId == userId);

            if (messageResult == null)
                return new DataOperationOutcome() { IsSuccessful = false, Message = "Inbox Message not found." };

            messageResult.Deleted = true;
            messageResult.DateDeleted = DateTime.Now;
            _inbox.SaveChanges();

            return DataOperationOutcome.Success;
        }


        public DataOperationOutcome RetrieveDeletedMessage(int messageId, Guid userId)
        {
            var messageResult = _inbox.InboxMessages.FirstOrDefault(x => x.Id == messageId && x.UserId == userId && x.Deleted == true);

            if (messageResult == null)
                return new DataOperationOutcome() { IsSuccessful = false, Message = "Inbox Message not found." };

            else if (!messageResult.Deleted)
                return new DataOperationOutcome() { IsSuccessful = false, Message = "Message is not deleted." };

            messageResult.Deleted = false;
            messageResult.DateDeleted = null;
            _inbox.SaveChanges();

            return DataOperationOutcome.Success;

        }

        #endregion


        #region Inbox

        public List<InboxMessage> GetAllMessages(Guid  userid)
        {
            return _inbox.InboxMessages.Where(x => x.UserId == userid).ToList();
        }

        public List<InboxMessage> GetAllMessagesByLabel (Guid userId, string label)
        {
            var labelId = GetLabelID(label);
            return _inbox.InboxMessages.Where(x => x.UserId == userId && x.LabelId == labelId).ToList();

        }

        public DataOperationOutcome DeleteLabelFromMail (Guid userId, int messageId)
        {
            var outcome = _inbox.InboxMessages.FirstOrDefault(x => x.Id == messageId && x.UserId == userId);

            if (outcome == null)
            {
                return new DataOperationOutcome() { IsSuccessful = false, Message = "Message not found" };
            }
            outcome.LabelId = null;
            _inbox.SaveChanges();

            return new DataOperationOutcome() { IsSuccessful = true, Message = "Label deleted" };
        }

        public DataOperationOutcome AddLabelToMail(Guid userId,string newLabelId, int messageId)
        {
            var outcome = _inbox.InboxMessages.FirstOrDefault(x => x.Id == messageId && x.UserId == userId);

            if (outcome == null)
            {
                return new DataOperationOutcome() { IsSuccessful = false, Message = "Message not found" };
            }
            var labelId = GetLabelID(newLabelId);
            outcome.LabelId = GetLabelbyID(labelId).ID;
            _inbox.SaveChanges();

            return new DataOperationOutcome() { IsSuccessful = true, Message = "Label updated" };
        }

        public List<InboxMessage> GetMessagesByAddress (string emailAddress)
        {
            return _inbox.InboxMessages.Where(x => x.FromAddress == emailAddress).ToList();
        }

        #endregion

        #region Mailers

        public void SendMail(UserIdentityModel user,string message, List<string> recipients, string label)
        {
            _inbox.InboxMessages.Add(new InboxMessage() //create a copy for the sender with all recipients

            {
                FromAddress = user.EmailAddress,
                ToAddress = string.Join(";", recipients),
                LabelId = GetLabelID(label),
                Message = message,
                DateReceived = DateTime.Now,
                UserId = user.UserId
            });

            foreach (var recipient in recipients) // create a copy for each recipient
            {
                
                _inbox.InboxMessages.Add(new InboxMessage() 
                {
                    FromAddress = user.EmailAddress,
                    ToAddress = recipient,
                    LabelId = GetLabelID(label),
                    Message = message,
                    DateReceived = DateTime.Now,
                    UserId = GetUserProfile(recipient).UserID
                });
            }

            _inbox.SaveChanges();

        }

        #endregion

        private int GetLabelID (string label)
        {
           return _inbox.Labels.FirstOrDefault(x => x.Name == label).ID;
        }


        public Label GetLabelbyID(int? labelID)
        {
            return _inbox.Labels.FirstOrDefault(x => x.ID == labelID);
        }

        public UserProfile GetUserProfile(string email,string fullNames)
        {
            return _inbox.UserProfiles.FirstOrDefault(x => x.FullNames == fullNames && x.Username == email);
        }

        public UserProfile GetUserProfile(string email)
        {
            return _inbox.UserProfiles.FirstOrDefault(x =>  x.Username == email);
        }

        public DataOperationOutcome RegisterNewUser (string userName, string password,string fullNames)
        {
            var user = _inbox.UserProfiles.FirstOrDefault(x => x.Username == userName);
            if (user != null)
            {
                return new DataOperationOutcome() { IsSuccessful = false, Message = "User alredy exists" };

            }

             _inbox.UserProfiles.Add(new UserProfile() { 
            FullNames = fullNames,
            Password = password,
            Role = "user",
            UserID = Guid.NewGuid(),
            Username = userName
            });

            _inbox.SaveChanges();

            return new DataOperationOutcome() { IsSuccessful = true };
        }
    }
}