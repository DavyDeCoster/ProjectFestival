using Festival.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestivalMvc.Models.DAL
{
    public class MessageRepository
    {
        public ObservableCollection<Message> GetMessages()
        {
            return Message.GetMessages();
        }
    }
}