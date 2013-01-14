using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecknerSharePointSolutions.Models
{
    public class ProposalContact
    {
        public ProposalContact()
        {

        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
