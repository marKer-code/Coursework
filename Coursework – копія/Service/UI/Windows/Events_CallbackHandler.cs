using System.Collections.Generic;

namespace UI.Windows
{
    public class Events_CallbackHandler
    {
        public void NewChat(string senderLogin)
        {
            Lists.chats.Add(senderLogin);
            if (Lists.noChat.Contains(senderLogin))
                Lists.noChat.Remove(senderLogin);
        }
        public void DeleteChat(string toDeleteLogin)
        {
            Lists.chats.Remove(toDeleteLogin);
            foreach (var item in Lists.messages)
                if (item.Value == toDeleteLogin)
                    Lists.messages.Remove(item.Key);
        }
        public void DeleteContact(string toDeleteLogin)
        {
            Lists.contacts.Remove(toDeleteLogin);
            Lists.chats.Remove(toDeleteLogin);
            foreach (var item in Lists.messages)
                if (item.Value == toDeleteLogin)
                    Lists.messages.Remove(item.Key);
        }
        public void ReceiveMessage(string obj)
        {
            string[] mes = obj.Split(' ');
            Lists.messages.Add(new List<string>()
            {
                mes[1],
                mes[2],
                mes[3]
            },
            mes[0]);
        }
        public void RejectRequest_(string receiverLogin)
               => Lists.sendRequests.Remove(receiverLogin);
        public void NewContact(string contactLogin)
        {
            Lists.sendRequests.Remove(contactLogin);
            Lists.contacts.Add(contactLogin);
            Lists.noChat.Add(contactLogin);
        }
        public void ReceiveRequest(string senderLogin)
           => Lists.receivedRequests.Add(senderLogin);
    }
}
