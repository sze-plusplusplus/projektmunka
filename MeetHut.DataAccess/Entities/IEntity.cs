using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.DataAccess.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        string ToString();
        Dictionary<string, string> GetKeyValuePairs();
    }
}
