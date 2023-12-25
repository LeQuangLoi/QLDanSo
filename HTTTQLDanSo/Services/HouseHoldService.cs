using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTTTQLDanSo.Services
{
    public class HouseHoldService : IHouseHoldService
    {
        private readonly IRegionRepository _iRegionRepository;
        private readonly IAddressRepository _iAddressRepository;
        private readonly IHouseHoldRepository _iHouseHoldRepository;
        private readonly IPersonalChangeRepository _iPersonalChangeRepository;
        private readonly IPersonalRepository _iPersonalRepository;
        private readonly IFamilyMemberRepository _iFamilyMemberRepository;

        public HouseHoldService(IRegionRepository iRegionRepository, IAddressRepository iAddressRepository
            , IHouseHoldRepository iHouseHoldRepository, IPersonalChangeRepository iPersonalChangeRepository, IPersonalRepository iPersonalRepository
            , IFamilyMemberRepository iFamilyMemberRepository)
        {
            _iRegionRepository = iRegionRepository;
            _iAddressRepository = iAddressRepository;
            _iHouseHoldRepository = iHouseHoldRepository;
            _iPersonalChangeRepository = iPersonalChangeRepository;
            _iPersonalRepository = iPersonalRepository;
            _iFamilyMemberRepository = iFamilyMemberRepository;
        }

        public async Task<IEnumerable<Address>> GetAddressesByRegionIdAsync(string regionId)
        {
            if (string.IsNullOrEmpty(regionId))
            {
                return Enumerable.Empty<Address>();
            }

            return await _iAddressRepository.GetAddressesByRegionIdAsync(regionId);
        }

        public async Task<IEnumerable<Address>> GetAddressesByWorkerIdAsync(int? wokerId)
        {
            if (!wokerId.HasValue)
            {
                return Enumerable.Empty<Address>();
            }
            return await _iAddressRepository.GetAddressesByWorkerIdAsync(wokerId.Value);
        }

        public async Task<IEnumerable<Region>> GetRegionsByParrentIdAsync(string parrentId)
        {
            if (string.IsNullOrEmpty(parrentId))
            {
                return Enumerable.Empty<Region>();
            }

            return await _iRegionRepository.GetRegionsByParrentIdAsync(parrentId);
        }

        public async Task<Region> GetRegionByRegionIdAsync(string regionId)
        {
            if (string.IsNullOrEmpty(regionId))
            {
                return null;
            }

            return await _iRegionRepository.GetRegionByRegionIdAsync(regionId);
        }

        public async Task<IEnumerable<HouseHold>> GetHouseHoldByHouseHoldIDAndRegionIdAndStatusAsync(string regionId, string addressID, IEnumerable<string> houseHoldStatus)
        {
            if (string.IsNullOrEmpty(regionId) || string.IsNullOrEmpty(addressID) || !houseHoldStatus.Any())
            {
                return Enumerable.Empty<HouseHold>();
            }

            return await _iHouseHoldRepository.GetHouseHoldByRegionIdAndAddressIDAndHouseHoldStatusAsync(regionId, addressID, houseHoldStatus);
        }

        public async Task<IEnumerable<PersonalInfo>> GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(string houseHoldID, string regionId, IEnumerable<string> personStatuss)
        {
            return await _iPersonalRepository.GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(houseHoldID, regionId, personStatuss);
        }

        public async Task<IEnumerable<PersonalChange>> GetPersonalChangeByPersonalIDAndRegionIdAsync(string personalID, string regionId)
        {
            return await _iPersonalChangeRepository.GetPersonalChangeByPersonalIDAndRegionIdAsync(personalID, regionId);
        }

        public async Task<IEnumerable<FamilyMember>> GetFamilyMemberAsync(string houseHoldID, string regionId, string mother_ID)
        {
            return await _iFamilyMemberRepository.GetFamilyMemberAsync(houseHoldID, regionId, mother_ID);
        }
    }
}