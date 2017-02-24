using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDevelopment.Model
{
   public class Message
    {
       public string Id { get; set; }

       public string Category { get; set; }

       public string TextId { get; set; }

       public byte[] EntryContent { get; set; }

       public TextEdit TextEdit { get; set; }
    }
}
