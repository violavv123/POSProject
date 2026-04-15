using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject.models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Severity { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string RelatedEntity { get; set; }
        public int? RelatedEntityId { get; set; }
        public bool IsRead { get; set; }
        public DateTime Created_At { get; set; }
        public int? Created_By { get; set; }

    }
}
