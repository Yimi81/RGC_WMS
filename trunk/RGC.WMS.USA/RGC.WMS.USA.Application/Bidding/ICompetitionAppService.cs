using RGC.WMS.USA.Application.Bidding.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain.Entities.Bidding;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bidding
{
    public interface ICompetitionAppService :IAppService
    {
        ResponseDto<string> Create(long loginId,CompetitionEditDto dto);

        ResponseDto<string> Delete(long loginId,long id);

        ResponseDto<string> Update(long loginId,CompetitionEditDto dto);

        ResponseDto<string> UpdateStatus(long id, long modifierUserId, bool isValid);

        ResponseDto<CompetitionDto> Get(long id);

        ResponsePageDto<CompetitionDto> GetPlatformCompetitionList(CompetitionSearchDto dto);

        ResponseDto<string> AddPlatform(CompetitionEditDto entity);

        int BatchUpdatePrice(List<CompetitionDaily> entity);

        ResponseDto<string> UpdatePlatform(CompetitionEditDto entity);

        ResponseDto<string> UpdateCompetitionPlatformPrice(long adminId, long CompetitionId, string retailPriceString);

        ResponseDto<string> DeletePlatform(long loginId,long id);
        ResponseDto<string> UpdatePlatformStatus(long loginId,long id, int status);

        ResponseDto<string> ForceRefreshCompetitionDict();

        ResponseDto<string> ForceRefreshCompetitionDailyDict();


        List<CompetitionDto> GetCompetitionPlatFormList();
        List<PlatformCompetitionObj> GetPlatFormCompetitionList();

        ResponsePageDto<CompetitionDto> GetItemCompetitionList(string key, long ItemId, int pageSize, int currentPage);
        ResponsePageDto<CompetitionDto> GetCompetitionPlatformList(CompetitionSearchDto dto);

        ResponsePageDto<CompetitionDto> GetCompetitionPlatformDetailList(CompetitionSearchDto dto);

        ResponsePageDto<SearchOutput> GetCompetitionDailySearchList(string key);

        ResponsePageDto<CompetitionDaily> GetCompetitionDailyList(CompSearchDto dto);

        ResponsePageDto<CompetitionDaily> GetCompetitionDailyChartList(CompSearchDto dto);

        ResponseDto<string> UpdateCompetitionDaily(CompetitionDaily entity);

        ResponseDto<string> AddCompetitionDaily(CompetitionDaily entity);


        ResponseDto<string> DeleteCompetitionDaily(long loginId,long id);

        int BatchAddCompetitionDaily(List<CompetitionDaily> dto);
        /// <summary>
        /// 批量修改平台CompetitionDto状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int BatchChangeCompetitionPlatformStatus(List<CompetitionDto> dto);
    }
}
