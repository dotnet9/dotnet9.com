using AutoMapper;
using Dotnet9.Application.Contracts.Donations;
using Dotnet9.Domain.Donations;

namespace Dotnet9.Application.Donations;

public class DonationAppService : IDonationAppService
{
    private readonly IDonationRepository _donationRepository;
    private readonly IMapper _mapper;

    public DonationAppService(IDonationRepository donationRepository, IMapper mapper)
    {
        _donationRepository = donationRepository;
        _mapper = mapper;
    }

    public async Task<DonationDto?> GetAsync()
    {
        var donation = await _donationRepository.GetAsync(x => x.Id > 0);
        return donation == null ? null : _mapper.Map<Donation, DonationDto>(donation);
    }

    public async Task<bool> UpdateAsync(DonationDto donationDto)
    {
        var donation = await _donationRepository.GetAsync(x => x.Id > 0);
        if (donation == null)
        {
            donation = _mapper.Map<DonationDto, Donation>(donationDto);
            await _donationRepository.InsertAsync(donation);
        }
        else
        {
            donation.Content = donationDto.Content;
            await _donationRepository.UpdateAsync(donation);
        }

        return true;
    }
}