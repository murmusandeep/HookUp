using AutoMapper;
using Entities.Dto;
using Entities.Exceptions;
using Entities.Helpers;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Extensions;
using HookUpDAL.Interfaces;

namespace HookUpBLL
{
    public class LikesBLL : ILikesBLL
    {
        private readonly ILikesDAL _likesDAL;
        private readonly IUsersDAL _usersDAL;
        private readonly IMapper _mapper;

        public LikesBLL(ILikesDAL likesDAL, IUsersDAL usersDAL, IMapper mapper)
        {
            _likesDAL = likesDAL;
            _usersDAL = usersDAL;
            _mapper = mapper;
        }
        public async Task<bool> AddLike(string username, int sourceUserId)
        {
            var likedUser = await _usersDAL.GetUserByUsername(username);
            var sourceUser = await _likesDAL.GetUserWithLikes(sourceUserId);

            if (likedUser == null)
                throw new UserNotFoundException(username);

            if (sourceUser.UserName == username)
                throw new BadRequestException("You cannot Liked Yourself");

            var userLike = await _likesDAL.GetUserLike(sourceUserId, likedUser.Id);

            if (userLike != null)
                throw new BadRequestException("You already like this user");

            userLike = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike);

            return await _usersDAL.SaveAllAsync();
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = await _likesDAL.GetUsers();

            var likes = await _likesDAL.GetLikes();

            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                users = likes.Select(like => like.TargetUser);
            }

            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.TargetUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }

            var likedUsers = users.Select(user => new LikeDto
            {
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id,
            });

            return await PagedList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
        }
    }
}
