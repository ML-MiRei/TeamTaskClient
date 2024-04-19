using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.CQRS.Team.Queries.GetTeamsByUserId
{
    public class GetTeamsByUserIdHandler(ITeamRepository teamRepository) : IRequestHandler<GetTeamsByUserIdCommand, List<TeamModel>>
    {
        public Task<List<TeamModel>> Handle(GetTeamsByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var teams = teamRepository.GetTeamsByUserId(request.UserId);
                if (teams == null || teams.Result.Count == 0)
                {
                    return Task.FromResult(new List<TeamModel>());
                }
                return teams;
            }
            catch (Exception)
            {
                return Task.FromResult(new List<TeamModel>());
            }
        }
    }
}
