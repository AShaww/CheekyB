using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Constants.ExceptionMessageConstants;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.UserPortfolioExceptions;
using CheekyServices.Interfaces;
using Serilog;

namespace CheekyServices.Implementations;

/// <summary>
/// service for dealing with userportfolio 
/// </summary>
public class UserPortfolioService : IUserPortfolioService
{
    private readonly IMapper _mapper;
    private readonly IUserPortfolioRepository _userPortfolioRepository;
    private readonly IUserRepository _userRepository;

    public UserPortfolioService(IUserPortfolioRepository userPortfolioRepository, IUserRepository userRepository, IMapper mapper)
    {
        _userPortfolioRepository = userPortfolioRepository ?? throw new ArgumentNullException(nameof(userPortfolioRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    #region GetUserPortfolioByUserId
    /// <inheritdoc/>
    public async Task<UserPortfolioDto> GetUserPortfolioByUserId(Guid userId)
    {
        var response = await _userPortfolioRepository.GetFirstOrDefault(x => x.UserId == userId);
        
        if (response == null)
        {
            Log.Error($"{UserPortfolioExceptionMessages.UserPortfolioNotFoundExceptionMessage} {userId}");
            throw new CheekyExceptions<UserPortfolioNotFoundException>("The user portfolio with the specified ID does not exist.");
        }

        var userPortfolio = _mapper.Map<UserPortfolioDto>(response);
        var user = await _userRepository.GetFirstOrDefault(x => x.UserId == userId);
        userPortfolio.FirstName = user.FirstName;
        userPortfolio.Surname = user.Surname;
        userPortfolio.FullName = user.FirstName + " " + user.Surname;
        userPortfolio.Email = user.Email;
      
        return _mapper.Map<UserPortfolioDto>(userPortfolio);
    }
    #endregion

    #region InsertUserPortfolio
    /// <inheritdoc/>
    public async Task<UserPortfolioDto> InsertUserPortfolio(UserPortfolioDto portfolioToInsert)
    {
        var portfolioToAdd = _mapper.Map<UserPortfolio>(portfolioToInsert);

        //does user portfolio already exist
        if (await _userPortfolioRepository.DoesExistInDb(x => x.UserId == portfolioToInsert.UserId))
        {
            Log.Error($"{UserPortfolioExceptionMessages.UserPortfolioDuplicateExceptionMessage} {portfolioToInsert.UserId}");
            throw new CheekyExceptions<UserPortfolioConflictException>(UserPortfolioExceptionMessages.UserPortfolioDuplicateExceptionMessage);
        }

        var insertedPortfolio = await _userPortfolioRepository.AddAsync(portfolioToAdd);

        portfolioToInsert = _mapper.Map<UserPortfolioDto>(insertedPortfolio);

        return portfolioToInsert;
    }
    #endregion

    #region UpdateUserPortfolio
    /// <inheritdoc/>
    public async Task<UserPortfolioDto> UpdateUserPortfolio(UserPortfolioDto portfolio)
    {
        var portfolioToUpdate = await _userPortfolioRepository.GetFirstOrDefault(a => a.UserPortfolioId == portfolio.UserPortfolioId);
        
        if (portfolio == null)
        {
            Log.Error($"{UserPortfolioExceptionMessages.UserPortfolioNotFoundExceptionMessage} {portfolio.UserPortfolioId}");
            throw new CheekyExceptions<UserPortfolioNotFoundException>(UserPortfolioExceptionMessages.UserPortfolioNotFoundExceptionMessage);
        }
        
        portfolioToUpdate = _mapper.Map(portfolio, portfolioToUpdate);
        
        await _userPortfolioRepository.UpdateAsync(portfolioToUpdate);

        return portfolio;
    }
    #endregion

}
