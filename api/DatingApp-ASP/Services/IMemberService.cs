using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp_ASP.DTOs;

namespace DatingApp.API.Services
{
    public interface IMemberService
    {
        List<MemberDto> GetMembers(MemberFilterDto memberFilterDto);

        MemberDto GetMemberByUsername(string username);
    }
}