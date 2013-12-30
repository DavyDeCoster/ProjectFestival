using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.model
{
    public class Message
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _messageText;

        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        public static ObservableCollection<Message> GetMessages()
        {
            ObservableCollection<Message> ocMessage = new ObservableCollection<Message>();
            string sSql = "Select * from Message";
            DbDataReader reader = DbAccess.GetData(sSql);
            while (reader.Read())
            {
                Message a = new Message()
                {
                    Id = (int)reader[0],
                    MessageText = (string)reader[1]
                };
                ocMessage.Add(a);
            }
            return ocMessage;
        }

        public static void AddMessage(Message m)
        {
            string sSql = "insert into Message (Message) values (@message)";

            DbParameter p1 = DbAccess.AddParameter("@message", m.MessageText);

            DbAccess.ModifyData(sSql, p1);
        }
        
    }
}
