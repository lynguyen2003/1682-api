using AutoMapper;
using DataServices.Interfaces;
using DataServices.Services.RedisCacheService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Video;
using Models.Entities;

namespace _1682_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VideosController : BaseController
    {
        public VideosController(IUnitOfWorks unitOfWorks, IMapper mapper, IRedisCacheService cache) : base(unitOfWorks, mapper, cache)
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

            var videoDto = _mapper.Map<VideoDTO>(video);

            // Lấy thông tin like từ cache
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var likeKey = $"video:{id}:like:{userId}";
                videoDto.IsLikedByUser = await _cache.GetAsync(likeKey) != null;
            }

            // Lấy số lượng comments từ cache
            //videoDto.CommentsCount = await GetCommentCountFromCacheAsync(id);

            return Ok(videoDto);
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

        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikeVideo(int id)
        {
            var video = await _context.Videos.Include(v => v.Likes)
            .FirstOrDefaultAsync(v => v.id == videoId);
            var user = await _unitOfWorks.Auths.FindAsync(userId);

            // Kiểm tra xem user đã like video chưa trong Redis
            var isLiked = await _cache.GetAsync(likeKey) != null;

            if (isLiked)
            {
                // Unlike
                await _unitOfWorks.Likes.Delete(l => l.video_id == videoId && l.user_id == userId);
                await _cache.DeleteAsync(likeKey);
            }
            else
            {
                // Like
                await _unitOfWorks.Likes.Add(new Likes { video_id = videoId, user_id = userId });
                await _cache.SetAsync(likeKey, "1"); // Giá trị "1" có thể là bất kỳ giá trị nào
            }

            await _unitOfWorks.CompleteAsync();
            return NoContent();
        }

        /*[HttpPost("{videoId}/comment")]
        public async Task<IActionResult> CommentVideo(int videoId, [FromBody] CommentDto commentDto)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var comment = new Comments
            {
                user_id = userId,
                video_id = videoId,
                comment = commentDto.Comment,
                Timestamp = DateTime.UtcNow
            };

            await _unitOfWorks.Comments.Add(comment);
            await _unitOfWorks.CompleteAsync();

            // Xóa cache comment của video để đảm bảo hiển thị comment mới nhất
            await _cache.DeleteAsync($"video:{videoId}:comments");

            return CreatedAtAction(nameof(GetVideo), new { id = videoId }, _mapper.Map<CommentDto>(comment));
        }*/

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






        /*private async Task<int> GetCommentCountFromCacheAsync(int videoId)
        {
            var cacheKey = $"video:{videoId}:commentcount";
            var cachedCount = await _cache.GetAsync(cacheKey);

            if (int.TryParse(cachedCount, out var count))
            {
                return count;
            }

            // Nếu không có cache, lấy từ database và lưu vào cache
            count = await _unitOfWorks.Comments.GetAll().CountAsync(c => c.video_id == videoId);
            await _cache.SetAsync(cacheKey, count.ToString(), TimeSpan.FromMinutes(5)); // Cache 5 phút

            return count;
        }*/

    }
}
