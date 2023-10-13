using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Request
{
    [Table("CooperationRequest", Schema = "RE")]
    public class CooperationRequest : BaseEntity<int>, IEntity<int>
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile{ get; set; }
        public string  Courses { get; set; }
        public string Description { get; set; }
        public string File { get; set; }

    }
}
