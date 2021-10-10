using System;
using System.Collections.Generic;
using InboxAPI.Models;

namespace InboxAPI.Interfaces
{
    interface IDataLayer
    {
        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="username">user email</param>
        /// <param name="password">user's password</param>
        /// <returns></returns>
        UserProfile Validateuser(string username, string password);

        /// <summary>
        /// Adds label to the label list
        /// </summary>
        /// <param name="labelName">label name</param>
        /// <returns></returns>
        DataOperationOutcome AddLabel(string labelName);

        /// <summary>
        /// Deletes a label
        /// </summary>
        /// <param name="labelName">Label name</param>
        /// <returns></returns>
        DataOperationOutcome DeleteLabel(string labelName);

        /// <summary>
        /// Gets deleted inbox messages
        /// </summary>
        /// <param name="userID">user identifier</param>
        /// <returns></returns>
        List<InboxMessage> GetDeletedMessages(Guid userID);

        /// <summary>
        /// Deletes a message
        /// </summary>
        /// <param name="messageId">Message identifier</param>
        /// <param name="userId">user Identifier</param>
        /// <returns>Data operation outcome</returns>
        DataOperationOutcome DeleteMessage(int messageId, Guid userId);

        /// <summary>
        /// Deletes a message
        /// </summary>
        /// <param name="messageId">Message identifier</param>
        /// <param name="userId">user Identifier</param>
        /// <returns>Data operation outcome</returns>
        DataOperationOutcome RetrieveDeletedMessage(int messageId, Guid userId);

        /// <summary>
        /// Gets all messages in inbox
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns></returns>
        List<InboxMessage> GetAllMessages(Guid userId);

        /// <summary>
        /// Gets all the labels
        /// </summary>
        /// <returns></returns>
        List<Label> getAllLabels();

        /// <summary>
        /// Gets the user details
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="fullNames">full names</param>
        /// <returns></returns>
        UserProfile GetUserProfile(string email, string fullNames);

        /// <summary>
        /// Gets the user details
        /// </summary>
        /// <param name="email">username/emailAddress</param>
        /// <returns></returns>
        UserProfile GetUserProfile(string email);

        DataOperationOutcome DeleteLabelFromMail(Guid userId, int messageId);

        /// <summary>
        /// Adds label to a message
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="newLabel">Label name</param>
        /// <param name="messageId">message identifier</param>
        /// <returns></returns>
        DataOperationOutcome AddLabelToMail(Guid userId, string newLabel, int messageId);

        /// <summary>
        /// Gets the label information
        /// </summary>
        /// <param name="labelID">Label Identifier</param>
        /// <returns></returns>
        Label GetLabelbyID(int? labelID);

        /// <summary>
        /// Gets all messages by label name
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="label">Label name</param>
        /// <returns>List of of messages</returns>
        List<InboxMessage> GetAllMessagesByLabel(Guid userId, string label);


        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userName">email address</param>
        /// <param name="password">hashed password</param>
        /// <param name="fullNames">full names</param>
        DataOperationOutcome RegisterNewUser(string userName, string password, string fullNames);

        /// <summary>
        /// Gets inbox messages sent from an address
        /// </summary>
        /// <param name="emailAddress">Origin email address</param>
        /// <returns></returns>
        List<InboxMessage> GetMessagesByAddress(string emailAddress);

        /// <summary>
        /// Send email 
        /// </summary>
        /// <param name="user">Sender profile</param>
        /// <param name="message">message</param>
        /// <param name="recipients">List of recipients</param>
        /// <param name="label">message label</param>
        void SendMail(UserIdentityModel user, string message, List<string> recipients, string label);
    }
}
