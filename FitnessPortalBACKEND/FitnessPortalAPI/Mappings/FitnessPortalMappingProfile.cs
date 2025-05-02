using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Models.UserProfileActions;

namespace FitnessPortalAPI.Mappings;
public class FitnessPortalMappingProfile : Profile
{
	public FitnessPortalMappingProfile()
	{
		CreateMap<CreateArticleDto, Article>()
			.ForMember(dest => dest.DateOfPublication, opt => opt.MapFrom(src => DateTime.Now));
		CreateMap<Article, ArticleDto>()
			.ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.CreatedBy.Username));
		CreateMap<UpdateArticleDto, Article>();

		CreateMap<BMI, BMIDto>();

		CreateMap<Training, TrainingDto>();
		CreateMap<Exercise, ExerciseDto>();
		CreateMap<CreateTrainingDto, Training>();
		CreateMap<CreateExerciseDto, Exercise>();
		CreateMap<Training, TrainingChartDataDto>()
			.ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateOfTraining.ToString("yyyy-MM-dd")))
			.ForMember(dest => dest.Payload, opt => opt.MapFrom(src => src.TotalPayload));

		CreateMap<RegisterUserDto, User>()
			.ForMember(dest => dest.RoleId, opt => opt.Ignore())
			.ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
		CreateMap<User, UserProfileInfoDto>();
		CreateMap<UpdateUserDto, User>();

		CreateMap<FriendshipRequest, FriendshipDto>()
			.ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Username));
		CreateMap<User, FriendDto>();
		CreateMap<User, MatchingUserDto>();
		CreateMap<User, FriendProfileDto>()
			.ForMember(dest => dest.NumberOfFriends, opt => opt.MapFrom(src => src.Friends.Count))
			.ForMember(dest => dest.NumberOfTrainings, opt => opt.Ignore())
			.ForMember(dest => dest.LastThreeTrainings, opt => opt.Ignore());
	}
}
