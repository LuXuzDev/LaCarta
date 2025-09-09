using AutoMapper;
using Business.Modules.Restaurants.DTOs;
using Domain.FileStorage;
using Domain.Modules.Address.Interfaces;
using Domain.Modules.Restaurants.Enums;
using Domain.Modules.Restaurants.Exceptions;
using Domain.Modules.Restaurants.Interfaces;
using Domain.Modules.Restaurants.Models;
using Domain.Modules.Shared.Exceptions;
using Domain.Modules.Users.Interfaces;


namespace Business.Modules.Restaurants.Services;

public class RestaurantServices : IRestaurantServices
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageServices;
    private readonly IUserRepository _userRepository;
    private readonly IMunicipalityRepository _municipalityRepository;

    public RestaurantServices
        (IRestaurantRepository restaurantRepository, IMapper mapper,
        IFileStorageService fileStorageServices, IUserRepository userRepository,
        IMunicipalityRepository municipalityRepository)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _fileStorageServices = fileStorageServices;
        _userRepository = userRepository;
        _municipalityRepository = municipalityRepository;
    }


    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsActivesAsync(CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetRestaurantsActiveAsync(ct);
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }


    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync(CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetAllAsync(ct);
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }


    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsByManagerIdAsync(int managerId, CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetByRestaurantsManagerId(managerId,ct);
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }


    public async Task<RestaurantDTO> GetByIdRestaurantAsync(int restaurantId, CancellationToken ct)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId, ct);

        if (restaurant == null)
            throw new EntityNotFoundException("Restaurant", restaurantId);
        return _mapper.Map<RestaurantDTO>(restaurant);
    }


    public async Task<IEnumerable<RestaurantDTO>> GetByRestaurantsCousineTypeAsync(CuisineType cousineType, CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetByRestaurantsCousineType(cousineType, ct);
        return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
    }


    public async Task CreateRestaurantAsync(CreateRestaurantDTO restaurant, CancellationToken ct)
    {
        var restaurantEntity = _mapper.Map<Restaurant>(restaurant);

        // 1. Validar que el usuario exista y sea RestaurantManager
        var user = await _userRepository.GetByIdAsync(restaurant.UserId,ct);
        if (user == null)
            throw new EntityNotFoundException("User", restaurant.UserId);

        if (user.Role?.Name != "RestaurantManager")
            throw new InvalidUserRoleException("El usuario debe ser RestaurantManager.");

        // 2. Validar unicidad de nombre, email, teléfono
        await ValidateUniqueName(restaurant.Name, ct);
        await ValidateUniqueEmail(restaurant.Email, ct);
        await ValidateUniquePhoneNumber(restaurant.PhoneNumber, ct);

        // 3. Validar que el municipio exista
        await ValidateExistMunicipality(restaurant.MunicipalityId,ct);

        // 4. Guardar la imagen usando el servicio de almacenamiento de archivos
        restaurantEntity.Image = await _fileStorageServices.SaveFileAsync(restaurant.Image);
        restaurantEntity.IsActive = false;
        restaurantEntity.CreatedAt = DateTime.UtcNow;
        restaurantEntity.UpdatedAt = DateTime.UtcNow;
        await _restaurantRepository.AddAsync(restaurantEntity, ct);
    }


    public async Task UpdateRestaurantAsync(UpdateRestaurantDTO restaurant,CancellationToken ct)
    {
        var restaurantExists = await _restaurantRepository.GetByIdAsync(restaurant.Id, ct);

        if (restaurantExists == null)
            throw new EntityNotFoundException("Restaurant", restaurant.Id);

        if (restaurant.Email != restaurantExists.Email)
            await ValidateUniqueEmail(restaurant.Email, ct, restaurant.Id);

        if (restaurant.Name != restaurantExists.Name)
            await ValidateUniqueName(restaurant.Name, ct, restaurant.Id);

        if (restaurant.PhoneNumber != restaurantExists.PhoneNumber)
            await ValidateUniquePhoneNumber(restaurant.PhoneNumber, ct, restaurant.Id);

        if(restaurant.MunicipalityId != restaurantExists.MunicipalityId)
            await ValidateExistMunicipality(restaurant.MunicipalityId, ct);

        string imageURL = restaurantExists.Image!;

        if (restaurant.Image != null)
        {
            var imageExist = await _fileStorageServices.FileExistsAsync(restaurantExists.Image!);
            if (imageExist)
                await _fileStorageServices.DeleteFileAsync(restaurantExists.Image!);
            imageURL = await _fileStorageServices.SaveFileAsync(restaurant.Image);
        }

        restaurantExists.Image = imageURL;

        UpdateRestaurat(restaurant,restaurantExists);
        
        await _restaurantRepository.UpdateAsync(ct);
    }


    private void UpdateRestaurat (UpdateRestaurantDTO restaurant, Restaurant restaurantExists)
    {
        restaurantExists.Name = restaurant.Name;
        restaurantExists.Email = restaurant.Email;
        restaurantExists.PhoneNumber = restaurant.PhoneNumber;
        restaurantExists.HasDelivery = restaurant.HasDelivery;
        restaurantExists.OpenHour = restaurant.OpenHour;
        restaurantExists.CloseHour = restaurant.CloseHour;
        restaurantExists.MunicipalityId = restaurant.MunicipalityId;
        restaurantExists.IsActive = restaurant.IsActive;
        restaurantExists.UpdatedAt = DateTime.UtcNow;
        restaurantExists.CuisineType = restaurant.CuisineType;
        restaurantExists.RestaurantTags = restaurant.RestaurantTags;
    }


    #region Validators
    private async Task ValidateUniqueName(string name,CancellationToken ct,int? restaurantId=null)
    {
        if (!await _restaurantRepository.IsNameUniqueAsync(name, ct,restaurantId))
            throw new NotUniqueNameException(name);
    }

    private async Task ValidateUniqueEmail(string email, CancellationToken ct, int? restaurantId = null)
    {
        if (!await _restaurantRepository.IsEmailUniqueAsync(email, ct, restaurantId))
            throw new NotUniqueEmailException(email);
    }

    private async Task ValidateUniquePhoneNumber(string phoneNumber, CancellationToken ct, int? restaurantId=null)
    {
        if (!await _restaurantRepository.IsPhoneNumberUniqueAsync(phoneNumber, ct,restaurantId))
            throw new NotUniquePhoneNumber(phoneNumber);
    }

    private async Task ValidateExistMunicipality(int municipalityId, CancellationToken ct)
    {
        var municiaplityExist = await _municipalityRepository.GetByIdAsync(municipalityId, ct);
        if (municiaplityExist == null)
            throw new EntityNotFoundException("Municipality", municipalityId);
    }
    #endregion
}
