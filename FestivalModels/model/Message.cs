using Festival.Model.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Festival.model
{
    public class Message : IDataErrorInfo
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _messageText;

        [Required(ErrorMessage = "De tekst is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
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
                    MessageText = (string)reader[2],
                };
                ocMessage.Add(a);
            }
            reader.Close();
            return ocMessage;
        }

        public static void AddMessage(Message m)
        {
            string sSql = "insert into Message (Message) values (@message)";

            DbParameter p1 = DbAccess.AddParameter("@message", m.MessageText);

            DbAccess.ModifyData(sSql, p1);
        }

        public string Error
        {
            get { return "Dit object is niet als juist gevalideerd"; }
        }

        public string this[string columName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columName
                    });
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                return string.Empty;
            }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
