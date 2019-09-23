using System;
using System.Threading;
using System.Threading.Tasks;
using MeetupMembersApi.Models;
using MeetupMembersApi.Mongo;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace MeetupMembersApi.Controllers
{
    [Route("members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IDataRepository<Member> _membersRepository;

        public MembersController(IDataRepository<Member> membersRepository)
        {
            _membersRepository = membersRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] Member member, CancellationToken cancellationToken)
        {
            member.Id = ObjectId.GenerateNewId().ToString();

            await _membersRepository.Create(member, cancellationToken);

            return Created(new Uri($"{Request.Scheme}://{Request.Host}/members/{member.Id}"), member);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken)
        {
            return Ok(await _membersRepository.ReadAll(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember([FromRoute] string id, CancellationToken cancellationToken)
        {
            var member = await _membersRepository.Read(id, cancellationToken);
            
            if (member is null)
                return NotFound();
            
            return Ok(member);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateMember([FromBody] Member member, CancellationToken cancellationToken)
        {
            var updatedMember = await _membersRepository.Update(member, cancellationToken);
            
            return Ok(updatedMember);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _membersRepository.Delete(id, cancellationToken);

            if (result)
                return Ok();

            return NotFound();
        }
    }
}