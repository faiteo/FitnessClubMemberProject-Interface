using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubMemberProject_Interface.DBLayer
{
    interface IMember
    {
        List<Member> GetAllMembers();
        Member FindMemberByID(int id);
        int DeleteMember(int id);
        int AddNewMember(string fn, string ln, string adr, string city, string phnr, string mail);
    }
}
