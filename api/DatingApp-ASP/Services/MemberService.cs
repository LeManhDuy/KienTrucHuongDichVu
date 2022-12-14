using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Data;
using DatingApp.API.Data.Entities;
using DatingApp.API.DTOs;
using DatingApp_ASP.DTOs;

namespace DatingApp.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MemberService(DataContext _context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = _context;
        }
        public MemberDto GetMemberByUsername(string username)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<User, MemberDto>(user);
        }

        public List<MemberDto> GetMembers(MemberFilterDto memberFilterDto)
        {
            return _context.AppUsers
                .Where(member => member.Username.Contains(memberFilterDto.Keyword)
                || member.Email.Contains(memberFilterDto.Keyword)
                || member.KnowAs.Contains(memberFilterDto.Keyword)
                || member.Introduction.Contains(memberFilterDto.Keyword))
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToList();
            // var users = _context.AppUsers.ToList();
            // return _mapper.Map<List<User>, List<MemberDto>>(users);
        }
    }
}