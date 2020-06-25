using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set; }
    }
}
