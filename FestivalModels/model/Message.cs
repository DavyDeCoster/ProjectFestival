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

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _author;

        public string Author
        {
            get { return _author; }
            set { _author = value; }
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
                    Title = (string)reader[1],
                    MessageText = (string)reader[2],
                    Author = (string)reader[3]
                };
                ocMessage.Add(a);
            }
            reader.Close();
            return ocMessage;
        }

        public static void AddMessage(Message m)
        {
            string sSql = "insert into Message (Message, Title, Author) values (@message, @title, @author)";

            DbParameter p1 = DbAccess.AddParameter("@message", m.MessageText);
            DbParameter p2 = DbAccess.AddParameter("@title", m.Title);
            DbParameter p3 = DbAccess.AddParameter("@author", m.Author);

            DbAccess.ModifyData(sSql, p1,p2,p3);
        }
        
    }
}
