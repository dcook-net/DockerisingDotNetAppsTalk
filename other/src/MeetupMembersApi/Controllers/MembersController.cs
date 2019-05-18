using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MeetupMembers.Model;
using MeetupMembers.Mongo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeetupMembers.Controllers
{
    [Route("members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IDataRepository<Member> _membersRepository;
        private readonly ILogger<MembersController> _logger;

        public MembersController(IDataRepository<Member> membersRepository, ILogger<MembersController> logger)
        {
            _membersRepository = membersRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] Member member, CancellationToken cancellationToken)
        {
            member.Id = Guid.NewGuid().ToString();

            await _membersRepository.Create(member, cancellationToken);

            return Created(new Uri($"{Request.Scheme}://{Request.Host}/members/{member.Id}"), member);
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