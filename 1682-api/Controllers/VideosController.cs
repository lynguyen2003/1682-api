using AutoMapper;
using DataServices.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Video;
using Models.Entities;

namespace _1682_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VideosController : BaseController
    {
        public VideosController(IUnitOfWorks unitOfWorks, IMapper mapper) : base(unitOfWorks, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var videos = await _unitOfWorks.Videos.GetAll();

            return Ok(_mapper.Map<List<VideoDTO>>(videos));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var video = await _unitOfWorks.Videos.GetById(id);
            if (video == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VideoDTO>(video));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VideoCreateDTO video)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = _mapper.Map<Videos>(video);

            await _unitOfWorks.Videos.Add(result);
            await _unitOfWorks.CompleteAsync();

            return CreatedAtAction("Get", new { id = video.user_id }, _mapper.Map<VideoDTO>(result));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VideoUpdateDTO videoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var video = await _unitOfWorks.Videos.GetById(id);
            if (video == null)
                return NotFound();

            var result = _mapper.Map<Videos>(videoDto);

            await _unitOfWorks.Videos.Update(result);
            await _unitOfWorks.CompleteAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var video = await _unitOfWorks.Videos.GetById(id);
            if (video == null)
            {
                return NotFound();
            }

            await _unitOfWorks.Videos.Delete(id);
            await _unitOfWorks.CompleteAsync();

            return NoContent();
        }

    }
}
